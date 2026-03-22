using ConsolePrism.Core.Renderers;

namespace ConsolePrism.Components;

using Core;
using Interfaces;

/// <summary>
/// A UI component that renders an animated spinner, suitable for indicating
/// ongoing asynchronous or long-running operations.
/// </summary>
/// <remarks>
/// Call <see cref="Start"/> to begin animation on a background thread,
/// and <see cref="Stop"/> to halt it. The spinner renders in place,
/// overwriting the same console line on each tick.
/// </remarks>
/// <param name="frames">The animation frames to cycle through.</param>
/// <param name="renderer">The renderer to write output to.</param>
/// <param name="label">An optional label displayed alongside the spinner.</param>
/// <param name="intervalMs">The delay in milliseconds between each animation frame.</param>
public sealed class Spinner(
    string[] frames,
    IRenderer renderer,
    string? label = null,
    int intervalMs = 80
) : ComponentBase, IDisposable
{
    private readonly string[] _frames = frames.Length > 0 ? frames : Dots;
    private CancellationTokenSource? _cts;
    private Task? _animationTask;
    private bool _disposed;
    private string? Label { get; } = label;
    private int IntervalMs { get; } = intervalMs;

    /// <summary>A classic braille dot spinner.</summary>
    public static readonly string[] Dots = ["⠋", "⠙", "⠹", "⠸", "⠼", "⠴", "⠦", "⠧", "⠇", "⠏"];

    /// <summary>A simple ASCII line spinner.</summary>
    public static readonly string[] Line = ["-", "\\", "|", "/"];

    /// <summary>A block-fill pulse spinner.</summary>
    public static readonly string[] Pulse = ["█", "▓", "▒", "░", "▒", "▓"];

    /// <summary>A bouncing arrow spinner.</summary>
    public static readonly string[] Arrow = ["←", "↖", "↑", "↗", "→", "↘", "↓", "↙"];

    /// <summary>
    /// Initializes a new <see cref="Spinner"/> using the default console renderer
    /// and the <see cref="Dots"/> frame set.
    /// </summary>
    public Spinner()
        : this(Dots, ConsoleRenderer.Instance) { }

    /// <summary>
    /// Initializes a new <see cref="Spinner"/> with an explicit frame set
    /// and the default console renderer.
    /// </summary>
    /// <param name="frames">The animation frames to cycle through.</param>
    public Spinner(string[] frames)
        : this(frames, ConsoleRenderer.Instance) { }

    /// <summary>
    /// Initializes a new <see cref="Spinner"/> with an explicit frame set,
    /// label, and the default console renderer.
    /// </summary>
    /// <param name="frames">The animation frames to cycle through.</param>
    /// <param name="label">An optional label displayed alongside the spinner.</param>
    public Spinner(string[] frames, string? label)
        : this(frames, ConsoleRenderer.Instance, label) { }

    /// <summary>
    /// Initializes a new <see cref="Spinner"/> with an explicit frame set,
    /// label, interval and the default console renderer.
    /// </summary>
    /// <param name="frames">The animation frames to cycle through.</param>
    /// <param name="label">An optional label displayed alongside the spinner.</param>
    /// <param name="intervalMs">The delay in milliseconds between each animation frame.</param>
    public Spinner(string[] frames, string? label, int intervalMs)
        : this(frames, ConsoleRenderer.Instance, label, intervalMs) { }

    /// <inheritdoc/>
    protected override bool SupportsRendererSwap => false;

    /// <inheritdoc/>
    protected override IRenderer? SwapRenderer(IRenderer? incoming) => null;

    /// <summary>
    /// Renders a single frame of the spinner at the current cursor position.
    /// Prefer <see cref="Start"/> for animated output.
    /// </summary>
    public override void Render() => RenderFrame(0);

    /// <summary>
    /// Starts the spinner animation on a background thread.
    /// Has no effect if the spinner is already running.
    /// </summary>
    public void Start()
    {
        if (_animationTask is not null)
        {
            return;
        }

        ConsoleHelper.HideCursor();
        _cts = new CancellationTokenSource();
        CancellationToken token = _cts.Token;

        _animationTask = Task.Run(
            async () =>
            {
                int frame = 0;
                while (!token.IsCancellationRequested)
                {
                    RenderFrame(frame);
                    frame = (frame + 1) % _frames.Length;
                    await Task.Delay(IntervalMs, token).ConfigureAwait(false);
                }
            },
            token
        );
    }

    /// <summary>
    /// Stops the spinner animation and clears the spinner line.
    /// </summary>
    /// <param name="finalMessage">
    /// An optional message to display after stopping.
    /// When <see langword="null"/>, the spinner line is cleared.
    /// </param>
    public void Stop(string? finalMessage = null)
    {
        _cts?.Cancel();

        try
        {
            _animationTask?.Wait();
        }
        catch (AggregateException ex) when (ex.InnerExceptions.All(e => e is TaskCanceledException))
        {
            // Task cancelled cleanly via CancellationToken
        }

        _animationTask = null;
        _cts = null;

        ConsoleHelper.ClearCurrentLine();

        if (!string.IsNullOrEmpty(finalMessage))
        {
            renderer.WriteColoredLine(finalMessage, this.ActiveTheme.Colors.Success);
        }

        ConsoleHelper.ShowCursor();
    }

    private void RenderFrame(int frameIndex)
    {
        int left = Console.CursorLeft;
        int top = Console.CursorTop;

        renderer.WriteColored(_frames[frameIndex], this.ActiveTheme.Colors.Primary);

        if (!string.IsNullOrEmpty(Label))
        {
            renderer.WriteColored(" ", this.ActiveTheme.Colors.Muted);
            renderer.WriteColored(Label, this.ActiveTheme.Colors.Muted);
        }

        renderer.SetCursorPosition(left, top);
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        Stop();
        _cts?.Dispose();
        _disposed = true;
    }
}
