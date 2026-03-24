namespace ConsolePrism.Core;

using Themes;

/// <summary>
/// Provides methods for writing semantically coloured text to the console,
/// resolving colours from the currently active <see cref="Theme"/>.
/// </summary>
public static class ColorWriter
{
	/// <summary>Writes an error message in the theme's error colour.</summary>
	/// <param name="message">The message to write.</param>
	public static void WriteError(string message) =>
		WriteColored(message, Theme.Current.Colors.Error);

	/// <summary>Writes an error message followed by a newline.</summary>
	/// <param name="message">The message to write.</param>
	public static void WriteErrorLine(string message) =>
		WriteColoredLine(message, Theme.Current.Colors.Error);

	/// <summary>Writes a success message in the theme's success colour.</summary>
	/// <param name="message">The message to write.</param>
	public static void WriteSuccess(string message) =>
		WriteColored(message, Theme.Current.Colors.Success);

	/// <summary>Writes a success message followed by a newline.</summary>
	/// <param name="message">The message to write.</param>
	public static void WriteSuccessLine(string message) =>
		WriteColoredLine(message, Theme.Current.Colors.Success);

	/// <summary>Writes a warning message in the theme's warning colour.</summary>
	/// <param name="message">The message to write.</param>
	public static void WriteWarning(string message) =>
		WriteColored(message, Theme.Current.Colors.Warning);

	/// <summary>Writes a warning message followed by a newline.</summary>
	/// <param name="message">The message to write.</param>
	public static void WriteWarningLine(string message) =>
		WriteColoredLine(message, Theme.Current.Colors.Warning);

	/// <summary>Writes an informational message in the theme's info colour.</summary>
	/// <param name="message">The message to write.</param>
	public static void WriteInfo(string message) =>
		WriteColored(message, Theme.Current.Colors.Info);

	/// <summary>Writes an informational message followed by a newline.</summary>
	/// <param name="message">The message to write.</param>
	public static void WriteInfoLine(string message) =>
		WriteColoredLine(message, Theme.Current.Colors.Info);

	/// <summary>Writes highlighted text in the theme's highlight colour.</summary>
	/// <param name="message">The message to write.</param>
	public static void WriteHighlight(string message) =>
		WriteColored(message, Theme.Current.Colors.Highlight);

	/// <summary>Writes highlighted text followed by a newline.</summary>
	/// <param name="message">The message to write.</param>
	public static void WriteHighlightLine(string message) =>
		WriteColoredLine(message, Theme.Current.Colors.Highlight);

	/// <summary>Writes text in an explicitly specified colour.</summary>
	/// <param name="message">The message to write.</param>
	/// <param name="color">The <see cref="ConsoleColor"/> to use.</param>
	public static void WriteColored(string message, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.Write(message);
		Console.ResetColor();
	}

	/// <summary>Writes text in an explicitly specified colour, followed by a newline.</summary>
	/// <param name="message">The message to write.</param>
	/// <param name="color">The <see cref="ConsoleColor"/> to use.</param>
	public static void WriteColoredLine(string message, ConsoleColor color)
	{
		Console.ForegroundColor = color;
		Console.WriteLine(message);
		Console.ResetColor();
	}
}
