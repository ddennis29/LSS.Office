namespace LSS.Core.Runtime;

/// <summary>
/// Provides host paths and runtime details used by infrastructure services.
/// </summary>
public interface IHostEnvironment
{
    string ProductName { get; }
    string Version { get; }
    string BaseFolder { get; }
    string LogFolder { get; }
    string SettingsPath { get; }
}
