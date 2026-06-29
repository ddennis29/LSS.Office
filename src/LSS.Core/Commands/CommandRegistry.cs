using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LSS.Core.Commands;

/// <summary>
/// In-memory command registry used by Ribbon callbacks and future task panes.
/// </summary>
public sealed class CommandRegistry : ICommandRegistry
{
    private readonly Dictionary<string, Type> _commands = new(StringComparer.OrdinalIgnoreCase);
    private readonly Dictionary<string, CommandDescriptor> _descriptors = new(StringComparer.OrdinalIgnoreCase);

    public IReadOnlyDictionary<string, Type> RegisteredCommands => _commands;

    public IReadOnlyCollection<CommandDescriptor> Descriptors => _descriptors.Values.OrderBy(x => x.Id, StringComparer.OrdinalIgnoreCase).ToArray();

    public void Register<TCommand>(string commandId) where TCommand : ICommand
    {
        if (string.IsNullOrWhiteSpace(commandId))
        {
            throw new ArgumentException("Command id is required.", nameof(commandId));
        }

        var type = typeof(TCommand);
        var attribute = type.GetCustomAttribute<CommandAttribute>();
        var descriptor = new CommandDescriptor(
            commandId,
            type,
            attribute?.DisplayName ?? type.Name,
            attribute?.Category ?? "General",
            attribute?.Description ?? string.Empty,
            attribute?.DeveloperOnly ?? false);

        _commands[commandId] = type;
        _descriptors[commandId] = descriptor;
    }

    public void Register<TCommand>() where TCommand : ICommand
    {
        var type = typeof(TCommand);
        var attribute = type.GetCustomAttribute<CommandAttribute>()
            ?? throw new InvalidOperationException($"Command {type.FullName} does not declare CommandAttribute.");
        Register<TCommand>(attribute.Id);
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

    public bool TryGetDescriptor(string commandId, out CommandDescriptor descriptor)
    {
        if (string.IsNullOrWhiteSpace(commandId))
        {
            descriptor = null!;
            return false;
        }

        return _descriptors.TryGetValue(commandId, out descriptor!);
    }
}
