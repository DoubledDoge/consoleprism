namespace ConsolePrism.Components;

/// <summary>
/// Represents a single cell within a <see cref="Table"/>, supporting custom text and optional colour styling.
/// </summary>
/// <param name="Text">The string content to display in the cell.</param>
/// <param name="Color">The optional foreground colour for this specific cell. If <see langword="null"/>, the table's default data colour is used.</param>
public readonly record struct TableCell(string Text, ConsoleColor? Color = null)
{
	/// <summary>
	/// Implicitly converts a string into a <see cref="TableCell"/> with no specific colour applied.
	/// </summary>
	/// <param name="text">The string content to wrap.</param>
	/// <returns>A new <see cref="TableCell"/> containing the provided text.</returns>
	public static implicit operator TableCell(string text) => new(text);

	/// <summary>
	/// Creates a new <see cref="TableCell"/> from the provided string.
	/// </summary>
	/// <param name="text">The string content to wrap.</param>
	/// <returns>A new <see cref="TableCell"/> containing the provided text.</returns>
	public static TableCell FromString(string text) => new(text);
}
