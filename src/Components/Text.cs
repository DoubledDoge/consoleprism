namespace ConsolePrism.Components;

using Core.Renderers;
using Interfaces;

/// <summary>
/// A simple text component that renders plain or colored text content.
/// </summary>
/// <param name="renderer">The renderer to write output to.</param>
/// <param name="content">The text content to render.</param>
/// <param name="color">Optional color to apply. If null, uses default console color.</param>
public sealed class Text(IRenderer renderer, string content, ConsoleColor? color = null)
	: ComponentBase
{
	private string Content { get; } = content;
	private ConsoleColor? Color { get; } = color;

	/// <summary>
	/// Initializes a new <see cref="Text"/> using the default console renderer.
	/// </summary>
	/// <param name="content">The text content to render.</param>
	/// <param name="color">Optional color to apply. If null, uses default console color.</param>
	public Text(string content, ConsoleColor? color = null)
		: this(ConsoleRenderer.Instance, content, color) { }

	/// <inheritdoc/>
	protected override IRenderer? SwapRenderer(IRenderer? swapRenderer) => null;

	/// <inheritdoc/>
	public override void Render()
	{
		if (Color.HasValue)
		{
			renderer.WriteColored(Content, Color.Value);
		}
		else
		{
			renderer.Write(Content);
		}
	}
}
