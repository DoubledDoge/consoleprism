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
public sealed class ProgressBar(
	IRenderer renderer,
	int current,
	string? label,
	bool inPlace,
	int total = 100
) : ComponentBase
{
	private string? Label { get; } = label;
	private int Total { get; } = total;
	private bool InPlace { get; } = inPlace;

	/// <summary>Gets or sets the current progress value of the bar.</summary>
	public int Current { get; set; } = current;

	/// <summary>Gets or sets the width of the bar in characters.</summary>
	public int BarWidth { get; set; } = 40;

	/// <summary>
	/// Initializes a new <see cref="ProgressBar"/> using the default console renderer.
	/// </summary>
	/// <param name="current">The current progress label.</param>
	/// <param name="label">The optional label.</param>
	/// <param name="total">The optional max progress value.</param>
	/// <param name="inPlace">If the bar updates in place which
	/// overwrites the current console line rather than advancing to a new one.</param>
	public ProgressBar(int current, string? label, int total, bool inPlace)
		: this(ConsoleRenderer.Instance, current, label, inPlace, total) { }

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
			renderer.WriteColored($"{Label}: ", colors.ProgressBarText);
		}

		renderer.Write("[");
		renderer.WriteColored(new string('█', filledWidth), colors.ProgressBarComplete);
		renderer.WriteColored(new string('░', emptyWidth), colors.ProgressBarIncomplete);
		renderer.WriteColored($"] {percentage:P0}", colors.ProgressBarText);
		if (InPlace)
		{
			ConsoleHelper.HideCursor();
			renderer.SetCursorPosition(startLeft, startTop);
			if (renderCurrent >= renderTotal)
			{
				ConsoleHelper.ShowCursor();
			}
		}
		else
		{
			renderer.WriteLine();
		}
	}
}
