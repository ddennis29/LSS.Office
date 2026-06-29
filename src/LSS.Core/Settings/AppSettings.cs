namespace LSS.Core.Settings;

/// <summary>
/// User-specific application settings persisted as JSON under LocalAppData.
/// </summary>
public sealed class AppSettings
{
    public string EnvironmentName { get; set; } = "Production";
    public string LogLevel { get; set; } = "Information";
    public bool EnableDiagnostics { get; set; } = true;
    public bool DeveloperMode { get; set; } = true;
    public string LastDocumentFolder { get; set; } = string.Empty;
    public string LastOpenedDocument { get; set; } = string.Empty;
}
