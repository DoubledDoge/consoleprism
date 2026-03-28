# ConsolePrism

<div align="center">

[![CI/CD](https://github.com/DoubledDoge/ConsolePrism/actions/workflows/ci-cd.yml/badge.svg)](https://github.com/DoubledDoge/ConsolePrism/actions/workflows/ci-cd.yml)
[![CodeQL](https://github.com/DoubledDoge/ConsolePrism/actions/workflows/codeql.yml/badge.svg)](https://github.com/DoubledDoge/ConsolePrism/actions/workflows/codeql.yml)
[![NuGet](https://img.shields.io/nuget/v/ConsolePrism.svg?label=NuGet)](https://www.nuget.org/packages/ConsolePrism)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![.NET](https://img.shields.io/badge/.NET-9.0%20%7C%2010.0-512BD4)](https://dotnet.microsoft.com)
[![Downloads](https://img.shields.io/nuget/dt/ConsolePrism.svg)](https://www.nuget.org/packages/ConsolePrism)

An opinionated, component-based UI framework for .NET console applications. Build terminal interfaces with tables, menus, progress bars, spinners, layout containers, and more.

</div>

---

## 📑 Table of Contents

- [🚀 Features](#-features)
- [📦 Installation](#-installation)
- [📚 Usage Guide](#-usage-guide)
- [🎨 Theming](#-theming)
- [🏗️ Architecture](#-architecture)
- [💻 Requirements](#-requirements)
- [📄 License](#-license)
- [🤝 Contributing](#-contributing)

---

## 🚀 Features

- **Theming System** — Fully composable themes combining colour schemes and border styles, with built-in presets
- **Semantic Colour Output** — Contextual coloured text for errors, success, warnings, and information
- **Rich Components** — Tables, menus, progress bars, spinners, and notifications
- **Layout Containers** — Panel, Row, Column, and Viewport for structured terminal layouts
- **Renderer Abstraction** — Swap rendering backends for testing or deferred output via `IRenderer`
- **Scoped Theming** — Apply themes globally or per-component, with scoped overrides via `ConsoleContext`

---

## 📦 Installation

### NuGet Package
```bash
dotnet add package ConsolePrism
```

### Manual Installation

1. Clone the repository
2. Add a reference to your project:
```bash
dotnet add reference path/to/src/ConsolePrism.csproj
```

---

## 📚 Usage Guide

### Semantic Color Output
```csharp
using ConsolePrism.Core;

ColorWriter.WriteSuccessLine("Operation completed!");
ColorWriter.WriteErrorLine("Something went wrong.");
ColorWriter.WriteWarningLine("Proceed with caution.");
ColorWriter.WriteInfoLine("Here is some information.");
ColorWriter.WriteHighlightLine("Important text.");

// Explicit colour when needed
ColorWriter.WriteColoredLine("Custom text", ConsoleColor.Magenta);
```

### Menus
```csharp
using ConsolePrism.Components;

// Arrow-key interactive menu (default)
string[] choices = ["New Game", "Continue", "Settings", "Exit"];

Menu interactive = new("Main Menu", MenuStyle.Interactive, choices);

int choice = interactive.Interact();

// Numbered menu
Menu numbered = new("Select Difficulty", MenuStyle.Numbered, "Easy", "Normal", "Hard");

int difficulty = numbered.Interact();

// Bordered menu
Menu bordered = new("Choose Mode", MenuStyle.Bordered, "Story", "Creative", "Survival");

int mode = bordered.Interact();
```

### Tables
```csharp
using ConsolePrism.Components;

string[] headers = ["Name", "Score", "Level"];
string[][] data = [
    ["Alice",   "1250", "10"],
    ["Bob",     "980",  "8" ],
    ["Charlie", "1500", "12"],
]; // Can be nullable for empty cells

new Table(headers, data).Render();

// Custom column widths
int[] widths = [15, 10, 8];
new Table(headers, data, widths).Render();
```

### Progress Bars
```csharp
using ConsolePrism.Components;

// Static progress bar
new ProgressBar(75, "Processing", 100).Render();

// Animated in-place progress
ProgressBar bar = new(0, "Loading", 100) { InPlace = true };

for (int i = 0; i <= 100; i++)
{
    bar.Current = i;
    bar.Render();
    Thread.Sleep(50);
}
```

### Spinners
```csharp
using ConsolePrism.Components;

Spinner spinner = new(Spinner.Dots, "Loading assets...");
spinner.Start();

// Do work...
await LoadAssetsAsync();

spinner.Stop("Assets loaded successfully!");

// Using Dispose pattern
using Spinner spinner = new(Spinner.Pulse, "Connecting...");
spinner.Start();
await ConnectAsync();
```

### Notifications
```csharp
using ConsolePrism.Components;

new Notification("File saved!", false, NotificationLevel.Success).Render();
new Notification("Low disk space.", false, NotificationLevel.Warning).Render();
new Notification("Connection failed.", false NotificationLevel.Error).Render();

// Transient notification
new Notification("Autosaving...", false, NotificationLevel.Info, 2000).Render();

// Bordered notification
new Notification("Welcome to ConsolePrism!", true, NotificationLevel.Info).Render();
```

### Console Utilities
```csharp
using ConsolePrism.Core;

// Positioning
ConsoleHelper.WriteCentered("Centered Title");
ConsoleHelper.WriteRight("Right-aligned", padding: 2);
ConsoleHelper.WriteAt("Positioned text", x: 10, y: 5);

// Cursor control
ConsoleHelper.HideCursor();
ConsoleHelper.MoveCursor(0, 10);
ConsoleHelper.ShowCursor();

// Drawing
ConsoleHelper.DrawHorizontalLine('─');
ConsoleHelper.WriteEmptyLines(2);
```

---

## 🎨 Theming

### Applying a Built-in Preset
```csharp
using ConsolePrism.Themes;
using ConsolePrism.Themes.Presets;

// Apply globally
Theme.Apply(NordTheme.Instance);
Theme.Apply(MonochromeTheme.Instance);
Theme.Apply(RetroTheme.Instance);

// Reset to default
Theme.Apply(Theme.Default);
```

### Creating a Custom Theme
```csharp
using ConsolePrism.Themes;

Theme myTheme = new()
{
    Colors = new ColorScheme
    {
        Primary   = ConsoleColor.Cyan,
        Success   = ConsoleColor.Green,
        Error     = ConsoleColor.Red,
        Warning   = ConsoleColor.Yellow,
        Info      = ConsoleColor.Blue,
        Highlight = ConsoleColor.Magenta,
        Muted     = ConsoleColor.DarkGray,

        MenuTitle    = ConsoleColor.Cyan,
        MenuOption   = ConsoleColor.White,
        MenuSelected = ConsoleColor.Green,
        MenuBorder   = ConsoleColor.DarkGray,

        TableHeader  = ConsoleColor.Cyan,
        TableBorder  = ConsoleColor.DarkGray,
        TableData    = ConsoleColor.White,

        ProgressBarComplete   = ConsoleColor.Green,
        ProgressBarIncomplete = ConsoleColor.DarkGray,
        ProgressBarText       = ConsoleColor.White
    },
    Border = BorderStyle.Rounded
};

Theme.Apply(myTheme);
```

### Scoped Theme Override
```csharp
using ConsolePrism.Core;
using ConsolePrism.Themes.Presets;

// Theme.Current is temporarily replaced for the duration of the block
using (new ConsoleContext(MonochromeTheme.Instance))
{
    table.Render();
}
// Theme.Current is automatically restored here
```

### Per-Component Theme Override
```csharp
Table table = new(headers, data)
{
    Theme = RetroTheme.Instance
};

table.Render(); // uses RetroTheme regardless of Theme.Current
```

### Border Styles
```csharp
using ConsolePrism.Themes;

// Built-in presets
BorderStyle.Single;   // ┌─┐ │ └─┘  (default)
BorderStyle.Double;   // ╔═╗ ║ ╚═╝
BorderStyle.Rounded;  // ╭─╮ │ ╰─╯
BorderStyle.Ascii;    // +-+ | +-+

// Custom border
Borderstyle custom = new()
{
    TopLeft  = '╔', TopRight  = '╗',
    BottomLeft = '╚', BottomRight = '╝',
    Horizontal = '═', Vertical = '║',
    Cross = '╬',
    TeeLeft = '╠', TeeRight = '╣',
    TeeTop  = '╦', TeeBottom = '╩'
};
```

---

## 🏗️ Architecture
```
ConsolePrism/
├── Interfaces/
│   ├── IRenderable       → Anything that can render itself
│   ├── IInteractable     → Components that accept user input
│   ├── IComponent        → Base interface for all UI components
|   └── IRenderer         → Output backend abstraction
│
├── Themes/
│   ├── ColorScheme       → Full color palette definition
│   ├── BorderStyle       → Border character set definition
│   ├── Theme             → Unified theme object (ColorScheme + BorderStyle)
│   └── Presets/
│       ├── NordTheme     → Arctic blue tones with rounded borders
│       ├── MonochromeTheme → Grayscale for minimal or accessible output
│       └── RetroTheme    → Amber tones with ASCII borders
│
├── Core/
│   ├── ColorWriter       → Semantic colored text output
│   ├── ConsoleHelper     → Cursor control, positioning, and drawing utilities
│   ├── ConsoleContext    → Scoped theme switching via IDisposable
│   └── Rendering/
│       ├── ConsoleRenderer   → Default implementation writing to Console
│       └── StringRenderer    → In-memory buffer for testing and layout buffering
│
├── Components/
|   ├── ComponentBase     → Abstract base with theme resolution and renderer swapping
│   ├── Menu              → Numbered, interactive, and bordered menu styles
│   ├── Table             → Auto-sizing bordered table with text wrapping
│   ├── ProgressBar       → Static and in-place animated progress bars
│   ├── Spinner           → Animated spinners for async operations
│   ├── Notification      → Transient styled messages with optional auto-dismiss
│   └── Prompt            → Styled input prompt with optional masked entry
│
└── Layout/
    ├── Panel             → Bordered content container with title support
    ├── Row               → Vertical component stacking with optional spacing
    ├── Column            → Horizontal side-by-side component layout
    └── Viewport          → Scrollable content region with fixed visible height
```

---

## 💻 Requirements

- .NET 9.0 or .NET 10.0
- Windows, macOS, or Linux
- A terminal with a dark background is recommended for the best visual experience

---

## 📄 License

See [LICENCE](LICENSE) for details. (MIT Licence)

---

## 🤝 Contributing

Contributions are welcome! You can:

- Report bugs or request features via [Issues](https://github.com/DoubledDoge/ConsolePrism/issues)
- Submit pull requests
- Suggest improvements to the API
- Propose new components, themes, or layout containers
