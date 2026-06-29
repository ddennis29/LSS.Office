namespace LSS.Word.Interop;

public interface IWordApplicationService
{
    string GetActiveDocumentName();
    string GetSelectionText();
    void InsertTextAtSelection(string text);
}
