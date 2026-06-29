using System;
using System.Collections.Generic;
using LSS.Common;
using LSS.Core.Diagnostics;
using LSS.Core.Settings;

namespace LSS.Infrastructure.Diagnostics;

public sealed class DiagnosticsService : IDiagnosticsService
{
    private readonly ISettingsService _settingsService;

    public DiagnosticsService(ISettingsService settingsService)
    {
        _settingsService = settingsService;
    }

    public IReadOnlyList<string> GetStartupReport()
    {
        return new[]
        {
            $"Product: {ApplicationConstants.ProductName}",
            $"Version: {ApplicationConstants.Version}",
            $"Machine: {Environment.MachineName}",
            $"User: {Environment.UserName}",
            $"OS: {Environment.OSVersion}",
            $"CLR: {Environment.Version}",
            $"Settings loaded: {_settingsService.Current is not null}",
            $"Started: {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
        };
    }
}
