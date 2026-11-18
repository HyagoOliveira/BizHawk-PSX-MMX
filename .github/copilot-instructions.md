# Agent Instructions

This repository develops an External Tool for BizHawk that analyzes the USA releases of Mega Man X4, X5, and X6 on PSX. It currently targets BizHawk v2.11.1, but the target version may change as newer BizHawk releases become available.

## General

- Be direct and pragmatic.
- Write code, comments, identifiers, commit messages, and documentation in English.
- Follow [C# style guide](../docs/csharp-style-guide.md).
- Keep MMX4, MMX5, and MMX6 addresses, layouts, and behavior separate.

## BizHawk

When implementing or diagnosing emulator behavior, verify the contract against the currently targeted BizHawk release (v2.11.1 until the project updates it):

- [API interfaces](https://github.com/TASEmulators/BizHawk/tree/2.11.1/src/BizHawk.Client.Common/Api/Interfaces): public `ApiContainer` services and method behavior.
- [API implementations](https://github.com/TASEmulators/BizHawk/tree/2.11.1/src/BizHawk.Client.Common/Api/Classes): concrete behavior and memory-domain handling.
- [External tool examples](https://github.com/TASEmulators/BizHawk/tree/2.11.1/ExternalToolProjects): canonical implementation patterns.

Use `MainRAM` offsets for PSX memory, not host-process addresses or executable addresses.

## Game Research

Use these matching-decompilation repositories as references for reconstructed C/assembly, object layouts, routines, animations, and behavior:

- MMX4 USA: https://github.com/HyagoOliveira/mmx4
- MMX5 USA (`SLUS-01334`): https://github.com/HyagoOliveira/mmx5
- MMX6 USA v1.1 (`SLUS-01395`): https://github.com/HyagoOliveira/mmx6

These are reverse-engineered forks, not original source code. Treat reconstructed names, comments, and structures as uncertain; validate addresses and conclusions against save states or live gameplay.

Repository-specific memory findings, layouts, and known limitations are documented in [PSX game memory reference](../docs/reference/psx-memory-reference.md). Keep extracted game data under `ROOMS_Extractions/`, ignore it in Git, and never treat it as a validated memory map.
