# Sprint 1.2 - Foundation Runtime Hardening

## Goal

Harden the initial foundation so Ribbon actions are routed through a command registry, diagnostics become useful for development, and Office COM access continues to move behind `LSS.Word` wrapper services.

## What Changed

### Command Registry

`ICommandRegistry` and `CommandRegistry` provide a central mapping between UI command ids and command implementation types. Ribbon callbacks now dispatch by stable command id rather than directly referencing every command type.

### Command Dispatcher

`ICommandDispatcher` now supports both generic execution and command-id execution:

```csharp
Task ExecuteAsync<TCommand>(CancellationToken cancellationToken = default);
Task ExecuteAsync(string commandId, CancellationToken cancellationToken = default);
```

### Diagnostics

The diagnostics window now uses tabs instead of one large textbox. Current tabs include:

- Runtime
- Settings
- Commands
- Word

### Word Wrapper Expansion

`IWordDocumentService` and `IWordSelectionService` were expanded so future commands can avoid direct Word COM access.

## Files of Interest

- `src/LSS.Core/Commands/ICommandRegistry.cs`
- `src/LSS.Core/Commands/CommandRegistry.cs`
- `src/LSS.Core/Commands/CommandDispatcher.cs`
- `src/LSS.Infrastructure/Diagnostics/DiagnosticsService.cs`
- `src/LSS.UI/Diagnostics/DiagnosticsForm.cs`
- `src/LSS.Word/Interop/IWordDocumentService.cs`
- `src/LSS.Word/Interop/IWordSelectionService.cs`
- `src/LSS.WordAddIn/Ribbon/RibbonCommandRouter.cs`

## Manual Test Plan

1. Open the solution in Visual Studio 2022 on Windows.
2. Restore NuGet packages.
3. Build the solution.
4. Start the Word add-in project.
5. Confirm the LSS Ribbon tab appears.
6. Click **Diagnostics** and verify tabbed diagnostics appear.
7. Click **Insert Test Text** and verify text is inserted at the current cursor position.

## Suggested Commit

```bash
git add .
git commit -m "Harden foundation runtime and command dispatching"
git push
```
