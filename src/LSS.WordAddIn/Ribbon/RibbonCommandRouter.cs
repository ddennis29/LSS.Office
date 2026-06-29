using System;
using Microsoft.Extensions.DependencyInjection;
using LSS.Core.Commands;
using LSS.Core.Logging;
using LSS.UI.Dialogs;

namespace LSS.WordAddIn.Ribbon;

public static class RibbonCommandRouter
{
    public static void Execute<TCommand>(IServiceProvider provider) where TCommand : ICommand
    {
        try
        {
            var command = provider.GetRequiredService<TCommand>();
            command.ExecuteAsync().GetAwaiter().GetResult();
        }
        catch (Exception ex)
        {
            provider.GetService<IAppLogger>()?.Error(ex, "Ribbon command failed");
            provider.GetService<IMessageDialogService>()?.ShowError("LSS Office Suite", ex.Message);
        }
    }
}
