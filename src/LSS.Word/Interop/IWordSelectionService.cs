using LSS.Word.Models;

namespace LSS.Word.Interop;

/// <summary>
/// Provides safe access to the current Word selection.
/// </summary>
public interface IWordSelectionService
{
    string GetSelectedText();
    void ReplaceSelectedText(string text);
    void InsertText(string text);
    bool HasSelection();
    WordSelectionSnapshot GetSnapshot();
}
