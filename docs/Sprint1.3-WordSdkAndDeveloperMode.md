# Sprint 1.3 - Word SDK Boundary and Developer Mode

## Summary

Sprint 1.3 strengthens the foundation by expanding the Word abstraction layer and introducing developer-mode diagnostics that will be used during the `.docm` migration.

## Added

- Command metadata via `CommandAttribute` and `CommandDescriptor`.
- Registry support for command descriptors and attribute-based registration.
- Host environment abstraction for LocalAppData, logs, and settings paths.
- Word SDK models for active document and selection snapshots.
- Word range service for controlled document range operations.
- Word diagnostics service for application, document, and selection diagnostics.
- Developer Ribbon group with:
  - Command Browser
  - Selection Inspector
  - Toggle Developer Mode
- GitHub Actions build workflow for Windows/MSBuild.

## Changed

- Diagnostics now includes runtime paths, command metadata, and Word-specific sections.
- Startup now registers `IHostEnvironment` and uses the environment paths for settings/logs.
- Command registration now uses command attributes instead of only hard-coded IDs.
- Application version updated to `1.3.0`.

## Architecture Rule

Only `LSS.Word` and the VSTO host should touch Office COM objects directly. Feature projects should depend on interfaces from `LSS.Word`, not `Microsoft.Office.Interop.Word`.

## Known Issues

- The Ribbon still uses XML callbacks instead of a fully dynamic Ribbon builder.
- Developer-mode changes may require reopening the Ribbon/Add-in before `getEnabled` refreshes visually.
- The VSTO project must be built on Windows with Visual Studio/Office tooling.

## Next Sprint

Sprint 1.4 should focus on the `.docm` discovery tool and conversion tracker automation.
