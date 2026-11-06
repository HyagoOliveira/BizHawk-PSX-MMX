# BizHawk-PSX-MMX

A specialized BizHawk **External Tool** plugin built in C# (.NET) for **BizHawk v2.11.1**, designed to inspect, monitor and analyze memory, internal states and mechanics across the PlayStation 1 (PSX) Mega Man X trilogy:
* **Mega Man X4** (USA / JPN)
* **Mega Man X5** (USA / JPN)
* **Mega Man X6** (USA / JPN)

## Requirements

- **BizHawk Emulator:** [v2.11.1](https://github.com/TASEmulators/BizHawk/releases/tag/2.11.1) (Octoshock / Nymashock PSX core recommended).
- **.NET SDK:** A version supported by the C# Dev Kit extension. The project targets .NET Framework 4.8 and restores its reference assemblies through NuGet.
- **Visual Studio Code:** Install the recommended C# Dev Kit extension.

## Visual Studio Code

Open the repository folder in VS Code. The project expects the BizHawk installation to be in a sibling `2.11.1` folder, as in the workspace layout documented in [AGENTS.md](./AGENTS.md). If BizHawk is installed elsewhere, pass its installation directory as the `BizHawk_Home` MSBuild property, for example: `dotnet msbuild src\BizHawkPSXMMX.csproj -restore -target:LaunchBizHawk -property:BizHawk_Home="D:\Emulators\BizHawk"` (append `-property:RomPath="D:\Games\game.cue"` to launch a ROM).

Use **Terminal > Run Build Task** (or `Ctrl+Shift+B`) to compile the tool. The `.vscode` task list also provides:

- **BizHawk: Deploy** — builds and copies the DLL into BizHawk's `ExternalTools` folder.
- **BizHawk: Launch** — builds, deploys, and opens BizHawk with the tool. It prompts for an optional ROM path; leave it blank to launch without a ROM.

Build does not terminate a running emulator or launch BizHawk automatically. Close BizHawk before deploying if its loaded tool DLL prevents the file from being replaced.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
