namespace ConsolePrism.Core.Renderers;

using Interfaces;

/// <summary>
/// The default <see cref="IRenderer"/> implementation that writes
/// directly to <see cref="Console"/> standard output.
/// </summary>
public sealed class ConsoleRenderer : IRenderer
{
	/// <summary>
	/// Gets the shared singleton instance of <see cref="ConsoleRenderer"/>.
	/// </summary>
	public static readonly ConsoleRenderer Instance = new();

	private ConsoleRenderer() { }

	/// <inheritdoc/>
	public void Write(string text) => Console.Write(text);

	/// <inheritdoc/>
	public void WriteLine(string text) => Console.WriteLine(text);

	/// <inheritdoc/>
	public void WriteLine() => Console.WriteLine();

	/// <inheritdoc/>
	public void WriteColored(string text, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.Write(text);
		Console.ResetColor();
	}

	/// <inheritdoc/>
	public void WriteColoredLine(string text, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.WriteLine(text);
		Console.ResetColor();
	}

	/// <inheritdoc/>
	public void SetCursorPosition(int x, int y) => Console.SetCursorPosition(x, y);

	/// <inheritdoc/>
	public void Clear() => Console.Clear();
}
