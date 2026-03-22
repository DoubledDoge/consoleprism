namespace ConsolePrism.Components;

using Core;
using Core.Renderers;
using Interfaces;
using Themes;

/// <summary>
/// A UI component that renders a transient, styled notification message,
/// optionally dismissing itself after a timeout.
/// </summary>
/// <param name="renderer">The renderer to write output to.</param>
/// <param name="message">The message to display.</param>
/// <param name="level">The severity level that determines the notification's colour.</param>
/// <param name="durationMs">The duration in milliseconds before the notification
/// is automatically cleared. When <c>0</c>, the notification persists
/// until manually cleared.</param>
public sealed class Notification(
    string message,
    IRenderer renderer,
    NotificationLevel level = NotificationLevel.Info,
    int durationMs = 0
) : ComponentBase
{
    private string Message { get; } = message;
    private NotificationLevel Level { get; } = level;
    private int DurationMs { get; } = durationMs;

    /// <summary>
    /// Gets or sets whether the notification is rendered with a bordered box.
    /// </summary>
    public bool Bordered { get; set; } = false;

    /// <summary>
    /// Initializes a new <see cref="Notification"/> using the default console renderer.
    /// </summary>
    /// <param name="message">The message to display.</param>
    /// <param name="level">The severity level that determines the notification's colour.</param>
    /// <param name="durationMs">The duration in milliseconds before the notification
    /// is automatically cleared. When <c>0</c>, the notification persists
    /// until manually cleared.</param>
    public Notification(
        string message,
        NotificationLevel level = NotificationLevel.Info,
        int durationMs = 0
    )
        : this(message, ConsoleRenderer.Instance, level, durationMs) { }

    /// <inheritdoc/>
    public override void Render()
    {
        ConsoleColor color = ResolveColor();
        string prefix = ResolvePrefix();
        string full = $"{prefix} {Message}";

        if (Bordered)
        {
            RenderBordered(full, color);
        }
        else
        {
            renderer.WriteColoredLine(full, color);
        }

        if (DurationMs <= 0)
        {
            return;
        }

        Thread.Sleep(DurationMs);
        ConsoleHelper.ClearCurrentLine();
    }

    private void RenderBordered(string content, ConsoleColor color)
    {
        BorderStyle border = this.ActiveTheme.Border;
        int width = content.Length + 4;

        renderer.WriteColoredLine(
            $"{border.TopLeft}{new string(border.Horizontal, width)}{border.TopRight}",
            color
        );

        renderer.WriteColored(border.Vertical.ToString(), color);
        renderer.WriteColored($"  {content}  ", color);
        renderer.WriteColoredLine(border.Vertical.ToString(), color);

        renderer.WriteColoredLine(
            $"{border.BottomLeft}{new string(border.Horizontal, width)}{border.BottomRight}",
            color
        );
    }

    private ConsoleColor ResolveColor() =>
        Level switch
        {
            NotificationLevel.Success => this.ActiveTheme.Colors.Success,
            NotificationLevel.Warning => this.ActiveTheme.Colors.Warning,
            NotificationLevel.Error => this.ActiveTheme.Colors.Error,
            _ => this.ActiveTheme.Colors.Info,
        };

    private string ResolvePrefix() =>
        Level switch
        {
            NotificationLevel.Success => "[✓]",
            NotificationLevel.Warning => "[!]",
            NotificationLevel.Error => "[✗]",
            _ => "[i]",
        };
}

/// <summary>Specifies the severity level of a <see cref="Notification"/>.</summary>
public enum NotificationLevel
{
    /// <summary>An informational notification.</summary>
    Info,

    /// <summary>A success notification.</summary>
    Success,

    /// <summary>A warning notification.</summary>
    Warning,

    /// <summary>An error notification.</summary>
    Error,
}
