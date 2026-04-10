namespace ConsolePrism.Themes.Presets;

/// <summary>
/// A soft pastel theme using muted, light tones for a gentle visual experience.
/// </summary>
/// <remarks>
/// Best experienced on a terminal with a dark background. Note that
/// <see cref="ConsoleColor"/> has no native pastel values, so this theme
/// approximates the aesthetic using the lightest available console colours.
/// </remarks>
public static class PastelTheme
{
	/// <summary>Gets the pastel theme instance.</summary>
	public static readonly Theme Instance = new()
	{
		Colors = new ColorScheme
		{
			Error = ConsoleColor.Red,
			Success = ConsoleColor.DarkCyan,
			Warning = ConsoleColor.DarkYellow,
			Info = ConsoleColor.DarkMagenta,
			Highlight = ConsoleColor.Magenta,

			Primary = ConsoleColor.DarkCyan,
			Muted = ConsoleColor.DarkGray,

			MenuTitle = ConsoleColor.Magenta,
			MenuOption = ConsoleColor.Gray,
			MenuSelected = ConsoleColor.DarkCyan,
			MenuBorder = ConsoleColor.DarkGray,

			TableHeader = ConsoleColor.Magenta,
			TableBorder = ConsoleColor.DarkGray,
			TableData = ConsoleColor.Gray,

			ProgressBarComplete = ConsoleColor.DarkCyan,
			ProgressBarIncomplete = ConsoleColor.DarkGray,
			ProgressBarText = ConsoleColor.Gray,
		},
		Border = BorderStyle.Double,
	};
}
