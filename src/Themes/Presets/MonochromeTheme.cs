namespace ConsolePrism.Themes.Presets;

/// <summary>
/// A monochrome theme using only greys and white, suitable for terminals
/// with limited colour support or accessibility requirements.
/// </summary>
public static class MonochromeTheme
{
	/// <summary>Gets the monochrome theme instance.</summary>
	public static readonly Theme Instance = new()
	{
		Colors = new ColorScheme
		{
			Error = ConsoleColor.White,
			Success = ConsoleColor.White,
			Warning = ConsoleColor.Gray,
			Info = ConsoleColor.Gray,
			Highlight = ConsoleColor.White,

			Primary = ConsoleColor.White,
			Muted = ConsoleColor.DarkGray,

			MenuTitle = ConsoleColor.White,
			MenuOption = ConsoleColor.Gray,
			MenuSelected = ConsoleColor.White,
			MenuBorder = ConsoleColor.DarkGray,

			TableHeader = ConsoleColor.White,
			TableBorder = ConsoleColor.DarkGray,
			TableData = ConsoleColor.Gray,

			ProgressBarComplete = ConsoleColor.White,
			ProgressBarIncomplete = ConsoleColor.DarkGray,
			ProgressBarText = ConsoleColor.Gray,
		},
		Border = BorderStyle.Single,
	};
}
