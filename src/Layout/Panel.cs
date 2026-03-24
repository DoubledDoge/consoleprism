namespace ConsolePrism.Layout;

using Components;
using Core.Renderers;
using Interfaces;
using Themes;

/// <summary>
/// A bordered container that renders a title and wraps any <see cref="IRenderable"/>
/// content within a styled box.
/// </summary>
/// <param name="renderer">The renderer to write output to.</param>
/// <param name="title">The title displayed in the panel's top border.</param>
/// <param name="content">The content to render inside the panel.</param>
/// <param name="horizontalPadding">The inner padding in characters on the left and right sides.</param>
/// <param name="verticalPadding">The inner padding in characters on the top and bottom sides.</param>
public sealed class Panel(
	IRenderer renderer,
	string? title,
	IRenderable? content,
	int horizontalPadding = 1,
	int verticalPadding = 0
) : ComponentBase
{
	private int HorizontalPadding { get; } = horizontalPadding;
	private int VerticalPadding { get; } = verticalPadding;

	/// <summary>
	/// Initializes a new <see cref="Panel"/> using the default console renderer.
	/// </summary>s
	/// <param name="title">The title displayed in the panel's top border.</param>
	/// <param name="content">The content to render inside the panel.</param>
	/// <param name="horizontalPadding">The inner padding in characters on the left and right sides.</param>
	/// <param name="verticalPadding">The inner padding in characters on the top and bottom sides.</param>
	public Panel(
		string? title,
		IRenderable? content,
		int horizontalPadding = 1,
		int verticalPadding = 0
	)
		: this(ConsoleRenderer.Instance, title, content, horizontalPadding, verticalPadding) { }

	/// <inheritdoc/>
	protected override IRenderer? SwapRenderer(IRenderer? swapRenderer) => null;

	/// <inheritdoc/>
	public override void Render()
	{
		BorderStyle border = ActiveTheme.Border;
		ColorScheme colors = ActiveTheme.Colors;
		int width = Console.WindowWidth;
		int innerWidth = width - 2;

		RenderTopBorder(border, colors, innerWidth);

		for (int i = 0; i < VerticalPadding; i++)
		{
			RenderEmptyRow(border, colors, innerWidth);
		}

		RenderContent(border, colors, innerWidth);

		for (int i = 0; i < VerticalPadding; i++)
		{
			RenderEmptyRow(border, colors, innerWidth);
		}

		RenderBottomBorder(border, colors, innerWidth);
	}

	private void RenderContent(BorderStyle border, ColorScheme colors, int innerWidth)
	{
		if (content is null)
		{
			return;
		}

		// Propagate theme override to content if applicable
		if (content is ComponentBase { Theme: null } cb && Theme is not null)
		{
			cb.Theme = Theme;
		}

		int contentWidth = innerWidth - HorizontalPadding * 2;

		StringRenderer sr = new();
		content.Render(sr);

		string[] lines = sr.Output.Split(Environment.NewLine);

		foreach (string line in lines)
		{
			renderer.WriteColored(border.Vertical.ToString(), colors.Primary);
			renderer.Write(new string(' ', HorizontalPadding));

			string trimmed =
				line.Length > contentWidth ? line[..contentWidth] : line.PadRight(contentWidth);

			renderer.Write(trimmed);
			renderer.Write(new string(' ', HorizontalPadding));
			renderer.WriteColoredLine(border.Vertical.ToString(), colors.Primary);
		}
	}

	private void RenderTopBorder(BorderStyle border, ColorScheme colors, int innerWidth)
	{
		renderer.WriteColored(border.TopLeft.ToString(), colors.Primary);

		if (!string.IsNullOrEmpty(title))
		{
			string label = $" {title} ";
			int remaining = innerWidth - label.Length;
			int left = remaining / 2;
			int right = remaining - left;

			renderer.WriteColored(new string(border.Horizontal, left), colors.Primary);
			renderer.WriteColored(label, colors.MenuTitle);
			renderer.WriteColored(new string(border.Horizontal, right), colors.Primary);
		}
		else
		{
			renderer.WriteColored(new string(border.Horizontal, innerWidth), colors.Primary);
		}

		renderer.WriteColoredLine(border.TopRight.ToString(), colors.Primary);
	}

	private void RenderBottomBorder(BorderStyle border, ColorScheme colors, int innerWidth)
	{
		renderer.WriteColored(border.BottomLeft.ToString(), colors.Primary);
		renderer.WriteColored(new string(border.Horizontal, innerWidth), colors.Primary);
		renderer.WriteColoredLine(border.BottomRight.ToString(), colors.Primary);
	}

	private void RenderEmptyRow(BorderStyle border, ColorScheme colors, int innerWidth)
	{
		renderer.WriteColored(border.Vertical.ToString(), colors.Primary);
		renderer.Write(new string(' ', innerWidth));
		renderer.WriteColoredLine(border.Vertical.ToString(), colors.Primary);
	}
}
