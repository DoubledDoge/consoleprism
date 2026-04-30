using System.Text;

namespace ConsolePrism.Components;

using Core.Renderers;
using Interfaces;
using Themes;

/// <summary>
/// A UI component that renders a bordered, auto-sizing data table with
/// text wrapping support.
/// </summary>
/// <remarks>
/// Initializes a new <see cref="Table"/> with an explicit renderer.
/// </remarks>
/// <param name="headers">Column header labels.</param>
/// <param name="data">Cell data, or <see langword="null"/> for a headers-only table.</param>
/// <param name="renderer">The renderer to write output to.</param>
/// <param name="columnWidths">Optional explicit column widths.</param>
public sealed class Table(
	string[] headers,
	TableCell[][]? data,
	IRenderer renderer,
	int[]? columnWidths = null
) : ComponentBase
{
	/// <summary>
	/// Initializes a new <see cref="Table"/> with the default console renderer.
	/// </summary>
	/// <param name="headers">Column header labels.</param>
	/// <param name="data">
	/// A jagged array of cell values, where the first dimension is rows
	/// and the second is columns. May be <see langword="null"/> to render headers only.
	/// </param>
	/// <param name="columnWidths">
	/// Optional explicit column widths. When <see langword="null"/>, widths are
	/// calculated automatically from content.
	/// </param>
	public Table(string[] headers, TableCell[][]? data, int[]? columnWidths = null)
		: this(headers, data, ConsoleRenderer.Instance, columnWidths) { }

	/// <summary>
	/// Initializes a new <see cref="Table"/> with the default console renderer using a string array.
	/// </summary>
	/// <param name="headers">Column header labels.</param>
	/// <param name="data">
	/// A jagged array of cell values, where the first dimension is rows
	/// and the second is columns. May be <see langword="null"/> to render headers only.
	/// </param>
	/// <param name="columnWidths">
	/// Optional explicit column widths. When <see langword="null"/>, widths are
	/// calculated automatically from content.
	/// </param>
	public Table(string[] headers, string[][]? data, int[]? columnWidths = null)
		: this(headers, ConvertToStringCells(data), ConsoleRenderer.Instance, columnWidths) { }

	/// <inheritdoc />
	protected override bool SupportsRendererSwap => false;

	/// <inheritdoc />
	protected override IRenderer? SwapRenderer(IRenderer? swapRenderer) => null;

	private static TableCell[][]? ConvertToStringCells(string[][]? data) =>
		data?.Select(row => row.Select(cell => (TableCell)cell).ToArray()).ToArray();

	/// <inheritdoc/>
	public override void Render()
	{
		if (headers.Length == 0)
		{
			renderer.WriteColoredLine(
				"Table requires at least one header.",
				ActiveTheme.Colors.Error
			);
			return;
		}

		int[] widths = columnWidths ?? CalculateColumnWidths();

		DrawTopBorder(widths);
		DrawHeaderRow(widths);
		DrawSeparator(widths);

		if (data is not null && data.GetLength(0) > 0)
		{
			DrawDataRows(widths);
		}

		DrawBottomBorder(widths);
	}

	private int[] CalculateColumnWidths()
	{
		int columnCount = headers.Length;
		int[] widths = new int[columnCount];

		for (int col = 0; col < columnCount; col++)
		{
			widths[col] = headers[col].Length;
		}

		if (data is not null)
		{
			int rowCount = data.Length;
			for (int row = 0; row < rowCount; row++)
			{
				for (int col = 0; col < columnCount; col++)
				{
					widths[col] = Math.Max(widths[col], data[row][col].Text.Length);
				}
			}
		}

		for (int i = 0; i < widths.Length; i++)
		{
			widths[i] += 2;
		}

		return widths;
	}

	private void DrawTopBorder(int[] widths)
	{
		(ColorScheme colors, BorderStyle border) = (ActiveTheme.Colors, ActiveTheme.Border);

		renderer.WriteColored(border.TopLeft.ToString(), colors.TableBorder);

		for (int i = 0; i < widths.Length; i++)
		{
			renderer.WriteColored(new string(border.Horizontal, widths[i]), colors.TableBorder);
			if (i < widths.Length - 1)
			{
				renderer.WriteColored(border.TeeTop.ToString(), colors.TableBorder);
			}
		}

		renderer.WriteColoredLine(border.TopRight.ToString(), colors.TableBorder);
	}

	private void DrawSeparator(int[] widths)
	{
		(ColorScheme colors, BorderStyle border) = (ActiveTheme.Colors, ActiveTheme.Border);

		renderer.WriteColored(border.TeeLeft.ToString(), colors.TableBorder);

		for (int i = 0; i < widths.Length; i++)
		{
			renderer.WriteColored(new string(border.Horizontal, widths[i]), colors.TableBorder);
			if (i < widths.Length - 1)
			{
				renderer.WriteColored(border.Cross.ToString(), colors.TableBorder);
			}
		}

		renderer.WriteColoredLine(border.TeeRight.ToString(), colors.TableBorder);
	}

	private void DrawBottomBorder(int[] widths)
	{
		(ColorScheme colors, BorderStyle border) = (ActiveTheme.Colors, ActiveTheme.Border);

		renderer.WriteColored(border.BottomLeft.ToString(), colors.TableBorder);

		for (int i = 0; i < widths.Length; i++)
		{
			renderer.WriteColored(new string(border.Horizontal, widths[i]), colors.TableBorder);
			if (i < widths.Length - 1)
			{
				renderer.WriteColored(border.TeeBottom.ToString(), colors.TableBorder);
			}
		}

		renderer.WriteColoredLine(border.BottomRight.ToString(), colors.TableBorder);
	}

	private void DrawHeaderRow(int[] widths)
	{
		(ColorScheme colors, BorderStyle border) = (ActiveTheme.Colors, ActiveTheme.Border);

		renderer.WriteColored(border.Vertical.ToString(), colors.TableBorder);

		for (int i = 0; i < headers.Length; i++)
		{
			renderer.WriteColored(PadCell(headers[i], widths[i]), colors.TableHeader);
			renderer.WriteColored(border.Vertical.ToString(), colors.TableBorder);
		}

		renderer.WriteLine();
	}

	private void DrawDataRows(int[] widths)
	{
		(ColorScheme colors, BorderStyle border) = (ActiveTheme.Colors, ActiveTheme.Border);
		int rowCount = data!.Length;

		for (int row = 0; row < rowCount; row++)
		{
			int columnCount = data[row].Length;
			List<TableCell[]> wrappedRows = WrapRow(row, widths);

			foreach (TableCell[] wrappedLine in wrappedRows)
			{
				renderer.WriteColored(border.Vertical.ToString(), colors.TableBorder);

				for (int col = 0; col < columnCount; col++)
				{
					TableCell cell =
						col < wrappedLine.Length ? wrappedLine[col] : new TableCell(string.Empty);
					ConsoleColor cellColor = cell.Color ?? colors.TableData;
					renderer.WriteColored(PadCell(cell.Text, widths[col]), cellColor);
					renderer.WriteColored(border.Vertical.ToString(), colors.TableBorder);
				}

				renderer.WriteLine();
			}
		}
	}

	private List<TableCell[]> WrapRow(int row, int[] widths)
	{
		int columnCount = data![row].Length;
		List<string>[] cellLines = new List<string>[columnCount];
		int maxLines = 1;

		for (int col = 0; col < columnCount; col++)
		{
			cellLines[col] = WrapText(data[row][col].Text, widths[col] - 2);
			maxLines = Math.Max(maxLines, cellLines[col].Count);
		}

		List<TableCell[]> wrappedRows = [];

		for (int line = 0; line < maxLines; line++)
		{
			TableCell[] wrappedLine = new TableCell[columnCount];
			for (int col = 0; col < columnCount; col++)
			{
				string text = line < cellLines[col].Count ? cellLines[col][line] : string.Empty;

				wrappedLine[col] = new TableCell(text, data[row][col].Color);
			}

			wrappedRows.Add(wrappedLine);
		}

		return wrappedRows;
	}

	private static List<string> WrapText(string text, int maxWidth)
	{
		if (string.IsNullOrEmpty(text))
		{
			return [string.Empty];
		}

		if (text.Length <= maxWidth)
		{
			return [text];
		}

		List<string> lines = [];
		StringBuilder currentLine = new();

		foreach (string word in text.Split(' '))
		{
			if (currentLine.Length == 0)
			{
				currentLine.Append(word);
			}
			else
			{
				int prospectiveLength = currentLine.Length + 1 + word.Length;
				if (prospectiveLength <= maxWidth)
				{
					currentLine.Append(' ').Append(word);
				}
				else
				{
					lines.Add(currentLine.ToString());
					currentLine.Clear();
					currentLine.Append(word);
				}
			}
		}

		if (currentLine.Length > 0)
		{
			lines.Add(currentLine.ToString());
		}

		return lines;
	}

	private static string PadCell(string? content, int width)
	{
		content ??= string.Empty;
		return content.Length >= width ? content[..width] : " " + content.PadRight(width - 1);
	}
}
