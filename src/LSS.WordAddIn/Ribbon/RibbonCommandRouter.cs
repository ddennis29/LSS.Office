using System;
using System.Threading.Tasks;
using LSS.Core.Commands;
using LSS.Core.Dialogs;
using LSS.WordAddIn.Commands;
using Microsoft.Extensions.DependencyInjection;
using Office = Microsoft.Office.Core;

namespace LSS.WordAddIn.Ribbon;

public sealed class RibbonCommandRouter
{
    private readonly Func<IServiceProvider> _serviceProviderFactory;

    public RibbonCommandRouter(Func<IServiceProvider> serviceProviderFactory)
    {
        _serviceProviderFactory = serviceProviderFactory;
    }

    public void OnDiagnosticsClicked(Office.IRibbonControl control)
    {
        Execute<DiagnosticsCommand>();
    }

    public void OnInsertDiagnosticTextClicked(Office.IRibbonControl control)
    {
        Execute<InsertDiagnosticTextCommand>();
    }

    public bool IsEnabled(Office.IRibbonControl control)
    {
        return true;
    }

    private void Execute<TCommand>() where TCommand : ICommand
    {
        try
        {
            var services = _serviceProviderFactory();
            var dispatcher = services.GetRequiredService<ICommandDispatcher>();
            dispatcher.ExecuteAsync<TCommand>().GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            var dialog = _serviceProviderFactory().GetService<IMessageDialogService>();
            dialog?.ShowError(ex);
        }
    }
}
