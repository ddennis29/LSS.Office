using System.Collections.Generic;

namespace LSS.Word.Diagnostics;

/// <summary>
/// Builds Word-specific diagnostics without leaking COM objects outside the Word SDK.
/// </summary>
public interface IWordDiagnosticsService
{
    IReadOnlyList<string> GetDocumentLines();
    IReadOnlyList<string> GetSelectionLines();
    IReadOnlyList<string> GetApplicationLines();
}
