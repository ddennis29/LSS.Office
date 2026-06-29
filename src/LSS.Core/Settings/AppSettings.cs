namespace LSS.Core.Settings;

public sealed class AppSettings
{
    public string EnvironmentName { get; set; } = "Production";
    public string LogLevel { get; set; } = "Information";
    public bool EnableDiagnostics { get; set; } = true;
    public string LastDocumentFolder { get; set; } = string.Empty;
}
