namespace LSS.Word.Interop;

/// <summary>
/// Provides range-level operations for the active Word document.
/// </summary>
public interface IWordRangeService
{
    string GetDocumentText();
    string GetText(int start, int end);
    void ReplaceText(int start, int end, string text);
}
