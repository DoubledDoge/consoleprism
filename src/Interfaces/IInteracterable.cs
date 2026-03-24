namespace ConsolePrism.Interfaces;

/// <summary>
/// Represents a component that accepts and responds to user input.
/// </summary>
public interface IInteractable
{
	/// <summary>
	/// Starts the interaction loop, blocking until the user completes input.
	/// </summary>
	/// <returns>
	/// The zero-based index of the user's selection, or <c>-1</c> if the interaction was cancelled.
	/// </returns>
	int Interact();
}
