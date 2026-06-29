# LSS Office Suite

Commercial-quality Microsoft Office tooling for Legislative Services workflows.

## Sprint 1 Foundation

This repository contains the initial Visual Studio 2022 solution foundation for replacing the legacy `.docm` VBA project with a maintainable C# Word VSTO add-in.

## Requirements

- Windows
- Microsoft Word Desktop
- Visual Studio 2022
- Office/SharePoint development workload
- .NET Framework 4.8 Developer Pack

## Projects

| Project | Target | Purpose |
|---|---:|---|
| `LSS.WordAddIn` | .NET Framework 4.8 | Word VSTO host, startup, ribbon, command routing |
| `LSS.Core` | .NET Standard 2.0 | Core abstractions, commands, app settings contracts |
| `LSS.Common` | .NET Standard 2.0 | Shared primitives and constants |
| `LSS.Infrastructure` | .NET Standard 2.0 | Logging, settings, file services |
| `LSS.Word` | .NET Framework 4.8 | Word interop wrappers and document services |
| `LSS.UI` | .NET Framework 4.8 | WinForms dialogs and task pane controls |
| `LSS.Legislative` | .NET Standard 2.0 | Legislative-domain services |
| `LSS.AI` | .NET Standard 2.0 | AI integration contracts |
| `LSS.GraphQL` | .NET Standard 2.0 | GraphQL integration contracts |
| `LSS.DeveloperTools` | .NET Framework 4.8 | Internal conversion/developer utilities |
| `LSS.Tests` | .NET Framework 4.8 | Unit tests for non-COM logic |

## First test

Open `LSS.Office.sln` in Visual Studio 2022, restore NuGet packages, set `LSS.WordAddIn` as the startup project, and press F5. The Ribbon XML includes an **LSS Office** tab with a diagnostic button.
