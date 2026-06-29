using System.Collections.Generic;

namespace LSS.Core.Diagnostics;

/// <summary>
/// Represents a named diagnostics section displayed in the developer diagnostics window.
/// </summary>
public sealed class DiagnosticsSection
{
    public DiagnosticsSection(string title, IEnumerable<string> lines)
    {
        Title = title;
        Lines = new List<string>(lines);
    }

    public string Title { get; }

    public IReadOnlyList<string> Lines { get; }
}
