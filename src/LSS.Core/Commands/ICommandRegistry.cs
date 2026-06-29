using System;
using System.Collections.Generic;

namespace LSS.Core.Commands;

/// <summary>
/// Resolves user-interface command identifiers to concrete command metadata and implementation types.
/// </summary>
public interface ICommandRegistry
{
    /// <summary>Gets every registered command mapping.</summary>
    IReadOnlyDictionary<string, Type> RegisteredCommands { get; }

    /// <summary>Gets every registered command descriptor.</summary>
    IReadOnlyCollection<CommandDescriptor> Descriptors { get; }

    /// <summary>Registers a command type for a stable command identifier.</summary>
    void Register<TCommand>(string commandId) where TCommand : ICommand;

    /// <summary>Registers a command type using metadata declared by <see cref="CommandAttribute"/>.</summary>
    void Register<TCommand>() where TCommand : ICommand;

    /// <summary>Attempts to resolve a command identifier to a command implementation type.</summary>
    bool TryResolve(string commandId, out Type commandType);

    /// <summary>Attempts to resolve a command identifier to a command descriptor.</summary>
    bool TryGetDescriptor(string commandId, out CommandDescriptor descriptor);
}
