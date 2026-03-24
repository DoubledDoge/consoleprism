namespace ConsolePrism.Themes;

/// <summary>
/// Represents a complete visual theme, combining a <see cref="ColorScheme"/>
/// and a <see cref="BorderStyle"/> into a single composable unit.
/// </summary>
public class Theme
{
	/// <summary>Gets or sets the colour scheme for this theme.</summary>
	public ColorScheme Colors { get; init; } = new();

	/// <summary>Gets or sets the border style for this theme.</summary>
	public BorderStyle Border { get; init; } = BorderStyle.Single;

	/// <summary>Gets the currently active global theme.</summary>
	public static Theme Current { get; private set; } = new();

	/// <summary>
	/// Applies the given theme as the active global theme.
	/// </summary>
	/// <param name="theme">The theme to apply.</param>
	public static void Apply(Theme theme)
	{
		ArgumentNullException.ThrowIfNull(theme);
		Current = theme;
	}
}
