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
- Analize the code inside this repository: https://github.com/TASEmulators/BizHawk/tree/2.11.1
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
- The factory currently identifies games by BizHawk game name: `Mega Man X4 (USA)`, `Mega Man X5 (USA)`, and `Mega Man X6 (USA)`. Unsupported or unavailable game names return `null`; callers must handle that case without reading memory.
- These addresses are offsets in BizHawk's PSX `MainRAM` memory domain, not host-process addresses. Read using the appropriate memory domain explicitly.
- The X4 USA player HP address is `0x141924`. External Tool reads in BizHawk 2.11.1 confirmed that this byte tracks the player's energy (for example, `0x20` before damage and `0x1D` after one observed hit). The project's convention is to display the raw value as an unsigned 8-bit integer (`ReadU8`).
- X5 and X6 addresses, along with player position/speed, camera, object groups, and flags, remain unvalidated until checked against live gameplay in BizHawk 2.11.1.
- When validating a field, record the game/release, memory domain, address, read width/signedness, raw values before and after a controlled in-game action, and what was visible in the game. Do not describe a candidate address or inferred representation as confirmed until this comparison supports it.
