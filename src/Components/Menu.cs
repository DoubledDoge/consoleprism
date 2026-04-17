namespace ConsolePrism.Components;

using Core;
using Core.Renderers;
using Interfaces;
using Themes;

/// <summary>
/// A UI component that renders interactive and static menu styles,
/// allowing users to select from a list of options.
/// </summary>
/// <param name="title">The menu title.</param>
/// <param name="renderer">The renderer to write output to.</param>
/// <param name="style">The visual style used to render the menu.</param>
/// <param name="footer">The menu footer. Defaults to null. (Interactive + Numbered)</param>
/// <param name="options">The selectable options.</param>
public sealed class Menu(
	string title,
	IRenderer renderer,
	MenuStyle style = MenuStyle.Interactive,
	string? footer = null,
	params string[] options
) : ComponentBase, IInteractable
{
	private IRenderer _renderer = renderer;
	private readonly List<string> _options = [.. options];
	private string Title { get; } = title;
	private string? Footer { get; } = footer;
	private MenuStyle Style { get; } = style;

	/// <summary>
	/// Initializes a new <see cref="Menu"/> with a title, style, optional footer, and options,
	/// using the default console renderer.
	/// </summary>
	/// <param name="title">The menu title.</param>
	/// <param name="style">The visual style used to render the menu.</param>
	/// <param name="footer">The menu footer. Defaults to null. (Interactive + Numbered)</param>
	/// <param name="options">The selectable options.</param>
	public Menu(string title, MenuStyle style, string? footer = null, params string[] options)
		: this(title, ConsoleRenderer.Instance, style, footer, options) { }

	/// <inheritdoc />
	protected override bool SupportsRendererSwap => true;

	/// <inheritdoc/>
	protected override IRenderer SwapRenderer(IRenderer? swapRenderer)
	{
		IRenderer previous = _renderer;
		if (swapRenderer is not null)
		{
			_renderer = swapRenderer;
		}

		return previous;
	}

	/// <summary>
	/// Renders the menu using the configured <see cref="Style"/>.
	/// </summary>
	public override void Render()
	{
		switch (Style)
		{
			case MenuStyle.Numbered:
				RenderNumbered();
				break;
			case MenuStyle.Bordered:
				RenderBordered();
				break;
			case MenuStyle.Interactive:
				break;
			default:
				RenderInteractive(0);
				break;
		}
	}

	/// <summary>
	/// Starts the interaction loop and returns the zero-based index of the
	/// selected option, or <c>-1</c> if the user pressed Escape.
	/// </summary>
	public int Interact() =>
		Style switch
		{
			MenuStyle.Numbered => InteractNumbered(),
			MenuStyle.Bordered => InteractBordered(),
			_ => InteractInteractive(),
		};

	#region Numbered
	private void RenderNumbered()
	{
		ColorScheme colors = ActiveTheme.Colors;

		_renderer.WriteColoredLine(Title, colors.MenuTitle);
		_renderer.WriteLine();

		for (int i = 0; i < _options.Count; i++)
		{
			_renderer.WriteColored($"[{i + 1}] ", colors.Primary);
			_renderer.WriteColoredLine(_options[i], colors.MenuOption);
		}

		_renderer.WriteColoredLine(Footer ?? string.Empty, colors.MenuTitle);
		_renderer.WriteLine();
	}

	private int InteractNumbered()
	{
		RenderNumbered();
		ColorScheme colors = ActiveTheme.Colors;

		while (true)
		{
			_renderer.WriteColored("Select an option: ", colors.Primary);
			string? input = Console.ReadLine();

			if (int.TryParse(input, out int choice) && choice >= 1 && choice <= _options.Count)
			{
				return choice - 1;
			}

			_renderer.WriteColoredLine("Invalid selection. Please try again.\n", colors.Error);
		}
	}
	#endregion

	#region Interactive
	private void RenderInteractive(int selectedIndex)
	{
		ColorScheme colors = ActiveTheme.Colors;

		_renderer.WriteColoredLine(Title, colors.MenuTitle);
		_renderer.WriteLine();

		for (int i = 0; i < _options.Count; i++)
		{
			if (i == selectedIndex)
			{
				_renderer.WriteColored("> ", colors.MenuSelected);
				_renderer.WriteColoredLine(_options[i], colors.MenuSelected);
			}
			else
			{
				_renderer.WriteColored("  ", colors.MenuOption);
				_renderer.WriteColoredLine(_options[i], colors.MenuOption);
			}
		}

		_renderer.WriteColoredLine(Footer ?? string.Empty, colors.MenuTitle);
		_renderer.WriteLine();
		_renderer.WriteColoredLine("Use up/down arrows to navigate, Enter to select", colors.Muted);
	}

	private int InteractInteractive()
	{
		ConsoleHelper.HideCursor();

		int selectedIndex = 0;
		int startRow = Console.CursorTop;
		ConsoleKey key;

		do
		{
			Console.SetCursorPosition(0, startRow);
			RenderInteractive(selectedIndex);
			key = Console.ReadKey(true).Key;

			selectedIndex = key switch
			{
				ConsoleKey.UpArrow => selectedIndex > 0 ? selectedIndex - 1 : _options.Count - 1,
				ConsoleKey.DownArrow => selectedIndex < _options.Count - 1 ? selectedIndex + 1 : 0,
				_ => selectedIndex,
			};
		} while (key != ConsoleKey.Enter);

		ConsoleHelper.ShowCursor();
		return selectedIndex;
	}

	#endregion

	#region Bordered
	private void RenderBordered(int selectedIndex = -1)
	{
		ColorScheme colors = ActiveTheme.Colors;
		BorderStyle border = ActiveTheme.Border;

		int maxWidth = Math.Max(Title.Length, _options.Max(o => o.Length)) + 8;
		int menuWidth = Math.Min(maxWidth, Console.WindowWidth - 4);

		// Top border
		_renderer.WriteColored(border.TopLeft.ToString(), colors.MenuBorder);
		_renderer.WriteColored(new string(border.Horizontal, menuWidth), colors.MenuBorder);
		_renderer.WriteColoredLine(border.TopRight.ToString(), colors.MenuBorder);

		// Title row
		_renderer.WriteColored(border.Vertical.ToString(), colors.MenuBorder);
		_renderer.WriteColored(ConsoleHelper.PadCenter(Title, menuWidth), colors.MenuTitle);
		_renderer.WriteColoredLine(border.Vertical.ToString(), colors.MenuBorder);

		// Title separator
		_renderer.WriteColored(border.TeeLeft.ToString(), colors.MenuBorder);
		_renderer.WriteColored(new string(border.Horizontal, menuWidth), colors.MenuBorder);
		_renderer.WriteColoredLine(border.TeeRight.ToString(), colors.MenuBorder);

		// Options
		for (int i = 0; i < _options.Count; i++)
		{
			string label = $"[{i + 1}] {_options[i]}";
			int padding = menuWidth - 2 - label.Length;

			_renderer.WriteColored(border.Vertical.ToString(), colors.MenuBorder);

			ConsoleColor optionColor = i == selectedIndex ? colors.MenuSelected : colors.MenuOption;

			_renderer.WriteColored($" {label} ", optionColor);
			_renderer.WriteColored(new string(' ', Math.Max(0, padding)), optionColor);
			_renderer.WriteColoredLine(border.Vertical.ToString(), colors.MenuBorder);
		}

		// Bottom border
		_renderer.WriteColored(border.BottomLeft.ToString(), colors.MenuBorder);
		_renderer.WriteColored(new string(border.Horizontal, menuWidth), colors.MenuBorder);
		_renderer.WriteColoredLine(border.BottomRight.ToString(), colors.MenuBorder);
	}

	private int InteractBordered()
	{
		RenderBordered();
		_renderer.WriteLine();

		ColorScheme colors = ActiveTheme.Colors;

		while (true)
		{
			_renderer.WriteColored("Select an option: ", colors.Primary);
			string? input = Console.ReadLine();

			if (int.TryParse(input, out int choice) && choice >= 1 && choice <= _options.Count)
			{
				return choice - 1;
			}

			_renderer.WriteColoredLine("Invalid selection. Please try again.\n", colors.Error);
		}
	}

	#endregion
}

/// <summary>Specifies the visual style used to render a <see cref="Menu"/>.</summary>
public enum MenuStyle
{
	/// <summary>A numbered list with keyboard input selection.</summary>
	Numbered,

	/// <summary>An arrow-key navigable list with live highlight.</summary>
	Interactive,

	/// <summary>A numbered list enclosed in a border box.</summary>
	Bordered,
}
