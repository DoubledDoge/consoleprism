namespace ConsolePrism.Interfaces;

/// <summary>
/// Represents any element that can render itself to the console.
/// </summary>
public interface IRenderable
{
    /// <summary>
    /// Renders the element using its configured renderer.
    /// </summary>
    void Render();

    /// <summary>
    /// Renders the element into an explicit <see cref="IRenderer"/>,
    /// overriding the component's configured renderer for this call only.
    /// </summary>
    /// <param name="renderer">The renderer to write output into.</param>
    void Render(IRenderer renderer);
}
