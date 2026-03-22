namespace ConsolePrism.Themes.Presets;

/// <summary>
/// A retro terminal theme with amber tones and ASCII borders,
/// evoking classic CRT monitors.
/// </summary>
public static class RetroTheme
{
    /// <summary>Gets the retro terminal theme instance.</summary>
    public static readonly Theme Instance = new()
    {
        Colors = new ColorScheme
        {
            Error = ConsoleColor.Red,
            Success = ConsoleColor.Yellow,
            Warning = ConsoleColor.DarkYellow,
            Info = ConsoleColor.Yellow,
            Highlight = ConsoleColor.White,

            Primary = ConsoleColor.Yellow,
            Muted = ConsoleColor.DarkYellow,

            MenuTitle = ConsoleColor.Yellow,
            MenuOption = ConsoleColor.DarkYellow,
            MenuSelected = ConsoleColor.White,
            MenuBorder = ConsoleColor.DarkYellow,

            TableHeader = ConsoleColor.Yellow,
            TableBorder = ConsoleColor.DarkYellow,
            TableData = ConsoleColor.DarkYellow,

            ProgressBarComplete = ConsoleColor.Yellow,
            ProgressBarIncomplete = ConsoleColor.DarkYellow,
            ProgressBarText = ConsoleColor.Yellow,
        },
        Border = BorderStyle.Ascii,
    };
}
