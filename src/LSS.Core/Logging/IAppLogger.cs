using System;

namespace LSS.Core.Logging;

/// <summary>
/// Application logging abstraction used by core services and commands.
/// </summary>
public interface IAppLogger
{
    void Information(string messageTemplate, params object[] propertyValues);
    void Warning(string messageTemplate, params object[] propertyValues);
    void Error(Exception exception, string messageTemplate, params object[] propertyValues);
}
