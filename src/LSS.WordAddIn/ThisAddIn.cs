using System;
using Microsoft.Extensions.DependencyInjection;
using LSS.WordAddIn.Bootstrap;

namespace LSS.WordAddIn;

public partial class ThisAddIn
{
    private IServiceProvider? _services;

    public IServiceProvider Services => _services ?? throw new InvalidOperationException("Add-in has not started.");

    private void ThisAddIn_Startup(object sender, EventArgs e)
    {
        _services = AddInBootstrapper.Start(Application);
    }

    private void ThisAddIn_Shutdown(object sender, EventArgs e)
    {
        if (_services is IDisposable disposable) disposable.Dispose();
    }

    protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
    {
        return new Ribbon.LssRibbon(() => Services);
    }

    #region VSTO generated code placeholder
    private void InternalStartup()
    {
        this.Startup += new EventHandler(ThisAddIn_Startup);
        this.Shutdown += new EventHandler(ThisAddIn_Shutdown);
    }
    #endregion
}
