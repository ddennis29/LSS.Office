namespace LSS.Word.Models;

/// <summary>
/// Immutable summary of the active Word selection.
/// </summary>
public sealed class WordSelectionSnapshot
{
    public WordSelectionSnapshot(bool hasSelection, string text, int start, int end, int storyType)
    {
        HasSelection = hasSelection;
        Text = text;
        Start = start;
        End = end;
        StoryType = storyType;
    }

    public bool HasSelection { get; }
    public string Text { get; }
    public int Start { get; }
    public int End { get; }
    public int StoryType { get; }
    public int Length => End - Start;
}
