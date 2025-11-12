# ConsolePrism

## 📑 Table of Contents

- [🚀 Features](#-features)
- [📦 Installation](#-installation)
- [📚 Core Classes and Usage](#-core-classes-and-usage)
- [🎨 Theming](#-theming)
- [💻 Requirements](#-requirements)
- [📄 License](#-license)
- [🤝 Contributing](#-contributing)

## 🚀 Features

-  **Theming System** — Easily changeable color schemes and border styles
-  **Semantic Text Output** — Contextualized colored text (errors, success, warnings, information)
-  **Basic Menu Components** — Numbered, interactive, and bordered menu styles
-  **Tables** — Auto-sizing tables with text wrapping support
-  **Progress Bars** — Simple visual progress indicators
-  **Layout Utilities** — Text positioning, centering, and cursor control for console use

## 📦 Installation

### NuGet Package

```bash
dotnet add package ConsolePrism
```

### Manual Installation
1. Clone the repository
2. Add a reference to your project:

```bash
dotnet add reference path/to/your/src/ConsolePrism.csproj
```

## Usage Example

```csharp
using ConsolePrism.Core;
using ConsolePrism.Components;
using ConsolePrism.Themes;

// Customize your theme (Both in your code, and in the package)
ColorScheme.Primary = ConsoleColor.Cyan;
BorderStyle.UseDouble();

// Display colored text
ColorWriter.WriteSuccessLine("Operation completed!");
ColorWriter.WriteErrorLine("Something went wrong.");

// Show a menu
var options = new[] { "Start Game", "Settings", "Exit" };
int choice = Menu.DisplayInteractive("Main Menu", options);

// Display data table
string[] headers = { "Name", "Score", "Level" };
string[,] data = {
    { "Alice", "1250", "10" },
    { "Bob", "980", "8" },
    { "Charlie", "1500", "12" }
};
Table.Display(headers, data);

// Show progress
for (int i = 0; i <= 100; i += 10)
{
    ProgressBar.DisplayInPlace(i, 100, label: "Loading");
    Thread.Sleep(200);
}
Console.WriteLine();
```

## 📚 Core Classes and Usage

### Color Writer

```csharp
ColorWriter.WriteError("Error message");
ColorWriter.WriteSuccess("Success message");
ColorWriter.WriteWarning("Warning message");
ColorWriter.WriteInfo("Info message");
ColorWriter.WriteHighlight("Highlighted text");

// Custom colors when needed
ColorWriter.WriteColored("Custom text", ConsoleColor.Magenta);
```

### Console Helper

```csharp
// Clear screen
ConsoleHelper.Clear();

// Position text
ConsoleHelper.WriteCentered("Centered Title");
ConsoleHelper.WriteRight("Right-aligned", padding: 2);
ConsoleHelper.WriteAt("Positioned text", x: 10, y: 5);

// Cursor control
ConsoleHelper.HideCursor();
ConsoleHelper.MoveCursor(0, 10);

// Draw lines
ConsoleHelper.DrawHorizontalLine('─');
```

### Menus

```csharp
// Simple numbered menu
int choice = Menu.DisplayNumbered("Select Option", 
    "Option 1", "Option 2", "Option 3");

// Interactive arrow-key navigation
int choice = Menu.DisplayInteractive("Main Menu",
    "New Game", "Continue", "Settings", "Exit");

// Bordered menu with box
int choice = Menu.DisplayBordered("Choose Difficulty",
    "Easy", "Normal", "Hard");
```

### Tables

```csharp
string[] headers = { "Column 1", "Column 2", "Column 3" };
string[,] data = {
    { "Data 1", "Data 2", "Data 3" },
    { "More data", "Even more", "Last column" }
};

Table.Display(headers, data);

// Custom column widths (optional)
int[] widths = { 15, 20, 10 };
Table.Display(headers, data, widths);
```

### Progress Bars

```csharp
// Simple progress bar
ProgressBar.Display(50, 100);

// With label
ProgressBar.DisplayLine(75, 100, label: "Processing");

// Update in place (for animations)
for (int i = 0; i <= 100; i++)
{
    ProgressBar.DisplayInPlace(i, 100, label: "Loading");
    Thread.Sleep(50);
}
```

## 🎨 Theming

### Color Scheme

```csharp
// Semantic colors
ColorScheme.Error = ConsoleColor.Red;
ColorScheme.Success = ConsoleColor.Green;
ColorScheme.Warning = ConsoleColor.Yellow;
ColorScheme.Info = ConsoleColor.Cyan;
ColorScheme.Highlight = ConsoleColor.Magenta;

// UI element colors
ColorScheme.Primary = ConsoleColor.Blue;
ColorScheme.Muted = ConsoleColor.DarkGray;

// Component-specific colors
ColorScheme.MenuTitle = ConsoleColor.Yellow;
ColorScheme.MenuSelected = ConsoleColor.Green;
ColorScheme.TableHeader = ConsoleColor.Cyan;
ColorScheme.ProgressBarComplete = ConsoleColor.Green;
```

## 💻 Requirements

- .NET 10.0 or higher
- Should work on Windows, macOS, and Linux terminals
- Best experience with terminals with dark backgrounds (Not going to make a light theme yet maybe, sorry)

## 📄 License

MIT License — see [LICENSE](LICENSE) file for details

## 🤝 Contributing

Contributions welcome! It’s all open source, so feel free to fork and make a pull request.

## 🗺️ Roadmap

Current version: **v0.1.1** (Pre-release)

Future considerations:
- Object-based table data support
- Input components (text boxes, confirmations)
- Layout containers (panels, columns)
- ASCII art utilities
- Animation helpers
- I don’t know, maybe more?

---

Last Updated: 2025
