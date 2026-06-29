using System;

namespace LSS.Core.Commands;

/// <summary>
/// Declares command metadata on an <see cref="ICommand"/> implementation.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class CommandAttribute : Attribute
{
    public CommandAttribute(string id, string displayName, string category)
    {
        Id = id;
        DisplayName = displayName;
        Category = category;
    }

    public string Id { get; }
    public string DisplayName { get; }
    public string Category { get; }
    public string Description { get; set; } = string.Empty;
    public bool DeveloperOnly { get; set; }
}
