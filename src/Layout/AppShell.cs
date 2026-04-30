namespace ConsolePrism.Layout;

using Components;
using Core;
using Interfaces;
using Themes;

/// <summary>
/// A root-level layout container that renders a persistent header, a main content area,
/// and forces a bounded footer to the very bottom of the console window.
/// </summary>
/// <param name="title">The text to display centered in the header panel.</param>
/// <param name="content">The main body content to render.</param>
/// <param name="leftFooter">Optional text aligned to the left of the footer.</param>
/// <param name="rightFooter">Optional text aligned to the right of the footer.</param>
public sealed class AppShell(
	string title,
	IRenderable content,
	string? leftFooter = null,
	string? rightFooter = null
) : ComponentBase
{
	private string Title { get; } = title;
	private IRenderable Content { get; } = content;
	private string? LeftFooter { get; } = leftFooter;
	private string? RightFooter { get; } = rightFooter;

	/// <inheritdoc />
	protected override bool SupportsRendererSwap => false;

	/// <inheritdoc />
	protected override IRenderer? SwapRenderer(IRenderer? swapRenderer) => null;

	/// <inheritdoc />
	public override void Render()
	{
		Console.Clear();

		int width = Console.WindowWidth;
		int height = Console.WindowHeight;
		ColorScheme colors = ActiveTheme.Colors;

		string headerText = ConsoleHelper.PadCenter($" {Title.ToUpperInvariant()} ", width - 2);
		new Panel(title: null, content: new ConsoleText(headerText, colors.Highlight)).Render();
		ConsoleHelper.WriteEmptyLines(1);

		Content.Render();

		int contentEndLine = Console.CursorTop;

		int footerLine = Math.Max(contentEndLine + 1, height - 4);

		Console.SetCursorPosition(0, footerLine);

		string footerL = LeftFooter ?? string.Empty;
		string footerR = RightFooter ?? string.Empty;
		int spaceCount = Math.Max(0, width - 4 - footerL.Length - footerR.Length);
		string footerText = footerL + new string(' ', spaceCount) + footerR;

		new Panel(title: null, content: new ConsoleText(footerText, colors.Muted)).Render();

		Console.SetCursorPosition(0, contentEndLine);
		Console.WriteLine();
	}
}
