using ConsolePrism.Core;

namespace ConsolePrism.Layout;

using Components;
using Core.Renderers;
using Interfaces;

/// <summary>
/// A scrollable content region that renders a fixed-height window into
/// a larger set of <see cref="IRenderable"/> children, controllable via
/// <see cref="ScrollUp"/> and <see cref="ScrollDown"/>.
/// </summary>
/// <param name="renderer">The renderer to write output to.</param>
/// <param name="height">The number of visible lines in the viewport.</param>
/// <param name="children">The child components to add.</param>
public sealed class Viewport(IRenderer renderer, int height = 20, params IRenderable[] children)
	: ComponentBase,
		IInteractable
{
	private IRenderer _renderer = renderer;
	private readonly List<IRenderable> _children = [.. children];
	private int Height { get; } = height;

	/// <summary>
	/// Gets the current scroll offset (zero-based line index of the top visible line).
	/// </summary>
	// ReSharper disable once MemberCanBePrivate.Global
	public int ScrollOffset { get; private set; }

	/// <summary>
	/// Initializes a new <see cref="Viewport"/> using the default console renderer.
	/// </summary>
	/// <param name="height">The number of visible lines in the viewport.</param>
	/// <param name="children">The child components to add.</param>
	public Viewport(int height = 20, params IRenderable[] children)
		: this(ConsoleRenderer.Instance, height, children) { }

	/// <summary>Adds a child component to the viewport's content.</summary>
	/// <param name="child">The component to add.</param>
	/// <returns>This <see cref="Viewport"/> instance, enabling a fluent chain.</returns>
	public Viewport Add(IRenderable child)
	{
		_children.Add(child);
		return this;
	}

	/// <summary>Scrolls the viewport up by the specified number of lines.</summary>
	/// <param name="lines">Number of lines to scroll. Defaults to 1.</param>
	public void ScrollUp(int lines = 1)
	{
		if (lines <= 0)
		{
			return;
		}

		ScrollOffset = Math.Max(0, ScrollOffset - lines);
	}

	/// <summary>Scrolls the viewport down by the specified number of lines.</summary>
	/// <param name="lines">Number of lines to scroll. Defaults to 1.</param>
	public void ScrollDown(int lines = 1)
	{
		if (lines <= 0)
		{
			return;
		}

		if (int.MaxValue - ScrollOffset < lines)
		{
			ScrollOffset = int.MaxValue;
		}
		else
		{
			ScrollOffset += lines;
		}
	}

	/// <summary>Resets the scroll position to the top.</summary>
	public void ScrollToTop() => ScrollOffset = 0;

	/// <inheritdoc/>
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

	/// <inheritdoc/>
	public override void Render()
	{
		StringRenderer sr = new();

		foreach (IRenderable child in _children)
		{
			child.Render(sr);
		}

		string[] allLines = sr.Output.Split(
			Environment.NewLine,
			StringSplitOptions.RemoveEmptyEntries
		);

		int maxOffset = Math.Max(0, allLines.Length - Height);
		ScrollOffset = Math.Min(ScrollOffset, maxOffset);

		foreach (string line in allLines.Skip(ScrollOffset).Take(Height))
		{
			_renderer.WriteLine(line);
		}
	}

	/// <summary>
	/// Starts the interaction loop and scrolls up or down based on if the up arrow or down arrow was pressed.
	/// </summary>
	/// <returns></returns>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public int Interact()
	{
		ConsoleHelper.HideCursor();
		int startRow = Console.CursorTop;
		ConsoleKey key;

		do
		{
			Console.SetCursorPosition(0, startRow);
			Render();

			key = Console.ReadKey(true).Key;

			if (key == ConsoleKey.UpArrow)
			{
				ScrollUp();
			}

			if (key == ConsoleKey.DownArrow)
			{
				ScrollDown();
			}
		} while (key != ConsoleKey.Escape && key != ConsoleKey.Enter);

		ConsoleHelper.ShowCursor();
		return ScrollOffset;
	}
}
