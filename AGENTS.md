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
- For Mega Man X4 (USA), the player's HP is read from offset `0x141924` in BizHawk's PSX `MainRAM` domain. Read HP as an unsigned 8-bit integer using `ReadU8`.

## 6. Extracted PSX Room Data

- Local extracted game data is stored under the Git-ignored `ROOMS_Extractions/` directory. It currently has separate `MMX4/`, `MMX5/`, and `MMX6/` folders.
- Use these files as local references when investigating room layouts, assets, and game-specific data. Keep each game's extracted files separate.
- The folders include disc files such as `ARC`, `STR`, `XA`, executables, and data files. They are not source code or validated memory maps; verify any conclusions against the running game.
- Do not add extracted ROM/disc files to Git or modify them as part of routine analysis. Keep implementation changes in the project source.

## 7. Mega Man X4 Recompilation Reference

- The [MegaManX4Recomp repository](https://github.com/mstan/MegaManX4Recomp) targets the original USA disc, serial `SLUS-00561` (`SLUS_005.61`). Its [disc identity notes](https://github.com/mstan/MegaManX4Recomp/blob/master/DISC.md) list SHA-256 `3ceab06ac99add4035f912188bcbf5b16c02056f46a33d38ff0c8dbce6cb613b` and identify the disc as BIN/CUE, single-track MODE2/2352.
- The resident PS-X executable `SLUS_005.61` loads at CPU address `0x80010000`, has text size `0x11F800`, and entry point `0x800DAE8C`. These are executable load/CPU addresses, not offsets for BizHawk's `MainRAM` memory API.
- The repository's [bounded disc inventory](https://github.com/mstan/MegaManX4Recomp/blob/master/docs/DISC_INVENTORY.md) reports 130 typed ARC containers with 612 members and no separate game executable overlay identified in its examined loader paths. Treat this as inventory evidence, not proof about every possible runtime path or the meaning of individual room assets.
- This project recompiles the PS1 executable to run natively on PC and adds optional mods. Addresses or pointers in that native process are not BizHawk PS1 `MainRAM` offsets; use the repository only as static reference for disc identity, executable metadata, and asset/archive investigation.

## 8. Mega Man X4 Decompilation Reference

- The [sozud/mmx4 repository](https://github.com/sozud/mmx4) is a reverse-engineered decompilation, not the original Capcom source project. Its C and assembly code are reconstructed from retail game executables, with the goal of producing byte-matching PlayStation builds.
- The reconstruction is incomplete: some routines remain in assembly or have not yet been fully decompiled. Function names, comments, and interpretations in the repository are research labels and may not match the original developers' names or documentation.
- Use it as a static reference for investigating X4 game logic, including enemy behavior. Do not treat its reconstructed source as an authoritative original design document or copy executable/process addresses as BizHawk `MainRAM` offsets; validate memory findings against the running game.
