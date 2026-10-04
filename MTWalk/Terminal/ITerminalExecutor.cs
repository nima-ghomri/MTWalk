using MTWalk.Configuration;

namespace MTWalk.Terminal;

/// <summary>
/// Interface for executing the terminal.
/// </summary>
interface ITerminalExecutor
{
    Task Execute(string configuration, CancellationToken cancellationToken = default);
    Task ExecuteByPath(string configurationPath, CancellationToken cancellationToken = default);
}
