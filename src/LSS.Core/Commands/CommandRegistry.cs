using System;
using System.Collections.Generic;

namespace LSS.Core.Commands;

/// <summary>
/// In-memory command registry used by Ribbon callbacks and future task panes.
/// </summary>
public sealed class CommandRegistry : ICommandRegistry
{
    private readonly Dictionary<string, Type> _commands = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyDictionary<string, Type> RegisteredCommands => _commands;

    public void Register<TCommand>(string commandId) where TCommand : ICommand
    {
        if (string.IsNullOrWhiteSpace(commandId))
        {
            throw new ArgumentException("Command id is required.", nameof(commandId));
        }

        _commands[commandId] = typeof(TCommand);
    }

    public bool TryResolve(string commandId, out Type commandType)
    {
        if (string.IsNullOrWhiteSpace(commandId))
        {
            commandType = typeof(ICommand);
            return false;
        }

        return _commands.TryGetValue(commandId, out commandType!);
    }
}
