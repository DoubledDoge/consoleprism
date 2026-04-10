namespace ConsolePrism.Themes.Presets;

/// <summary>
/// A theme inspired by the Solarized colour palette, using warm ambers
/// and earthy tones with cool accent colours.
/// </summary>
/// <remarks>
/// Best experienced on a terminal with a dark brown or dark gray background.
/// </remarks>
public static class SolarizedTheme
{
	/// <summary>Gets the Solarized-inspired theme instance.</summary>
	public static readonly Theme Instance = new()
	{
		Colors = new ColorScheme
		{
			Error = ConsoleColor.Red,
			Success = ConsoleColor.DarkCyan,
			Warning = ConsoleColor.Yellow,
			Info = ConsoleColor.Cyan,
			Highlight = ConsoleColor.DarkYellow,

			Primary = ConsoleColor.DarkYellow,
			Muted = ConsoleColor.DarkGray,

			MenuTitle = ConsoleColor.DarkYellow,
			MenuOption = ConsoleColor.Yellow,
			MenuSelected = ConsoleColor.Cyan,
			MenuBorder = ConsoleColor.DarkGray,

			TableHeader = ConsoleColor.DarkYellow,
			TableBorder = ConsoleColor.DarkGray,
			TableData = ConsoleColor.Yellow,

			ProgressBarComplete = ConsoleColor.DarkCyan,
			ProgressBarIncomplete = ConsoleColor.DarkGray,
			ProgressBarText = ConsoleColor.Yellow,
		},
		Border = BorderStyle.Rounded,
	};
}
