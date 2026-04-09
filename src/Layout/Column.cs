namespace ConsolePrism.Layout;

using Components;
using Core.Renderers;
using Interfaces;

/// <summary>
/// A horizontal layout container that renders child components side by side,
/// each allocated an equal share of the available console width.
/// </summary>
/// <remarks>
/// Each child is rendered into a <see cref="StringRenderer"/> buffer first,
/// then columns are interleaved line by line to produce the final output.
/// This means child components do not need to be aware of column layout at all.
/// </remarks>
/// <param name="renderer">The renderer to write output to.</param>
/// <param name="gap">The number of space characters inserted between columns.</param>
public sealed class Column(IRenderer renderer, int gap) : ComponentBase
{
	private readonly List<IRenderable> _children = [];
	private readonly List<StringRenderer> _bufferRenderers = [];
	private int Gap { get; } = gap;

	/// <summary>
	/// Initializes a new <see cref="Column"/> using the default console renderer.
	/// </summary>
	/// <param name="gap">The number of space characters inserted between columns.</param>
	public Column(int gap)
		: this(ConsoleRenderer.Instance, gap) { }

	/// <summary>
	/// Adds a child component to this column layout.
	/// </summary>
	/// <param name="child">The component to add.</param>
	/// <returns>This <see cref="Column"/> instance, enabling a fluent chain.</returns>
	public Column Add(IRenderable child)
	{
		_children.Add(child);
		return this;
	}

	/// <inheritdoc/>
	protected override IRenderer? SwapRenderer(IRenderer? swapRenderer) => null;

	/// <inheritdoc/>
	public override void Render()
	{
		if (_children.Count == 0)
		{
			return;
		}

		// Grow the renderer pool to match child count if needed
		while (_bufferRenderers.Count < _children.Count)
		{
			_bufferRenderers.Add(new StringRenderer());
		}

		int totalWidth = Console.WindowWidth;
		int gapTotal = Gap * (_children.Count - 1);
		int columnWidth = (totalWidth - gapTotal) / _children.Count;
		string gapString = new(' ', Gap);

		// Render each child into its own reusable string buffer
		List<string[]> buffers =
		[
			.. _children.Select(
				(child, i) =>
				{
					_bufferRenderers[i].Reset();

					if (child is ComponentBase { Theme: null } cb && Theme is not null)
					{
						cb.Theme = Theme;
					}

					child.Render(_bufferRenderers[i]);
					return _bufferRenderers[i]
						.Output.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
				}
			),
		];

		int maxLines = buffers.Max(b => b.Length);

		for (int line = 0; line < maxLines; line++)
		{
			for (int col = 0; col < buffers.Count; col++)
			{
				string content = line < buffers[col].Length ? buffers[col][line] : string.Empty;

				content =
					content.Length > columnWidth
						? content[..columnWidth]
						: content.PadRight(columnWidth);

				renderer.Write(content);

				if (col < buffers.Count - 1)
				{
					renderer.Write(gapString);
				}
			}

			renderer.WriteLine();
		}
	}
}
