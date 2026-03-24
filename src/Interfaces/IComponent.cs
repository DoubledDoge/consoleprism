using ConsolePrism.Themes;

namespace ConsolePrism.Interfaces;

/// <summary>
/// Base interface for all ConsolePrism UI components.
/// Combines rendering capability with optional theme overriding.
/// </summary>
public interface IComponent : IRenderable
{
	/// <summary>
	/// Gets or sets an optional theme override for this component.
	/// When null, the component uses <see cref="Theme.Current"/>.
	/// </summary>
	Theme? Theme { get; set; }
}
