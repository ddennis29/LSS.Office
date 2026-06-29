namespace LSS.Word.Interop;

public interface IWordSelectionService
{
    string GetSelectedText();
    void ReplaceSelectedText(string text);
}
