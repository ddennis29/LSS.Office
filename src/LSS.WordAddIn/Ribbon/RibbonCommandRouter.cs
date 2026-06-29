using System;
using LSS.Core.Commands;
using LSS.Core.Dialogs;
using LSS.Core.Logging;
using LSS.Core.Settings;
using LSS.WordAddIn.Commands;
using Microsoft.Extensions.DependencyInjection;
using Office = Microsoft.Office.Core;

namespace LSS.WordAddIn.Ribbon;

/// <summary>
/// Routes Office Ribbon callbacks into the command dispatcher.
/// </summary>
public sealed class RibbonCommandRouter
{
    private readonly Func<IServiceProvider> _serviceProviderFactory;

    public RibbonCommandRouter(Func<IServiceProvider> serviceProviderFactory)
    {
        _serviceProviderFactory = serviceProviderFactory;
    }

    public void OnDiagnosticsClicked(Office.IRibbonControl control) => Execute(CommandIds.Diagnostics);
    public void OnInsertDiagnosticTextClicked(Office.IRibbonControl control) => Execute(CommandIds.InsertDiagnosticText);
    public void OnToggleDeveloperModeClicked(Office.IRibbonControl control) => Execute(CommandIds.ToggleDeveloperMode);
    public void OnCommandBrowserClicked(Office.IRibbonControl control) => Execute(CommandIds.CommandBrowser);
    public void OnSelectionInspectorClicked(Office.IRibbonControl control) => Execute(CommandIds.SelectionInspector);

    public bool IsEnabled(Office.IRibbonControl control)
    {
        if (control is null)
        {
            return false;
        }

        return control.Id switch
        {
            "btnDiagnostics" => true,
            "btnInsertDiagnosticText" => true,
            "btnToggleDeveloperMode" => true,
            _ => false
        };
    }

    public bool IsDeveloperEnabled(Office.IRibbonControl control)
    {
        if (control is null)
        {
            return false;
        }

        try
        {
            return _serviceProviderFactory().GetRequiredService<ISettingsService>().Current.DeveloperMode;
        }
        catch
        {
            return false;
        }
    }

    private void Execute(string commandId)
    {
        IServiceProvider? services = null;
        try
        {
            services = _serviceProviderFactory();
            var dispatcher = services.GetRequiredService<ICommandDispatcher>();
            dispatcher.ExecuteAsync(commandId).GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            services ??= _serviceProviderFactory();
            services.GetService<IAppLogger>()?.Error(ex, "Ribbon command failed: {CommandId}", commandId);
            services.GetService<IMessageDialogService>()?.ShowError(ex);
        }
    }
}
