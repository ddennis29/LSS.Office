using LSS.Core.Logging;

namespace LSS.Infrastructure.Logging;

public sealed class FileAppLogger : IAppLogger
{
    private readonly string _logFilePath;
    private readonly object _gate = new();

    public FileAppLogger(string logFilePath)
    {
        _logFilePath = logFilePath;
        Directory.CreateDirectory(Path.GetDirectoryName(_logFilePath)!);
    }

    public void Information(string messageTemplate, params object[] propertyValues) => Write("INFO", null, messageTemplate, propertyValues);
    public void Warning(string messageTemplate, params object[] propertyValues) => Write("WARN", null, messageTemplate, propertyValues);
    public void Error(Exception exception, string messageTemplate, params object[] propertyValues) => Write("ERROR", exception, messageTemplate, propertyValues);

    private void Write(string level, Exception? exception, string messageTemplate, object[] propertyValues)
    {
        var message = propertyValues.Length == 0 ? messageTemplate : string.Format(messageTemplate.Replace("{CommandName}", "{0}"), propertyValues);
        var line = $"{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss.fff zzz} [{level}] {message}";
        if (exception != null) line += Environment.NewLine + exception;
        lock (_gate) File.AppendAllText(_logFilePath, line + Environment.NewLine);
    }
}
