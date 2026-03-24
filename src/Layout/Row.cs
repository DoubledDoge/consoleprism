namespace ConsolePrism.Layout;

using Components;
using Core.Renderers;
using Interfaces;

/// <summary>
/// A vertical layout container that renders a sequence of <see cref="IRenderable"/>
/// components stacked top to bottom, with optional spacing between them.
/// </summary>
/// <param name="renderer">The renderer to write output to.</param>
/// <param name="spacing">The number of blank lines inserted between each child component.</param>
public sealed class Row(IRenderer renderer, int spacing = 0) : ComponentBase
{
	private readonly List<IRenderable> _children = [];
	private int Spacing { get; } = spacing;

	/// <summary>
	/// Initializes a new <see cref="Row"/> using the default console renderer.
	/// </summary>
	/// <param name="spacing">The number of blank lines inserted between each child component.</param>
	public Row(int spacing = 0)
		: this(ConsoleRenderer.Instance, spacing) { }

	/// <summary>
	/// Adds a child component to this row.
	/// </summary>
	/// <param name="child">The component to add.</param>
	/// <returns>This <see cref="Row"/> instance, enabling a fluent chain.</returns>
	public Row Add(IRenderable child)
	{
		_children.Add(child);
		return this;
	}

	/// <inheritdoc/>
	protected override IRenderer? SwapRenderer(IRenderer? swapRenderer) => null;

	/// <inheritdoc/>
	public override void Render()
	{
		for (int i = 0; i < _children.Count; i++)
		{
			_children[i].Render(renderer);

			if (Spacing <= 0 || i >= _children.Count - 1)
			{
				continue;
			}

			for (int s = 0; s < Spacing; s++)
			{
				renderer.WriteLine();
			}
		}
	}
}
