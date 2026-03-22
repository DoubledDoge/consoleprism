namespace ConsolePrism.Core;

/// <summary>
/// Provides low-level console utilities for cursor control, text positioning,
/// window queries, and line drawing.
/// </summary>
public static class ConsoleHelper
{
    /// <summary>Clears the content of a specific console line.</summary>
    /// <param name="line">The zero-based row index to clear.</param>
    public static void ClearLine(int line)
    {
        Console.SetCursorPosition(0, line);
        Console.Write(new string(' ', Console.WindowWidth));
        Console.SetCursorPosition(0, line);
    }

    /// <summary>Clears the line the cursor is currently on.</summary>
    public static void ClearCurrentLine() => ClearLine(Console.CursorTop);

    /// <summary>Moves the cursor to the specified console coordinates.</summary>
    /// <param name="x">The column position.</param>
    /// <param name="y">The row position.</param>
    public static void MoveCursor(int x, int y) => Console.SetCursorPosition(x, y);

    /// <summary>Hides the console cursor.</summary>
    public static void HideCursor() => Console.CursorVisible = false;

    /// <summary>Shows the console cursor.</summary>
    public static void ShowCursor() => Console.CursorVisible = true;

    /// <summary>Returns the current cursor position as an (X, Y) tuple.</summary>
    public static (int X, int Y) GetCursorPosition() => (Console.CursorLeft, Console.CursorTop);

    /// <summary>Returns the current console window dimensions as a (Width, Height) tuple.</summary>
    public static (int Width, int Height) GetWindowSize() =>
        (Console.WindowWidth, Console.WindowHeight);

    /// <summary>
    /// Writes text centered horizontally within the console window.
    /// </summary>
    /// <param name="text">The text to write.</param>
    /// <param name="row">
    /// The row to write on. Defaults to the current cursor row if not specified.
    /// </param>
    public static void WriteCentered(string text, int? row = null)
    {
        int x = Math.Max(0, (Console.WindowWidth - text.Length) / 2);
        int y = row ?? Console.CursorTop;

        Console.SetCursorPosition(x, y);
        Console.Write(text);
    }

    /// <summary>
    /// Writes text aligned to the right edge of the console window.
    /// </summary>
    /// <param name="text">The text to write.</param>
    /// <param name="row">The row to write on. Defaults to the current cursor row.</param>
    /// <param name="padding">Additional right-side padding in characters.</param>
    public static void WriteRight(string text, int? row = null, int padding = 0)
    {
        int x = Math.Max(0, Console.WindowWidth - text.Length - padding);
        int y = row ?? Console.CursorTop;

        Console.SetCursorPosition(x, y);
        Console.Write(text);
    }

    /// <summary>Writes text at an explicit console coordinate.</summary>
    /// <param name="text">The text to write.</param>
    /// <param name="x">The column position.</param>
    /// <param name="y">The row position.</param>
    public static void WriteAt(string text, int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(text);
    }

    /// <summary>Writes a specified number of empty lines to the console.</summary>
    /// <param name="count">The number of blank lines to write.</param>
    public static void WriteEmptyLines(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine();
        }
    }

    /// <summary>
    /// Returns a string padded symmetrically to centre the text within a given width.
    /// </summary>
    /// <param name="text">The text to centre.</param>
    /// <param name="totalWidth">The total width of the resulting string.</param>
    /// <param name="paddingChar">The character to pad with. Defaults to a space.</param>
    /// <returns>The padded string, or the original text if it exceeds <paramref name="totalWidth"/>.</returns>
    public static string PadCenter(string text, int totalWidth, char paddingChar = ' ')
    {
        if (text.Length >= totalWidth)
        {
            return text;
        }

        int totalPadding = totalWidth - text.Length;
        int leftPadding = totalPadding / 2;
        int rightPadding = totalPadding - leftPadding;

        return new string(paddingChar, leftPadding) + text + new string(paddingChar, rightPadding);
    }

    /// <summary>
    /// Draws a horizontal line across the full width of the console window.
    /// </summary>
    /// <param name="character">The character to draw with. Defaults to <c>-</c>.</param>
    public static void DrawHorizontalLine(char character = '-') =>
        Console.WriteLine(new string(character, Console.WindowWidth));

    /// <summary>
    /// Draws a horizontal line across the full console width at a specific row.
    /// </summary>
    /// <param name="y">The row to draw on.</param>
    /// <param name="character">The character to draw with. Defaults to <c>-</c>.</param>
    public static void DrawHorizontalLineAt(int y, char character = '-')
    {
        Console.SetCursorPosition(0, y);
        Console.Write(new string(character, Console.WindowWidth));
    }
}
