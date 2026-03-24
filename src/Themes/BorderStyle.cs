namespace ConsolePrism.Themes;

/// <summary>
/// Defines the border characters used when rendering bordered components.
/// </summary>
public class BorderStyle
{
	/// <summary>Gets or sets the top-left corner character.</summary>
	public char TopLeft { get; init; } = '┌';

	/// <summary>Gets or sets the top-right corner character.</summary>
	public char TopRight { get; init; } = '┐';

	/// <summary>Gets or sets the bottom-left corner character.</summary>
	public char BottomLeft { get; init; } = '└';

	/// <summary>Gets or sets the bottom-right corner character.</summary>
	public char BottomRight { get; init; } = '┘';

	/// <summary>Gets or sets the horizontal line character.</summary>
	public char Horizontal { get; init; } = '─';

	/// <summary>Gets or sets the vertical line character.</summary>
	public char Vertical { get; init; } = '│';

	/// <summary>Gets or sets the cross/intersection character.</summary>
	public char Cross { get; init; } = '┼';

	/// <summary>Gets or sets the left tee character.</summary>
	public char TeeLeft { get; init; } = '├';

	/// <summary>Gets or sets the right tee character.</summary>
	public char TeeRight { get; init; } = '┤';

	/// <summary>Gets or sets the top tee character.</summary>
	public char TeeTop { get; init; } = '┬';

	/// <summary>Gets or sets the bottom tee character.</summary>
	public char TeeBottom { get; init; } = '┴';

	/// <summary>A classic single-line box drawing border (default).</summary>
	public static readonly BorderStyle Single = new();

	/// <summary>A double-line box drawing border.</summary>
	public static readonly BorderStyle Double = new()
	{
		TopLeft = '╔',
		TopRight = '╗',
		BottomLeft = '╚',
		BottomRight = '╝',
		Horizontal = '═',
		Vertical = '║',
		Cross = '╬',
		TeeLeft = '╠',
		TeeRight = '╣',
		TeeTop = '╦',
		TeeBottom = '╩',
	};

	/// <summary>A rounded corner border using single-line sides.</summary>
	public static readonly BorderStyle Rounded = new()
	{
		TopLeft = '╭',
		TopRight = '╮',
		BottomLeft = '╰',
		BottomRight = '╯',
		Horizontal = '─',
		Vertical = '│',
		Cross = '┼',
		TeeLeft = '├',
		TeeRight = '┤',
		TeeTop = '┬',
		TeeBottom = '┴',
	};

	/// <summary>A minimal ASCII border for environments without Unicode support.</summary>
	public static readonly BorderStyle Ascii = new()
	{
		TopLeft = '+',
		TopRight = '+',
		BottomLeft = '+',
		BottomRight = '+',
		Horizontal = '-',
		Vertical = '|',
		Cross = '+',
		TeeLeft = '+',
		TeeRight = '+',
		TeeTop = '+',
		TeeBottom = '+',
	};
}
