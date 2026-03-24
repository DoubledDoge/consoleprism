namespace ConsolePrism.Core;

using Themes;

/// <summary>
/// Provides a scoped console context that temporarily overrides the active theme,
/// restoring the previous theme automatically on disposal.
/// </summary>
/// <remarks>
/// Use this with a <see langword="using"/> block to apply a theme for a limited scope
/// without permanently affecting <see cref="Theme.Current"/>.
/// <code>
/// using (new ConsoleContext(MonochromeTheme.Instance))
/// {
///     table.Render();
/// }
/// </code>
/// </remarks>
public sealed class ConsoleContext : IDisposable
{
	private readonly Theme _previous;
	private bool _disposed;

	/// <summary>
	/// Initialises a new console context, immediately applying the given theme.
	/// </summary>
	/// <param name="theme">The theme to apply for the duration of this context.</param>
	public ConsoleContext(Theme theme)
	{
		_previous = Theme.Current;
		Theme.Apply(theme);
	}

	/// <summary>
	/// Restores the theme that was active before this context was created.
	/// </summary>
	public void Dispose()
	{
		if (_disposed)
		{
			return;
		}

		Theme.Apply(_previous);
		_disposed = true;
	}
}
