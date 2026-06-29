using System;
using System.IO;
using System.Reflection;
using Microsoft.Office.Core;

namespace LSS.WordAddIn.Ribbon;

public sealed class LssRibbon : IRibbonExtensibility
{
    private readonly Func<IServiceProvider> _serviceProviderFactory;
    private IRibbonUI? _ribbon;

    public LssRibbon(Func<IServiceProvider> serviceProviderFactory)
    {
        _serviceProviderFactory = serviceProviderFactory;
    }

    public string GetCustomUI(string ribbonId)
    {
        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("LSS.WordAddIn.Ribbon.LssRibbon.xml");
        if (stream == null) return string.Empty;
        using var reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    public void OnLoad(IRibbonUI ribbonUi) => _ribbon = ribbonUi;

    public void OnDiagnostics(IRibbonControl control)
    {
        RibbonCommandRouter.Execute<Commands.DiagnosticsCommand>(_serviceProviderFactory());
    }
}
