# C# Style Guide

A comprehensive coding standard and style guide for developing using modern C# features in .NET Framework 4.8 / modern SDK style.

## 1. Core Principles

- **Clarity over Brevity**: Write descriptive, intent-revealing code.
- **SOLID Compliance**: Maintain decoupled classes; separate UI layout from memory parsing and emulation business logic.
- **Modern C# Standards**: Leverage modern C# syntax (up to C# 12) while keeping target runtime compatibility (`net48`).
- **Relative Pathing**: Never hardcode machine-dependent paths. All assemblies, ROM paths, and emulator binaries must be resolved relative to workspace root.

## 2. Naming Conventions

### 2.1 Identifiers Summary

| Element | Style | Example | Notes |
| :--- | :--- | :--- | :--- |
| Classes, Structs, Enums | `PascalCase` | `MemoryInspectorForm`, `DisplaySurfaceID` | Standard .NET naming |
| Interfaces | `IPascalCase` | `IExternalToolForm`, `IMemoryApi` | Always prefix with `I` |
| Methods | `PascalCase` | `UpdateAfter()`, `DrawOverlay()` | Standard .NET naming |
| Properties | `PascalCase` | `WindowTitleStatic`, `ApiContainer` | Standard .NET naming |
| Local Variables | `camelCase` | `currentFrame`, `outlineColor` | Descriptive, self-documenting |
| Method Arguments | `camelCase` | `surfaceID`, `sampleByte` | Avoid single-letter names |
| Constants | `UPPER_SNAKE_CASE` | `DEFAULT_FONT_SIZE`, `MAX_BUFFER_SLOTS` | Descriptive |

### 2.2 Strict Field Rules
- **NEVER use the underscore prefix (`_`)** for fields under any circumstances.
  ```csharp
  // Correct
  private readonly Label frameCounterLabel = new();
  private ApiContainer apis;

  // Incorrect
  private readonly Label _frameCounterLabel = new();
  private ApiContainer _apis;
  ```

## 3. Formatting & Code Structure

### 3.1 File-Scoped Namespaces (C# 10+)
Use file-scoped namespaces to reduce visual nesting across all source files.

```csharp
namespace BizHawkExternalTool;

using System.Drawing;
using System.Windows.Forms;
using BizHawk.Client.Common;
using BizHawk.Client.EmuHawk;
```

### 3.2 Expression-Bodied Members
Use expression-bodied syntax (`=>`) for one-liner methods, read-only properties, and property getters.

```csharp
// Read-only properties
protected override string WindowTitleStatic => "Memory Inspector";
private ApiContainer APIs => MaybeAPIContainer!;

// One-liner methods
public override void Restart() => base.Restart();
```

### 3.3 Implicit Typing (`var`)
Use `var` whenever the concrete type is explicit from the right-hand side assignment, instantiation, or standard factory pattern.

```csharp
// Correct
var currentFrame = APIs.Emulation.FrameCount();
var outlineColor = Color.Red;
var label = new Label();

// Use explicit type only when type identity is ambiguous or needs casting
byte sampleByte = APIs.Memory.ReadByte(0x09A0);
```

### 3.4 Concise / Inline Statements
Keep small and concise conditions on a single line or early-return guards without redundant block nesting.

```csharp
// Preferred early-exit guard
if (MaybeAPIContainer is null || APIs.Emulation.GetGameInfo() is null) return;
```

## 4. Modern C# Features (Post-C# 9.0)

When applying modern C# features, write code with explicit semantics:

### 4.1 Target-Typed `new()` Expressions (C# 9.0)
Omit the redundant constructor type when declared explicitly on the left side:

```csharp
private readonly Label greetingLabel = new()
{
    Text = "Tool Loaded",
    Location = new Point(20, 20),
    AutoSize = true
};
```

### 4.2 String Interpolation Specifiers
Format hex representations and byte values using inline format specifiers rather than manual `ToString()` conversion calls:

```csharp
// Formats byte as two-digit uppercase hex (e.g., 0x0F)
var hexText = $"RAM (0x09A0): 0x{sampleByte:X2}";
```

---

## 5. BizHawk Tool Architecture & API Rules

### 5.1 Form Lifecycle & Registration
- External Tool forms must inherit from `ToolFormBase` and implement `IExternalToolForm`.
- Forms must be decorated with `[ExternalTool("Tool Name", Description = "...")]`.
- Forms **must** override `protected override string WindowTitleStatic` to satisfy window title and docking state requirements.

```csharp
[ExternalTool("Custom Inspector", Description = "Inspects live memory.")]
public sealed class CustomInspectorForm : ToolFormBase, IExternalToolForm
{
    protected override string WindowTitleStatic => "Custom Inspector";
    
    public ApiContainer? MaybeAPIContainer { get; set; }
    private ApiContainer APIs => MaybeAPIContainer!;
}
```

### 5.2 Emulation Frame Loop (`UpdateAfter`)
- Memory reads and OSD rendering must happen in `UpdateAfter()`.
- Always check that the API container and active game info exist before accessing memory or core APIs:

```csharp
protected override void UpdateAfter()
{
    if (MaybeAPIContainer is null || APIs.Emulation.GetGameInfo() is null) return;
    
    // Logic here
}
```

### 5.3 OSD Graphics Rendering (`IGuiApi`)
- In BizHawk 2.11.1, drawing calls require an explicit surface identifier to prevent internal renderer exceptions (`Get2DRenderer`).
- **Always provide `DisplaySurfaceID.Client`** (or `DisplaySurfaceID.EmuCore`) as the `surfaceID` parameter:

```csharp
APIs.Gui.DrawRectangle(
    x: 20,
    y: 20,
    width: 140,
    height: 50,
    line: Color.Red,
    background: Color.FromArgb(64, 255, 0, 0),
    surfaceID: DisplaySurfaceID.Client
);

APIs.Gui.DrawString(
    x: 25,
    y: 25,
    message: $"FRAME: {currentFrame}",
    forecolor: Color.Yellow,
    backcolor: Color.FromArgb(180, 0, 0, 0),
    fontsize: 11,
    surfaceID: DisplaySurfaceID.Client
);
```

### 5.4 Text Rendering Guidelines
- **`APIs.Gui.DrawString`**: Preferred for general debugging and HUD text overlays. Supports fonts, sizing, and backcolors reliably.
- **`APIs.Gui.PixelText`**: Use only with default font settings (omit `fontfamily`). Passing desktop font names (e.g., `"Arial"`) causes glyph mismatch and fails silently.

## 6. Project & MSBuild Configuration Guidelines

1. **Targeting**: `.csproj` must specify `<TargetFramework>net48</TargetFramework>` with `<LangVersion>12.0</LangVersion>`.
2. **References**: Reference `Microsoft.NETFramework.ReferenceAssemblies` (v1.0.3) to enable modern SDK builds.
3. **Build Target Protection (`PreBuild`)**: Must terminate any running `EmuHawk.exe` using `taskkill /F /IM EmuHawk.exe 2>&1 || exit 0` to release OS assembly locks.
4. **Deploy Target (`PostBuild`)**: Copy `.dll` output to `$(BizHawk_Home)ExternalTools` and start the emulator asynchronously with `--open-ext-tool-dll`.
