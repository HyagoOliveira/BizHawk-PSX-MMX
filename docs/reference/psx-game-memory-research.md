# PSX Game Memory and Research Reference

This document records detailed API and game-memory research for this BizHawk External Tools project. Keep repository-wide agent instructions in the root `AGENTS.md`.

## BizHawk API References

Because BizHawk lacks an isolated API documentation website, all contracts, behaviors, and available services must be referenced directly from the following sources:

### 1. Official GitHub Repository (`TASEmulators/BizHawk`)
- Analyze the code inside this repository: https://github.com/TASEmulators/BizHawk/tree/2.11.1
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

## PSX Mega Man Memory Maps

- The analyzer targets the USA releases of Mega Man X4, X5, and X6. Keep each game's addresses separate; do not reuse an address across games just because the field layout appears similar.
- Store address data in `src/Memory/GameMemoryAddress.cs` and select it through `GameMemoryAddressFactory.Create(gameName)`. Add or correct mappings there rather than embedding game-specific addresses in the form.
- These addresses are offsets in BizHawk's PSX `MainRAM` memory domain, not host-process addresses. Read using the appropriate memory domain explicitly.
- For Mega Man X4 (USA), the player's HP is read from offset `0x141924` in BizHawk's PSX `MainRAM` domain. Read HP as an unsigned 8-bit integer using `ReadU8`.

## Extracted PSX Room Data

- Local extracted game data is stored under the Git-ignored `ROOMS_Extractions/` directory. It currently has separate `MMX4/`, `MMX5/`, and `MMX6/` folders.
- Use these files as local references when investigating room layouts, assets, and game-specific data. Keep each game's extracted files separate.
- The folders include disc files such as `ARC`, `STR`, `XA`, executables, and data files. They are not source code or validated memory maps; verify any conclusions against the running game.
- Do not add extracted ROM/disc files to Git or modify them as part of routine analysis. Keep implementation changes in the project source.

## Mega Man X4 Recompilation Reference

- The [MegaManX4Recomp repository](https://github.com/mstan/MegaManX4Recomp) targets the original USA disc, serial `SLUS-00561` (`SLUS_005.61`). Its [disc identity notes](https://github.com/mstan/MegaManX4Recomp/blob/master/DISC.md) list SHA-256 `3ceab06ac99add4035f912188bcbf5b16c02056f46a33d38ff0c8dbce6cb613b` and identify the disc as BIN/CUE, single-track MODE2/2352.
- The resident PS-X executable `SLUS_005.61` loads at CPU address `0x80010000`, has text size `0x11F800`, and entry point `0x800DAE8C`. These are executable load/CPU addresses, not offsets for BizHawk's `MainRAM` memory API.
- The repository's [Ghidra instructions](https://github.com/mstan/MegaManX4Recomp/blob/master/ghidra/instructions.txt) specify the PS1 CPU as a 32-bit, little-endian MIPS R3000A using the MIPS I instruction set. Ghidra should use `MIPS:LE:32:default`; COP0 is the system-control coprocessor and COP2 is the GTE used for geometry operations. The CPU has no hardware FPU. This processor setup helps disassemble and analyze the executable; it is not a method for reading live RAM. BizHawk External Tools must read the emulator's `MainRAM` through its memory API.
- The same instructions describe KSEG0 (`0x80000000`-`0x801FFFFF`) and KSEG1 (`0xA0000000`-`0xA01FFFFF`) as virtual address ranges for the same 2 MB main RAM. For a KSEG0 RAM address, subtract `0x80000000` to get its `MainRAM` offset; confirm the target lies within RAM and validate candidate fields at runtime.
- The repository's [bounded disc inventory](https://github.com/mstan/MegaManX4Recomp/blob/master/docs/DISC_INVENTORY.md) reports 130 typed ARC containers with 612 members and no separate game executable overlay identified in its examined loader paths. Treat this as inventory evidence, not proof about every possible runtime path or the meaning of individual room assets.
- This project recompiles the PS1 executable to run natively on PC and adds optional mods. Addresses or pointers in that native process are not BizHawk PS1 `MainRAM` offsets; use the repository only as static reference for disc identity, executable metadata, and asset/archive investigation.

## Mega Man X4 Decompilation Reference

- The [HyagoOliveira/mmx4 fork](https://github.com/HyagoOliveira/mmx4) is a reverse-engineered decompilation. The [copilot-instructions.md](https://github.com/HyagoOliveira/mmx4/blob/main/.github/copilot-instructions.md) describes the project layout and rules for interpreting its reconstructed sources.
- This is a reverse-engineered decompilation, not the original Capcom source project. Its C and assembly code are reconstructed from retail game executables, with the goal of producing byte-matching PlayStation builds. The reconstruction remains incomplete; some routines are still assembly or have not yet been fully decompiled.
- Treat the C files as the authoritative implementation within the decompilation. Any neighboring C# files are conceptual translations for explanation or analysis, not production PC implementations, and may be incomplete. Preserve uncertainty: reconstructed names, comments, and interpretations may not reflect the original developers' names or intent.
- Use the fork as a static reference for investigating X4 game logic, including enemy behavior. Do not treat the reconstructed source as an authoritative original design document or copy executable/process addresses as BizHawk `MainRAM` offsets; validate memory findings against the running game.

## Mega Man X4 Player Address Research

- The [`psx-modding-toolchain` X4 example](https://github.com/mateusfavarin/psx-modding-toolchain/tree/main/games/Example_MegaManX4) contains a USA symbol map and reconstructed object structures that can guide address research. In [`symbols/object.txt`](https://github.com/mateusfavarin/psx-modding-toolchain/blob/main/games/Example_MegaManX4/symbols/object.txt), the USA `mega` player object base is `0x801418C8`.
- In [`include/object.h`](https://github.com/mateusfavarin/psx-modding-toolchain/blob/main/games/Example_MegaManX4/include/object.h), the reconstructed player structure places `hp` at offset `0x5C` from that base. Thus `0x801418C8 + 0x5C = 0x80141924`, corresponding to BizHawk PSX `MainRAM` offset `0x141924` (subtract the `0x80000000` KSEG0 base for this address). This independently corroborates the existing X4 USA HP location; continue reading the byte with `ReadU8` and validate its runtime meaning in-game.
- The same structure places candidate `x` and `y` fields at offsets `0x08` and `0x0C`, yielding candidate MainRAM offsets `0x1418D0` and `0x1418D4`. These are unverified: the reconstructed declarations alone do not establish units, fixed-point interpretation, signedness, or runtime behavior. Test movement and camera/room transitions before adding them to the production map.
- The example's Japanese symbol map uses a different `mega` base (`0x801419A8`). Use only the USA symbols for the target USA game; never transfer addresses between regional builds or to X5/X6.
- When validating a candidate, record game/release, BizHawk memory domain, offset, read width and signedness, raw values before and after a controlled action, and the corresponding visible in-game behavior. Keep candidates marked unvalidated until this comparison supports them.
