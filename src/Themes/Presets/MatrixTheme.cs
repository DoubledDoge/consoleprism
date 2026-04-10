namespace ConsolePrism.Themes.Presets;

/// <summary>
/// A theme inspired by the Matrix film aesthetic, using cascading greens
/// against a dark background.
/// </summary>
/// <remarks>
/// Best experienced on a terminal with a black background.
/// </remarks>
public static class MatrixTheme
{
	/// <summary>Gets the Matrix-inspired theme instance.</summary>
	public static readonly Theme Instance = new()
	{
		Colors = new ColorScheme
		{
			Error = ConsoleColor.Red,
			Success = ConsoleColor.Green,
			Warning = ConsoleColor.DarkGreen,
			Info = ConsoleColor.DarkGreen,
			Highlight = ConsoleColor.White,

			Primary = ConsoleColor.Green,
			Muted = ConsoleColor.DarkGreen,

			MenuTitle = ConsoleColor.Green,
			MenuOption = ConsoleColor.DarkGreen,
			MenuSelected = ConsoleColor.White,
			MenuBorder = ConsoleColor.DarkGreen,

			TableHeader = ConsoleColor.Green,
			TableBorder = ConsoleColor.DarkGreen,
			TableData = ConsoleColor.DarkGreen,

			ProgressBarComplete = ConsoleColor.Green,
			ProgressBarIncomplete = ConsoleColor.DarkGreen,
			ProgressBarText = ConsoleColor.Green,
		},
		Border = BorderStyle.Single,
	};
}
