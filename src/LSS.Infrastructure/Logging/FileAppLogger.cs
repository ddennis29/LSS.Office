using System;
using System.IO;
using System.Text.RegularExpressions;
using LSS.Core.Logging;

namespace LSS.Infrastructure.Logging;

/// <summary>
/// Lightweight file logger used before a full logging pipeline is introduced.
/// </summary>
public sealed class FileAppLogger : IAppLogger
{
    private static readonly Regex NamedTokenRegex = new Regex("\\{[A-Za-z_][A-Za-z0-9_]*\\}", RegexOptions.Compiled);
    private readonly string _logFilePath;
    private readonly object _gate = new object();

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
        var message = Format(messageTemplate, propertyValues);
        var line = $"{DateTimeOffset.Now:yyyy-MM-dd HH:mm:ss.fff zzz} [{level}] {message}";
        if (exception != null)
        {
            line += Environment.NewLine + exception;
        }

        lock (_gate)
        {
            File.AppendAllText(_logFilePath, line + Environment.NewLine);
        }
    }

    private static string Format(string messageTemplate, object[] propertyValues)
    {
        if (propertyValues.Length == 0)
        {
            return messageTemplate;
        }

        var index = 0;
        var formatted = NamedTokenRegex.Replace(messageTemplate, _ => "{" + index++ + "}");
        try
        {
            return string.Format(formatted, propertyValues);
        }
        catch (FormatException)
        {
            return messageTemplate + " " + string.Join(", ", propertyValues);
        }
    }
}
