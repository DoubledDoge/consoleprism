namespace ConsolePrism.Components;

using Core.Renderers;
using Interfaces;

/// <summary>
/// A simple text component that renders plain or colored text content.
/// </summary>
/// <param name="renderer">The renderer to write output to.</param>
/// <param name="content">The text content to render.</param>
/// <param name="color">Optional color to apply. If null, uses default console color.</param>
public sealed class ConsoleText(IRenderer renderer, string content, ConsoleColor? color = null)
	: ComponentBase
{
	private IRenderer _renderer = renderer;
	private string Content { get; } = content;
	private ConsoleColor? Color { get; } = color;

	/// <summary>
	/// Initializes a new <see cref="ConsoleText"/> using the default console renderer.
	/// </summary>
	/// <param name="content">The text content to render.</param>
	/// <param name="color">Optional color to apply. If null, uses default console color.</param>
	public ConsoleText(string content, ConsoleColor? color = null)
		: this(ConsoleRenderer.Instance, content, color) { }

	/// <inheritdoc/>
	protected override bool SupportsRendererSwap => true;

	/// <inheritdoc/>
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
		if (Color.HasValue)
		{
			_renderer.WriteColoredLine(Content, Color.Value);
		}
		else
		{
			_renderer.WriteLine(Content);
		}
	}
}
