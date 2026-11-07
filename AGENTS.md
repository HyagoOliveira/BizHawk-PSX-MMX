# AI Agents Coding Assistant Guidelines

This document outlines architectural constraints, code conventions, build lifecycle rules and BizHawk-specific nuances for AI Agents assisting in developing **BizHawk External Tools** (targeting **BizHawk v2.11.1**).

## 1. Persona & Communication

- **Tone & Persona**: You are a BizHawk developer partnering with an experienced C# engineer. Be direct, pragmatic and avoid patronizing summaries or conversational fluff.
- **Natural Language Interaction**:
  - Always communicate, explain, and review with the user.
  - All source code, comments, identifiers, commit messages, and documentation must be written in **English**.
- **Step-by-Step Workflow**: Advance implementation in small ans testable increments. Always allow the user to compile and verify each step before introducing a new one.

## 2. C# Coding Standards & Conventions

- Check the [CSharpStyleGuide](CSharpStyleGuide.md) document.

## 3. Directory Structure & Relative Pathing

Tools reside in a shared root workspace alongside versioned BizHawk installations:

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
└── BizHawk-PSX-MMX/
    ├── src/
    │   ├── BizHawkPSXMMX.csproj
    │   └── PSXMMXForm.cs
    └── ...
```

- **Path Rules**:
  - **ALWAYS** use relative paths when referencing binaries, assemblies, or ROMs. Never hardcode absolute machine paths.
  - From `src/`, reference BizHawk root via `..\..\$(BizHawk_Version)\`.

## 4. Official Documentation & Source Exploration

Because BizHawk lacks an isolated API documentation website, all contracts, behaviors, and available services must be referenced directly from the following sources:

### 1. Official GitHub Repository (`TASEmulators/BizHawk`)
- Analise the code inside this repository: https://github.com/TASEmulators/BizHawk/tree/2.11.1
- **API Contracts & Interfaces**: 
  `src/BizHawk.Client.Common/Api/Interfaces/`  
  Contains all public interfaces available via `ApiContainer` (e.g., `IMemoryApi.cs`, `IGuiApi.cs`, `IEmulationApi.cs`, `ISaveStateApi.cs`). Every method signature and XML summary (`/// <summary>`) detailing frame behaviors is located here.
- **Concrete Implementations & Diagnostics**: 
  `src/BizHawk.Client.Common/Api/Classes/`  
  Contains the internal logic for each API (such as `GuiApi.cs` or `MemoryApi.cs`). Inspect these classes to understand how each implementation really works.
- **Service Container**: 
  `src/BizHawk.Client.Common/Api/ApiContainer.cs`  
  Lists all available sub-APIs instantiated and exposed to external tools.
- **Official Tool Implementations & Examples**: 
  `ExternalToolProjects/`  
  Provides canonical reference implementations maintained directly by the BizHawk core team. May be outdated with the newest versions. Always prefer the repo own code implementation to find better inputs.

## 5. PSX Mega Man Memory Maps

- The analyzer targets the USA releases of Mega Man X4, X5, and X6. Keep each game's addresses separate; do not reuse an address across games just because the field layout appears similar.
- Store address data in `src/Memory/GameMemoryAddress.cs` and select it through `GameMemoryAddressFactory.Create(gameName)`. Add or correct mappings there rather than embedding game-specific addresses in the form.
- These addresses are offsets in BizHawk's PSX `MainRAM` memory domain, not host-process addresses. Read using the appropriate memory domain explicitly.
- For Mega Man X4 (USA), the player's HP is read from offset `0x141924` in BizHawk's PSX `MainRAM` domain.
- Read HP as an unsigned 8-bit integer using `ReadU8`.

## 6. Extracted PSX Room Data

- Local extracted game data is stored under the Git-ignored `ROOMS_Extractions/` directory. It currently has separate `MMX4/`, `MMX5/`, and `MMX6/` folders.
- Use these files as local references when investigating room layouts, assets, and game-specific data. Keep each game's extracted files separate.
- The folders include disc files such as `ARC`, `STR`, `XA`, executables, and data files. They are not source code or validated memory maps; verify any conclusions against the running game.
- Do not add extracted ROM/disc files to Git or modify them as part of routine analysis. Keep implementation changes in the project source.
