using System.Collections.Generic;

namespace LSS.Core.Diagnostics;

public interface IDiagnosticsService
{
    IReadOnlyList<string> GetStartupReport();
}
