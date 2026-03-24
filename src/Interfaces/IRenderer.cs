namespace ConsolePrism.Interfaces;

using Core.Renderers;

/// <summary>
/// Defines the contract for a rendering backend used by ConsolePrism components.
/// </summary>
/// <remarks>
/// The default implementation writes directly to the console via
/// <see cref="ConsoleRenderer"/>. Alternative implementations such as
/// <see cref="StringRenderer"/> can capture output for testing or deferred rendering.
/// </remarks>
public interface IRenderer
{
	/// <summary>Writes text without a trailing newline.</summary>
	/// <param name="text">The text to write.</param>
	void Write(string text);

	/// <summary>Writes text followed by a newline.</summary>
	/// <param name="text">The text to write.</param>
	void WriteLine(string text);

	/// <summary>Writes a blank line.</summary>
	void WriteLine();

	/// <summary>
	/// Writes text in a specific <see cref="ConsoleColor"/>, then resets the color.
	/// </summary>
	/// <param name="text">The text to write.</param>
	/// <param name="color">The foreground color to apply.</param>
	void WriteColored(string text, ConsoleColor color);

	/// <summary>
	/// Writes text in a specific <see cref="ConsoleColor"/> followed by a newline,
	/// then resets the color.
	/// </summary>
	/// <param name="text">The text to write.</param>
	/// <param name="color">The foreground color to apply.</param>
	void WriteColoredLine(string text, ConsoleColor color);

	/// <summary>Sets the cursor position within the output target.</summary>
	/// <param name="x">The column position.</param>
	/// <param name="y">The row position.</param>
	void SetCursorPosition(int x, int y);

	/// <summary>Clears the output target.</summary>
	void Clear();
}
