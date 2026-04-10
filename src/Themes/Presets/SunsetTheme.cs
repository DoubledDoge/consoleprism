namespace ConsolePrism.Themes.Presets;

/// <summary>
/// A theme inspired by warm sunset hues, featuring rich magenta,
/// golden yellows, and soft whites to evoke a twilight atmosphere.
/// </summary>
public static class SunsetTheme
{
	/// <summary>Gets the sunset-inspired theme instance.</summary>
	public static readonly Theme Instance = new()
	{
		Colors = new ColorScheme
		{
			Error = ConsoleColor.Red,
			Success = ConsoleColor.Magenta,
			Warning = ConsoleColor.Yellow,
			Info = ConsoleColor.DarkYellow,
			Highlight = ConsoleColor.White,

			Primary = ConsoleColor.Magenta,
			Muted = ConsoleColor.DarkYellow,

			MenuTitle = ConsoleColor.Magenta,
			MenuOption = ConsoleColor.DarkYellow,
			MenuSelected = ConsoleColor.White,
			MenuBorder = ConsoleColor.DarkYellow,

			TableHeader = ConsoleColor.Magenta,
			TableBorder = ConsoleColor.DarkYellow,
			TableData = ConsoleColor.DarkYellow,

			ProgressBarComplete = ConsoleColor.Magenta,
			ProgressBarIncomplete = ConsoleColor.DarkYellow,
			ProgressBarText = ConsoleColor.Magenta,
		},
		Border = BorderStyle.Rounded,
	};
}
