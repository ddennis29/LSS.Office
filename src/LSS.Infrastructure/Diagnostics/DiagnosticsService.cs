using System;
using System.Collections.Generic;
using System.Linq;
using LSS.Common;
using LSS.Core.Commands;
using LSS.Core.Diagnostics;
using LSS.Core.Runtime;
using LSS.Core.Settings;

namespace LSS.Infrastructure.Diagnostics;

/// <summary>
/// Builds runtime diagnostics for the LSS Office Suite host.
/// </summary>
public sealed class DiagnosticsService : IDiagnosticsService
{
    private readonly ISettingsService _settingsService;
    private readonly ICommandRegistry _commandRegistry;
    private readonly IHostEnvironment _hostEnvironment;

    public DiagnosticsService(ISettingsService settingsService, ICommandRegistry commandRegistry, IHostEnvironment hostEnvironment)
    {
        _settingsService = settingsService;
        _commandRegistry = commandRegistry;
        _hostEnvironment = hostEnvironment;
    }

    public IReadOnlyList<string> GetStartupReport()
    {
        return GetSnapshot().First(section => section.Title == "Runtime").Lines;
    }

    public IReadOnlyList<DiagnosticsSection> GetSnapshot()
    {
        var settings = _settingsService.Current;
        return new[]
        {
            new DiagnosticsSection("Runtime", new[]
            {
                $"Product: {ApplicationConstants.ProductName}",
                $"Version: {ApplicationConstants.Version}",
                $"Machine: {Environment.MachineName}",
                $"User: {Environment.UserName}",
                $"OS: {Environment.OSVersion}",
                $"CLR: {Environment.Version}",
                $"64-bit process: {Environment.Is64BitProcess}",
                $"Started: {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
            }),
            new DiagnosticsSection("Paths", new[]
            {
                $"Base folder: {_hostEnvironment.BaseFolder}",
                $"Log folder: {_hostEnvironment.LogFolder}",
                $"Settings: {_hostEnvironment.SettingsPath}"
            }),
            new DiagnosticsSection("Settings", new[]
            {
                $"Settings loaded: {settings is not null}",
                $"Developer mode: {settings?.DeveloperMode ?? false}",
                $"Diagnostics enabled: {settings?.EnableDiagnostics ?? false}",
                $"Log level: {settings?.LogLevel ?? "Default"}",
                $"Last opened document: {settings?.LastOpenedDocument ?? "None"}"
            }),
            new DiagnosticsSection("Commands", _commandRegistry.Descriptors
                .OrderBy(command => command.Id, StringComparer.OrdinalIgnoreCase)
                .Select(command => $"{command.Id} -> {command.ImplementationType.FullName} | {command.Category} | DeveloperOnly={command.DeveloperOnly}"))
        };
    }
}
