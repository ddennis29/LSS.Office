namespace LSS.Core.Commands;

public interface ICommand
{
    string Name { get; }
    Task ExecuteAsync(CancellationToken cancellationToken = default);
}
