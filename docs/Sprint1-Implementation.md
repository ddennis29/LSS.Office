# Sprint 1 Implementation Notes

## Commit 1.1 - Foundation Runtime

This snapshot adds the first working runtime path for the LSS Office Suite Word add-in.

### Added

- `ICommandDispatcher` and `CommandDispatcher`
- Shared `IMessageDialogService` in `LSS.Core`
- Diagnostics service and diagnostics WinForms window
- Word document and selection wrapper services
- `ServiceRegistration` extension for DI setup
- Two end-to-end Ribbon commands:
  - Diagnostics
  - Insert Test Text

### Expected behavior

1. Open the solution in Visual Studio 2022.
2. Restore NuGet packages.
3. Build the solution.
4. Run the VSTO add-in project.
5. Word should load an `LSS` Ribbon tab.
6. Click `Diagnostics` to open the diagnostics window.
7. Click `Insert Test Text` to verify Ribbon → command → Word service flow.

### Notes

The disabled Ribbon buttons are placeholders for migrated `.docm` functionality. They are intentionally visible so the future product structure is clear, but they will not execute until their features are converted.
