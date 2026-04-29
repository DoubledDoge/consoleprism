namespace ConsolePrism.Components;

using Core;
using Core.Renderers;
using Interfaces;
using Themes;

/// <summary>
/// A UI component that renders a visual progress bar to the console.
/// </summary>
/// <param name="renderer">The renderer to write output to.</param>
/// <param name="current">The current progress label.</param>
/// <param name="label">The optional label.</param>
/// <param name="inPlace">If the bar updates in place which
/// overwrites the current console line rather than advancing to a new one.</param>
/// <param name="total">The optional max progress value.</param>
/// <param name="barWidth">The width of the bar in characters. Defaults to 40.</param>
public sealed class ProgressBar(
	IRenderer renderer,
	int current,
	string? label,
	bool inPlace,
	int total = 100,
	int barWidth = 40
) : ComponentBase
{
	private IRenderer _renderer = renderer;
	private string? Label { get; } = label;
	private int Total { get; } = total;
	private bool InPlace { get; } = inPlace;
	private int BarWidth { get; } = barWidth;

	/// <summary>Gets or sets the current progress value of the bar.</summary>
	private int Current { get; set; } = current;

	/// <summary>
	/// Initializes a new <see cref="ProgressBar"/> using the default console renderer.
	/// </summary>
	/// <param name="current">The current progress label.</param>
	/// <param name="label">The optional label.</param>
	/// <param name="total">The optional max progress value.</param>
	/// <param name="inPlace">If the bar updates in place which
	/// overwrites the current console line rather than advancing to a new one.</param>
	/// <param name="barWidth">The width of the bar in characters. Defaults to 40.</param>
	public ProgressBar(int current, string? label, int total, bool inPlace, int barWidth = 40)
		: this(ConsoleRenderer.Instance, current, label, inPlace, total, barWidth) { }

	/// <inheritdoc />
	protected override bool SupportsRendererSwap => true;

	/// <inheritdoc />
	protected override IRenderer SwapRenderer(IRenderer? swapRenderer)
	{
		IRenderer previous = _renderer;
		if (swapRenderer is not null)
		{
			_renderer = swapRenderer;
		}
		return previous;
	}

	/// <inheritdoc/>
	public override void Render()
	{
		ColorScheme colors = ActiveTheme.Colors;

		int renderTotal = Math.Max(1, Total);
		int renderCurrent = Math.Clamp(Current, 0, renderTotal);

		double percentage = (double)renderCurrent / renderTotal;
		int filledWidth = (int)(BarWidth * percentage);
		int emptyWidth = BarWidth - filledWidth;

		int startLeft = Console.CursorLeft;
		int startTop = Console.CursorTop;

		if (!string.IsNullOrEmpty(Label))
		{
			_renderer.WriteColored($"{Label}: ", colors.ProgressBarText);
		}

		_renderer.Write("[");
		_renderer.WriteColored(new string('█', filledWidth), colors.ProgressBarComplete);
		_renderer.WriteColored(new string('░', emptyWidth), colors.ProgressBarIncomplete);
		_renderer.WriteColored($"] {percentage:P0}", colors.ProgressBarText);
		if (InPlace)
		{
			ConsoleHelper.HideCursor();
			_renderer.SetCursorPosition(startLeft, startTop);
			if (renderCurrent >= renderTotal)
			{
				ConsoleHelper.ShowCursor();
			}
		}
		else
		{
			_renderer.WriteLine();
		}
	}
}
