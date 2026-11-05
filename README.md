# BizHawk External Tool Template

A clean, modular starter template for developing custom **External Tools** for the [BizHawk](https://github.com/TASEmulators/BizHawk) multi-system emulator (targeting the newest **v2.11.1**).

This template configures a modern **C# 12** project, providing automated build lifecycle management and a simple `ToolFormBase` implementation.

## Prerequisites

- **IDE**: Visual Studio 2026 or equivalent
- **Emulator**: [BizHawk 2.11.1](https://github.com/TASEmulators/BizHawk/releases/tag/2.11.1)
- **Target Runtime**: .NET Framework 4.8

## Directory Architecture

This template expects your repository to sit side-by-side with your versioned BizHawk installations inside your development root folder:

```text
Root/
├── 2.11.1/
│   ├── dll/
│   │   ├── BizHawk.Client.Common.dll
│   │   ├── BizHawk.Common.dll
│   │   ├── BizHawk.Emulation.Common.dll
│   │   └── BizHawk.WinForms.Controls.dll
│   ├── ExternalTools/
│   └── EmuHawk.exe
└── BizHawk-ExternalTool-Template/
    ├── .gitignore
    ├── README.md
    └── src/
        ├── BizHawkExternalToolTemplate.csproj
        └── CustomToolForm.cs
```

## Build Pipeline

The project file provides a "hot-reload" development cicle via custom MSBuild targets:

1. Kill any running EmuHawk instance
2. Copies the compiled assembly dll into the BizHawk/ExternalTools folder
3. Re-launches EmuHawk and opens your External Tool automaticaly with an optional Test ROM

### Configuration

Open [BizHawkExternalToolTemplate](src/BizHawkExternalToolTemplate.csproj) to configure your environment settings:

```xml
<PropertyGroup>
  <!-- Target BizHawk environment settings -->
  <BizHawk_Version>2.11.1</BizHawk_Version>
  <BizHawk_Home>$(ProjectDir)..\..\$(BizHawk_Version)\</BizHawk_Home>
  <BizHawk_DLL>$(BizHawk_Home)dll\</BizHawk_DLL>

  <!-- Optional path to your test ROM. Leave it empty to open BizHawk without a Test ROM -->
  <TestRom_Path>C:\Users\{USER_NAME}\Documents\Games\ROMS\PSOne\{GAME_FOLDER}\{GAME}.cue</TestRom_Path>
</PropertyGroup>
```

## AI Assistant Guidelines

If you are using AI agents to accelerate your development workflow (such as GitHub Copilot, ChatGPT or Gemini), refer to [AGENTS.md](AGENTS.md). The guide defines the BizHawk v2.11.1 API lifecycle and other caveats.

## Getting Started: Creating a New Tool

1. **Use this template**: Clone or create a new repository based on this structure.
   - [Optional] Change the [CSharpStyleGuide](CSharpStyleGuide.md) document to fit into your code style.
2. **Rename the Project and Class**:
   - Rename `BizHawkExternalToolTemplate.csproj` to your tool name (e.g., `MemoryMonitor.csproj`).
   - Rename `CustomToolForm.cs` (e.g., `MemoryMonitorForm.cs`).
3. **Update Metadata**:
   - Change the `[ExternalTool("Your Tool Name")]` attribute.
   - Update `WindowTitleStatic => "Your Tool Name";`.
4. **Implement Lifecycle Methods**:
   - `Restart()`: Reset state variables whenever a new game/ROM is loaded.
   - `UpdateAfter()`: Runs after every emulated frame. Use this to read RAM addresses via `APIs.Memory` or render any thing via `APIs.Gui`.

## License

This template is licensed under the [MIT License](LICENSE).
