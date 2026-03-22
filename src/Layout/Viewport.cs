using ConsolePrism.Components;
using ConsolePrism.Core.Renderers;
using ConsolePrism.Interfaces;

namespace ConsolePrism.Layout;

/// <summary>
/// A scrollable content region that renders a fixed-height window into
/// a larger set of <see cref="IRenderable"/> children, controllable via
/// <see cref="ScrollUp"/> and <see cref="ScrollDown"/>.
/// </summary>
/// <param name="renderer">The renderer to write output to.</param>
public sealed class Viewport(IRenderer renderer) : ComponentBase
{
    private readonly List<IRenderable> _children = [];

    /// <summary>
    /// Gets or sets the number of visible lines in the viewport.
    /// </summary>
    public int Height { get; set; } = 20;

    /// <summary>
    /// Gets the current scroll offset (zero-based line index of the top visible line).
    /// </summary>
    public int ScrollOffset { get; private set; }

    /// <summary>
    /// Initialises a new <see cref="Viewport"/> using the default console renderer.
    /// </summary>
    public Viewport()
        : this(ConsoleRenderer.Instance) { }

    /// <summary>Adds a child component to the viewport's content.</summary>
    /// <param name="child">The component to add.</param>
    /// <returns>This <see cref="Viewport"/> instance, enabling a fluent chain.</returns>
    public Viewport Add(IRenderable child)
    {
        _children.Add(child);
        return this;
    }

    /// <summary>Scrolls the viewport up by the specified number of lines.</summary>
    /// <param name="lines">Number of lines to scroll. Defaults to 1.</param>
    public void ScrollUp(int lines = 1) => ScrollOffset = Math.Max(0, ScrollOffset - lines);

    /// <summary>Scrolls the viewport down by the specified number of lines.</summary>
    /// <param name="lines">Number of lines to scroll. Defaults to 1.</param>
    public void ScrollDown(int lines = 1) => ScrollOffset += lines;

    /// <summary>Resets the scroll position to the top.</summary>
    public void ScrollToTop() => ScrollOffset = 0;

    /// <inheritdoc/>
    protected override IRenderer? SwapRenderer(IRenderer? incoming) => null;

    /// <inheritdoc/>
    public override void Render()
    {
        // Render all children into a shared buffer to determine total line count
        StringRenderer sr = new();

        foreach (IRenderable child in _children)
        {
            child.Render(sr);
        }

        string[] allLines = sr.Output.Split(Environment.NewLine);

        // Clamp scroll offset against actual content height
        int maxOffset = Math.Max(0, allLines.Length - Height);
        ScrollOffset = Math.Min(ScrollOffset, maxOffset);

        // Render only the visible window using the clamped ScrollOffset
        foreach (string line in allLines.Skip(ScrollOffset).Take(Height))
        {
            renderer.WriteLine(line);
        }
    }
}
