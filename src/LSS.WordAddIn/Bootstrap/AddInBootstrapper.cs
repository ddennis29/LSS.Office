using System;
using Microsoft.Extensions.DependencyInjection;
using LSS.Common;
using LSS.Core.Logging;
using LSS.Core.Settings;
using Word = Microsoft.Office.Interop.Word;

namespace LSS.WordAddIn.Bootstrap;

public static class AddInBootstrapper
{
    public static IServiceProvider Start(Word.Application application)
    {
        var services = new ServiceCollection();
        var baseFolder = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            ApplicationConstants.ProductName);
        var logFile = System.IO.Path.Combine(baseFolder, "Logs", DateTime.Today.ToString("yyyy-MM-dd") + ".log");
        var settingsFile = System.IO.Path.Combine(baseFolder, ApplicationConstants.SettingsFileName);

        services.AddLssOfficeServices(application, logFile, settingsFile);

        var provider = services.BuildServiceProvider();
        provider.GetRequiredService<ISettingsService>().LoadAsync().GetAwaiter().GetResult();
        provider.GetRequiredService<IAppLogger>().Information("LSS Office Suite started");
        return provider;
    }
}
