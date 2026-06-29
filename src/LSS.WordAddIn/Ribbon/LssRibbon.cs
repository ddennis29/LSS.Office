using System;
using System.IO;
using System.Reflection;
using Office = Microsoft.Office.Core;

namespace LSS.WordAddIn.Ribbon;

public sealed class LssRibbon : Office.IRibbonExtensibility
{
    private readonly RibbonCommandRouter _router;

    public LssRibbon(Func<IServiceProvider> serviceProviderFactory)
    {
        _router = new RibbonCommandRouter(serviceProviderFactory);
    }

    public string GetCustomUI(string ribbonId)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("LSS.WordAddIn.Ribbon.LssRibbon.xml");
        if (stream is null) return string.Empty;
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public void OnDiagnosticsClicked(Office.IRibbonControl control) => _router.OnDiagnosticsClicked(control);
    public void OnInsertDiagnosticTextClicked(Office.IRibbonControl control) => _router.OnInsertDiagnosticTextClicked(control);
    public void OnToggleDeveloperModeClicked(Office.IRibbonControl control) => _router.OnToggleDeveloperModeClicked(control);
    public void OnCommandBrowserClicked(Office.IRibbonControl control) => _router.OnCommandBrowserClicked(control);
    public void OnSelectionInspectorClicked(Office.IRibbonControl control) => _router.OnSelectionInspectorClicked(control);
    public bool IsEnabled(Office.IRibbonControl control) => _router.IsEnabled(control);
    public bool IsDeveloperEnabled(Office.IRibbonControl control) => _router.IsDeveloperEnabled(control);
}
