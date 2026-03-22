namespace ConsolePrism.Themes;

/// <summary>
/// Defines the colour palette used across all ConsolePrism components. (Default Theme)
/// </summary>
public class ColorScheme
{
    /// <summary>Gets or sets the colour used for error messages.</summary>
    public ConsoleColor Error { get; init; } = ConsoleColor.Red;

    /// <summary>Gets or sets the colour used for success messages.</summary>
    public ConsoleColor Success { get; init; } = ConsoleColor.Green;

    /// <summary>Gets or sets the colour used for warning messages.</summary>
    public ConsoleColor Warning { get; init; } = ConsoleColor.Yellow;

    /// <summary>Gets or sets the colour used for informational messages.</summary>
    public ConsoleColor Info { get; init; } = ConsoleColor.Cyan;

    /// <summary>Gets or sets the colour used for highlighted text.</summary>
    public ConsoleColor Highlight { get; init; } = ConsoleColor.Magenta;

    /// <summary>Gets or sets the primary UI colour.</summary>
    public ConsoleColor Primary { get; init; } = ConsoleColor.Blue;

    /// <summary>Gets or sets the colour used for muted or secondary text.</summary>
    public ConsoleColor Muted { get; init; } = ConsoleColor.DarkGray;

    /// <summary>Gets or sets the colour used for menu titles.</summary>
    public ConsoleColor MenuTitle { get; init; } = ConsoleColor.Yellow;

    /// <summary>Gets or sets the colour used for unselected menu options.</summary>
    public ConsoleColor MenuOption { get; init; } = ConsoleColor.White;

    /// <summary>Gets or sets the colour used for the selected menu option.</summary>
    public ConsoleColor MenuSelected { get; init; } = ConsoleColor.Green;

    /// <summary>Gets or sets the colour used for menu borders.</summary>
    public ConsoleColor MenuBorder { get; init; } = ConsoleColor.DarkGray;

    /// <summary>Gets or sets the colour used for table headers.</summary>
    public ConsoleColor TableHeader { get; init; } = ConsoleColor.Cyan;

    /// <summary>Gets or sets the colour used for table borders.</summary>
    public ConsoleColor TableBorder { get; init; } = ConsoleColor.DarkGray;

    /// <summary>Gets or sets the colour used for table data cells.</summary>
    public ConsoleColor TableData { get; init; } = ConsoleColor.White;

    /// <summary>Gets or sets the colour used for the completed portion of a progress bar.</summary>
    public ConsoleColor ProgressBarComplete { get; init; } = ConsoleColor.Green;

    /// <summary>Gets or sets the colour used for the incomplete portion of a progress bar.</summary>
    public ConsoleColor ProgressBarIncomplete { get; init; } = ConsoleColor.DarkGray;

    /// <summary>Gets or sets the colour used for progress bar labels.</summary>
    public ConsoleColor ProgressBarText { get; init; } = ConsoleColor.White;
}
