# Changelog

## 0.1.0-foundation

- Sprint 1 foundation solution.

## Sprint 1.2 - Foundation Runtime Hardening

### Added
- Command registry for mapping Ribbon command ids to command implementation types.
- Command dispatcher overload for executing commands by string command id.
- Structured diagnostics sections and tabbed diagnostics window.
- Expanded Word document wrapper with document count, table count, and active document detection.
- Expanded Word selection wrapper with insert text support.
- Stable command id constants for Ribbon-to-command routing.

### Changed
- Ribbon command router now dispatches through command ids instead of direct generic calls.
- Diagnostics command now inherits from `CommandBase` and shows runtime, settings, commands, and Word state tabs.
- Insert diagnostic text command now uses the command base logging pipeline.
- Settings model now includes developer mode and last opened document fields.
- JSON settings service now uses `StreamReader`/`StreamWriter` for .NET Standard 2.0 compatibility.

### Known Issues
- This package was generated in a Linux container without the .NET SDK or Visual Studio/VSTO targets installed, so the solution could not be locally compiled here. It is structured for Visual Studio 2022 on Windows.
