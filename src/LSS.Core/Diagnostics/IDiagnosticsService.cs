using System.Collections.Generic;

namespace LSS.Core.Diagnostics;

/// <summary>
/// Builds diagnostic snapshots for support and development troubleshooting.
/// </summary>
public interface IDiagnosticsService
{
    /// <summary>
    /// Gets the original text startup report for logging and compatibility.
    /// </summary>
    IReadOnlyList<string> GetStartupReport();

    /// <summary>
    /// Gets a structured diagnostics snapshot for the diagnostics window.
    /// </summary>
    IReadOnlyList<DiagnosticsSection> GetSnapshot();
}
