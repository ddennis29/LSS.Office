using System;

namespace LSS.Core.Commands;

/// <summary>
/// Describes a command that can be executed by the host, Ribbon, task panes, or developer tools.
/// </summary>
public sealed class CommandDescriptor
{
    public CommandDescriptor(string id, Type implementationType, string displayName, string category, string description, bool developerOnly = false)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        ImplementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
        DisplayName = displayName ?? implementationType.Name;
        Category = category ?? "General";
        Description = description ?? string.Empty;
        DeveloperOnly = developerOnly;
    }

    /// <summary>Stable command identifier used by Ribbon XML and task panes.</summary>
    public string Id { get; }

    /// <summary>Concrete command implementation type registered with dependency injection.</summary>
    public Type ImplementationType { get; }

    /// <summary>User-facing command display name.</summary>
    public string DisplayName { get; }

    /// <summary>Logical command category.</summary>
    public string Category { get; }

    /// <summary>Short command description.</summary>
    public string Description { get; }

    /// <summary>Indicates that the command is intended only for developer diagnostics.</summary>
    public bool DeveloperOnly { get; }
}
