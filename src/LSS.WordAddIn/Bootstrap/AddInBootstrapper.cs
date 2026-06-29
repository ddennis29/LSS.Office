using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using LSS.Common;
using LSS.Core.Logging;
using LSS.Core.Runtime;
using LSS.Core.Settings;
using LSS.Infrastructure;
using Word = Microsoft.Office.Interop.Word;

namespace LSS.WordAddIn.Bootstrap;

public static class AddInBootstrapper
{
    public static IServiceProvider Start(Word.Application application)
    {
        var services = new ServiceCollection();
        var environment = new HostEnvironment();
        var logFile = Path.Combine(environment.LogFolder, DateTime.Today.ToString("yyyy-MM-dd") + ".log");

        services.AddSingleton<IHostEnvironment>(environment);
        services.AddLssOfficeServices(application, logFile, environment.SettingsPath);

        var provider = services.BuildServiceProvider();
        provider.GetRequiredService<ISettingsService>().LoadAsync().GetAwaiter().GetResult();
        provider.GetRequiredService<IAppLogger>().Information("{ProductName} started", ApplicationConstants.ProductName);
        return provider;
    }
}
