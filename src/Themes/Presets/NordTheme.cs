namespace ConsolePrism.Themes.Presets;

/// <summary>
/// A theme inspired by the Nord colour palette with cool, arctic blues and muted tones.
/// </summary>
public static class NordTheme
{
    /// <summary>Gets the Nord-inspired theme instance.</summary>
    public static readonly Theme Instance = new()
    {
        Colors = new ColorScheme
        {
            Error = ConsoleColor.Red,
            Success = ConsoleColor.Cyan,
            Warning = ConsoleColor.Yellow,
            Info = ConsoleColor.Blue,
            Highlight = ConsoleColor.Magenta,

            Primary = ConsoleColor.Cyan,
            Muted = ConsoleColor.DarkBlue,

            MenuTitle = ConsoleColor.Cyan,
            MenuOption = ConsoleColor.White,
            MenuSelected = ConsoleColor.Blue,
            MenuBorder = ConsoleColor.DarkBlue,

            TableHeader = ConsoleColor.Cyan,
            TableBorder = ConsoleColor.DarkBlue,
            TableData = ConsoleColor.White,

            ProgressBarComplete = ConsoleColor.Cyan,
            ProgressBarIncomplete = ConsoleColor.DarkBlue,
            ProgressBarText = ConsoleColor.White,
        },
        Border = BorderStyle.Rounded,
    };
}
