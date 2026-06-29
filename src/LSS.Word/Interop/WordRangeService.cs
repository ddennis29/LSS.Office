using System;
using Word = Microsoft.Office.Interop.Word;

namespace LSS.Word.Interop;

/// <summary>
/// Safe Word range operations. Office COM access stays inside this project.
/// </summary>
public sealed class WordRangeService : IWordRangeService
{
    private readonly Word.Application _application;

    public WordRangeService(Word.Application application)
    {
        _application = application ?? throw new ArgumentNullException(nameof(application));
    }

    public string GetDocumentText()
    {
        if (_application.Documents.Count == 0) return string.Empty;
        return _application.ActiveDocument.Content?.Text ?? string.Empty;
    }

    public string GetText(int start, int end)
    {
        if (_application.Documents.Count == 0) return string.Empty;
        var range = _application.ActiveDocument.Range(start, end);
        try
        {
            return range.Text ?? string.Empty;
        }
        finally
        {
            if (System.Runtime.InteropServices.Marshal.IsComObject(range))
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
            }
        }
    }

    public void ReplaceText(int start, int end, string text)
    {
        if (_application.Documents.Count == 0) return;
        var range = _application.ActiveDocument.Range(start, end);
        try
        {
            range.Text = text ?? string.Empty;
        }
        finally
        {
            if (System.Runtime.InteropServices.Marshal.IsComObject(range))
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(range);
            }
        }
    }
}
