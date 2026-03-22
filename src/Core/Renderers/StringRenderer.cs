using System.Text;

namespace ConsolePrism.Core.Renderers;

using Interfaces;

/// <summary>
/// An <see cref="IRenderer"/> implementation that captures all output
/// into an in-memory string buffer instead of writing to the console.
/// </summary>
/// <remarks>
/// Primarily intended for unit testing where it passes a <see cref="StringRenderer"/>
/// into any component to capture and assert against its rendered output
/// without requiring a real terminal.
/// </remarks>
public sealed class StringRenderer : IRenderer
{
    private readonly StringBuilder _buffer = new();

    /// <summary>
    /// Gets the full captured output as a single string.
    /// </summary>
    public string Output => _buffer.ToString();

    /// <summary>Clears the captured output buffer.</summary>
    public void Reset() => _buffer.Clear();

    /// <inheritdoc/>
    public void Write(string text) => _buffer.Append(text);

    /// <inheritdoc/>
    public void WriteLine(string text) => _buffer.AppendLine(text);

    /// <inheritdoc/>
    public void WriteLine() => _buffer.AppendLine();

    /// <inheritdoc/>
    public void WriteColored(string text, ConsoleColor color) => _buffer.Append(text);

    /// <inheritdoc/>
    public void WriteColoredLine(string text, ConsoleColor color) => _buffer.AppendLine(text);

    /// <inheritdoc/>
    public void SetCursorPosition(int x, int y) { }

    /// <inheritdoc/>
    public void Clear() => _buffer.Clear();
}
