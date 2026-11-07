# Agent Instructions

This repository develops External Tools targeting BizHawk v2.11.1

## General

- Be direct and pragmatic when explaining or reviewing changes.
- Write source code, comments, identifiers, commit messages, and documentation in English.

## C# Conventions

- Follow [CSharpStyleGuide.md](CSharpStyleGuide.md).

## BizHawk and Game Data

- Check BizHawk API contracts and implementations before relying on API behavior. The API source locations and detailed game research are in [PSX game memory and research](docs/reference/psx-game-memory-research.md).
- The analyzer targets USA releases of Mega Man X4, X5, and X6. Keep each game's addresses separate in `src/Memory/GameMemoryAddress.cs`.
- Treat `ROOMS_Extractions/` as local reference data. Do not modify or add its extracted game files to Git.
