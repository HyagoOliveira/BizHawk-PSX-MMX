# C# Style Guide

This guide defines the C# conventions used by this .NET Framework 4.8 project. Prefer the existing implementation over this guide only when an established repository pattern is more accurate.

## General principles

- Favor clarity and explicit intent over brevity.
- Keep UI layout, memory parsing, game logic, and external-tool lifecycle responsibilities separated.
- Use C# features supported by C# 12 while retaining compatibility with `net48`.
- Never hardcode machine-dependent BizHawk paths, ROM paths, or emulator binaries.
- Resolve paths through project properties or workspace-relative configuration.
- Keep code, comments, identifiers, commit messages, and documentation in English.

## Naming

| Element | Convention | Example |
|---|---|---|
| Namespace, class, struct, enum | `PascalCase` | `BizHawk.PSXMMX.Memory` |
| Interface | `IPascalCase` | `IMemoryApi` |
| Method and property | `PascalCase` | `UpdateAfter()`, `Address` |
| Parameter and local variable | `camelCase` | `baseAddress`, `currentFrame` |
| Constant | `UPPER_SNAKE_CASE` | `DEFAULT_FONT_SIZE` |

Use descriptive names. Avoid abbreviations unless they are established by BizHawk or the game domain.

Fields must not use an underscore prefix:

```csharp
// Correct
private readonly Label frameCounterLabel = new();
private ApiContainer apis;

// Incorrect
private readonly Label _frameCounterLabel = new();
private ApiContainer _apis;
```

## Formatting

- Use file-scoped namespaces.
- Place `using` directives before the namespace.
- Open braces on the next line.
- One blank line should separate methods and closely related declarations.
- Keep expressions and conditionals readable; do not compact unrelated statements onto one line.
- Use expression-bodied members for short properties and one-line methods.

```csharp
using System.Drawing;
using BizHawk.Client.Common;

namespace BizHawk.PSXMMX.Math;

public static class Coordinates
{
    public static readonly Size Offset = new(18, 0);

    public static int ToClientX(int gamePosition) => gamePosition + Offset.Width;
}
```

## Typing and modern C#

- Use `var` when the assigned type is explicit and unambiguous.
- Use an explicit type when the identity could be ambiguous, especially for numeric conversions or API boundaries.
- Use target-typed `new()` for explicitly typed declarations and constructor arguments.
- Use nullable annotations consistently; guard nullable API containers and optional values before use.
- Preserve fixed-width numeric types when reading emulator memory. Do not replace `sbyte`, `byte`, `ushort`, `uint`, or `long` with `int` merely for convenience.

```csharp
var currentFrame = APIs.Emulation.FrameCount();
var outlineColor = Color.Red;
var label = new Label();

byte sampleByte = APIs.Memory.ReadByte(0x09A0);
var greetingLabel = new Label
{
    Text = "Tool Loaded"
};
```

Format hex values with interpolation:

```csharp
var hexText = $"RAM (0x09A0): 0x{sampleByte:X2}";
```

Use collection expressions only when they improve an already clear collection initialization and remain compatible with the project's language/runtime targets.

## Nullability and guards

Prefer narrow, explicit guards over broad exception handling:

```csharp
if (MaybeAPIContainer is null) return;
if (APIs.Emulation.GetGameInfo() is null) return;
```

Do not silently cast nullable values to a non-null type. If a dependency or API result is required, validate it and surface an appropriate error or unsupported-state message.

## Memory access

- Access PSX values through BizHawk's memory API.
- Use explicit memory-domain addresses and `MainRAM` offsets for the target game.
- Keep MMX4, MMX5, and MMX6 addresses in separate game mappings.
- Do not reuse an address or structure layout across games solely because the fields appear similar.
- Preserve reconstructed offsets as offsets; do not describe executable, bus, or process addresses as emulator `MainRAM` addresses.
- Document the source and validation status of each non-obvious mapping.
- Where a reconstructed field is uncertain, retain the raw type and mark the meaning as unknown rather than guessing.

Consult the matching-decompilation forks and [PSX game memory reference](reference/psx-memory-reference.md) when interpreting game structures.

## Error handling

- Use `try/catch` only around operations that can fail for a recognized, actionable reason.
- Do not catch broad exceptions and continue as though initialization succeeded.
- Include enough context in a log or UI error to identify the failed operation.
- Prefer early returns for unsupported or uninitialized state.

## Documentation

- Update the [PSX game memory reference](reference/psx-memory-reference.md) when adding or correcting verified game addresses or layouts.
- State which USA release and which runtime evidence support a memory finding.
- Distinguish reconstructed C or assembly behavior from verified emulator behavior.
- Do not commit extracted ROM/disc data or private game dumps.
