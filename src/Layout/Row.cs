using ConsolePrism.Components;
using ConsolePrism.Core.Renderers;
using ConsolePrism.Interfaces;

namespace ConsolePrism.Layout;

/// <summary>
/// A vertical layout container that renders a sequence of <see cref="IRenderable"/>
/// components stacked top to bottom, with optional spacing between them.
/// </summary>
/// <param name="renderer">The renderer to write output to.</param>
public sealed class Row(IRenderer renderer) : ComponentBase
{
    private readonly List<IRenderable> _children = [];

    /// <summary>
    /// Gets or sets the number of blank lines inserted between each child component.
    /// </summary>
    public int Spacing { get; set; } = 0;

    /// <summary>
    /// Initialises a new <see cref="Row"/> using the default console renderer.
    /// </summary>
    public Row()
        : this(ConsoleRenderer.Instance) { }

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
    protected override IRenderer? SwapRenderer(IRenderer? incoming) => null;

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
