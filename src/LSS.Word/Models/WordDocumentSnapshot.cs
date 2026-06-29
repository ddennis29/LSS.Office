namespace LSS.Word.Models;

/// <summary>
/// Immutable summary of the currently active Word document.
/// </summary>
public sealed class WordDocumentSnapshot
{
    public WordDocumentSnapshot(bool hasDocument, string name, string fullName, int paragraphCount, int tableCount, int wordCount, bool saved)
    {
        HasDocument = hasDocument;
        Name = name;
        FullName = fullName;
        ParagraphCount = paragraphCount;
        TableCount = tableCount;
        WordCount = wordCount;
        Saved = saved;
    }

    public bool HasDocument { get; }
    public string Name { get; }
    public string FullName { get; }
    public int ParagraphCount { get; }
    public int TableCount { get; }
    public int WordCount { get; }
    public bool Saved { get; }
}
