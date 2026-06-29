using System;
using System.Collections.Generic;
using LSS.Word.Interop;
using Word = Microsoft.Office.Interop.Word;

namespace LSS.Word.Diagnostics;

/// <summary>
/// Default Word diagnostics implementation.
/// </summary>
public sealed class WordDiagnosticsService : IWordDiagnosticsService
{
    private readonly Word.Application _application;
    private readonly IWordDocumentService _documentService;
    private readonly IWordSelectionService _selectionService;

    public WordDiagnosticsService(Word.Application application, IWordDocumentService documentService, IWordSelectionService selectionService)
    {
        _application = application ?? throw new ArgumentNullException(nameof(application));
        _documentService = documentService ?? throw new ArgumentNullException(nameof(documentService));
        _selectionService = selectionService ?? throw new ArgumentNullException(nameof(selectionService));
    }

    public IReadOnlyList<string> GetApplicationLines() => new[]
    {
        $"Word version: {_application.Version}",
        $"Caption: {_application.Caption}",
        $"Documents: {_documentService.GetDocumentCount()}",
        $"Visible: {_application.Visible}"
    };

    public IReadOnlyList<string> GetDocumentLines() => new[]
    {
        $"Has document: {_documentService.HasActiveDocument()}",
        $"Name: {_documentService.GetActiveDocumentName() ?? "No active document"}",
        $"Path: {_documentService.GetActiveDocumentPath() ?? "No active document"}",
        $"Paragraphs: {_documentService.GetParagraphCount()}",
        $"Tables: {_documentService.GetTableCount()}"
    };

    public IReadOnlyList<string> GetSelectionLines() => new[]
    {
        $"Has selection: {_selectionService.HasSelection()}",
        $"Selected text length: {_selectionService.GetSelectedText().Length}",
        $"Preview: {TrimPreview(_selectionService.GetSelectedText())}"
    };

    private static string TrimPreview(string value)
    {
        value = (value ?? string.Empty).Replace("\r", " ").Replace("\n", " ").Trim();
        return value.Length <= 120 ? value : value.Substring(0, 120) + "...";
    }
}
