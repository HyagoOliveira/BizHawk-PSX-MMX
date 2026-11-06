# BizHawk-PSX-MMX

A specialized BizHawk **External Tool** plugin built in C# (.NET) for **BizHawk v2.11.1**, designed to inspect, monitor and analyze memory, internal states and mechanics across the PlayStation 1 (PSX) Mega Man X trilogy:
* **Mega Man X4** (USA / JPN)
* **Mega Man X5** (USA / JPN)
* **Mega Man X6** (USA / JPN)

## Requirements

- **BizHawk Emulator:** [v2.11.1](https://github.com/TASEmulators/BizHawk/releases/tag/2.11.1)
- **.NET SDK:** A version supported by the C# Dev Kit extension. The project targets .NET Framework 4.8 and restores its reference assemblies through NuGet.
- **Visual Studio Code:** Install the recommended C# Dev Kit extension.

## Visual Studio Code

Open the repository folder in VS Code. The project expects the BizHawk installation to be in a sibling `2.11.1` folder, as in the workspace layout documented in [AGENTS.md](./AGENTS.md). If BizHawk is installed elsewhere, pass its installation directory as the `BizHawk_Home` MSBuild property, for example: `dotnet msbuild src\BizHawkPSXMMX.csproj -restore -target:LaunchBizHawk -property:BizHawk_Home="D:\Emulators\BizHawk"` (append `-property:RomPath="D:\Games\game.cue"` to launch a ROM).

### Build

Press `Ctrl+Shift+B` to build, deploy the tool, and launch BizHawk in one step. VS Code prompts for an optional ROM path; leave it blank to start without a ROM.

To compile without deploying or launching BizHawk, open the Command Palette with `Ctrl+Shift+P`, select **Tasks: Run Task**, then choose **Build only**. You can also run this command from the integrated terminal at the repository root:

```powershell
dotnet build .\src\BizHawkPSXMMX.csproj
```

The `.vscode` task list also provides:

- **BizHawk: Deploy** — builds and copies the DLL into BizHawk's `ExternalTools` folder.
- **Build, Deploy and Launch** — the default build task, which runs all steps above.

Before deployment, the workflow closes the `EmuHawk.exe` instance from the configured BizHawk installation. It first requests a normal shutdown and waits up to 10 seconds; if the emulator does not close, it forcefully stops that process so the DLL can be replaced. Other BizHawk installations are not affected.

## License

This project is licensed under the MIT License - see the LICENSE file for details.
