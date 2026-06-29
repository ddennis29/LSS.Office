using System;
using System.Collections.Generic;

namespace LSS.Core.Commands;

/// <summary>
/// Resolves user-interface command identifiers to concrete command types.
/// </summary>
public interface ICommandRegistry
{
    /// <summary>
    /// Gets every registered command mapping.
    /// </summary>
    IReadOnlyDictionary<string, Type> RegisteredCommands { get; }

    /// <summary>
    /// Registers a command type for a stable command identifier.
    /// </summary>
    /// <typeparam name="TCommand">The command implementation type.</typeparam>
    /// <param name="commandId">The Ribbon or UI command identifier.</param>
    void Register<TCommand>(string commandId) where TCommand : ICommand;

    /// <summary>
    /// Attempts to resolve a command identifier to a command implementation type.
    /// </summary>
    bool TryResolve(string commandId, out Type commandType);
}
