using System;
using System.IO;
using LSS.Common;
using LSS.Core.Runtime;

namespace LSS.Infrastructure;

/// <summary>
/// Default runtime environment for the Word add-in host.
/// </summary>
public sealed class HostEnvironment : IHostEnvironment
{
    public HostEnvironment()
    {
        ProductName = ApplicationConstants.ProductName;
        Version = ApplicationConstants.Version;
        BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), ProductName);
        LogFolder = Path.Combine(BaseFolder, "Logs");
        SettingsPath = Path.Combine(BaseFolder, ApplicationConstants.SettingsFileName);
        Directory.CreateDirectory(BaseFolder);
        Directory.CreateDirectory(LogFolder);
    }

    public string ProductName { get; }
    public string Version { get; }
    public string BaseFolder { get; }
    public string LogFolder { get; }
    public string SettingsPath { get; }
}
