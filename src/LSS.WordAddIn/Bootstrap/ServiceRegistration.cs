using LSS.Core.Commands;
using LSS.Core.Diagnostics;
using LSS.Core.Dialogs;
using LSS.Core.Logging;
using LSS.Core.Settings;
using LSS.Infrastructure.Diagnostics;
using LSS.Infrastructure.Logging;
using LSS.Infrastructure.Settings;
using LSS.UI.Dialogs;
using LSS.Word.Diagnostics;
using LSS.Word.Interop;
using LSS.WordAddIn.Commands;
using Microsoft.Extensions.DependencyInjection;
using Word = Microsoft.Office.Interop.Word;

namespace LSS.WordAddIn.Bootstrap;

/// <summary>
/// Central dependency registration for the Word add-in host.
/// </summary>
public static class ServiceRegistration
{
    public static IServiceCollection AddLssOfficeServices(
        this IServiceCollection services,
        Word.Application application,
        string logFile,
        string settingsFile)
    {
        services.AddSingleton(application);
        services.AddSingleton<IAppLogger>(_ => new FileAppLogger(logFile));
        services.AddSingleton<ISettingsService>(_ => new JsonSettingsService(settingsFile));
        services.AddSingleton<ICommandRegistry>(_ => BuildCommandRegistry());
        services.AddSingleton<ICommandDispatcher, CommandDispatcher>();
        services.AddSingleton<IDiagnosticsService, DiagnosticsService>();
        services.AddSingleton<IWordApplicationService, WordApplicationService>();
        services.AddSingleton<IWordDocumentService, WordDocumentService>();
        services.AddSingleton<IWordSelectionService, WordSelectionService>();
        services.AddSingleton<IWordRangeService, WordRangeService>();
        services.AddSingleton<IWordDiagnosticsService, WordDiagnosticsService>();
        services.AddSingleton<IMessageDialogService, MessageDialogService>();
        services.AddTransient<DiagnosticsCommand>();
        services.AddTransient<InsertDiagnosticTextCommand>();
        services.AddTransient<ToggleDeveloperModeCommand>();
        services.AddTransient<ShowCommandBrowserCommand>();
        services.AddTransient<ShowSelectionInspectorCommand>();
        return services;
    }

    private static ICommandRegistry BuildCommandRegistry()
    {
        var registry = new CommandRegistry();
        registry.Register<DiagnosticsCommand>();
        registry.Register<InsertDiagnosticTextCommand>();
        registry.Register<ToggleDeveloperModeCommand>();
        registry.Register<ShowCommandBrowserCommand>();
        registry.Register<ShowSelectionInspectorCommand>();
        return registry;
    }
}
