namespace ConsolePrism.Components;

using Interfaces;
using Themes;

/// <summary>
/// Abstract base class for all ConsolePrism components, providing shared
/// theme resolution and common rendering infrastructure.
/// </summary>
public abstract class ComponentBase : IComponent
{
	/// <inheritdoc/>
	public Theme? Theme { get; set; }

	/// <summary>
	/// Resolves the active theme for this component — returns the local
	/// override if set, otherwise falls back to <see cref="Theme.Current"/>.
	/// </summary>
	protected Theme ActiveTheme => Theme ?? Theme.Current;

	/// <summary>
	/// Gets a value indicating whether this component supports renderer swapping
	/// via <see cref="Render(IRenderer)"/>.
	/// </summary>
	/// <remarks>
	/// Components that hold a mutable <see cref="IRenderer"/> field should override
	/// this to return <see langword="true"/> and implement <see cref="SwapRenderer"/>.
	/// Components using primary constructor renderer parameters return
	/// <see langword="false"/> and will ignore renderer swap attempts.
	/// </remarks>
	protected virtual bool SupportsRendererSwap => false;

	/// <inheritdoc/>
	public abstract void Render();

	/// <inheritdoc/>
	public virtual void Render(IRenderer renderer)
	{
		if (!SupportsRendererSwap)
		{
			Render();
			return;
		}

		IRenderer? previous = SwapRenderer(renderer);
		Render();
		SwapRenderer(previous);
	}

	/// <summary>
	/// Swaps the component's internal renderer, returning the previous one.
	/// Only called when <see cref="SupportsRendererSwap"/> is <see langword="true"/>.
	/// </summary>
	/// <param name="swapRenderer">The renderer to swap in.</param>
	/// <returns>The renderer that was active before the swap.</returns>
	protected virtual IRenderer? SwapRenderer(IRenderer? swapRenderer) => null;
}
