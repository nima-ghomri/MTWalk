using MTWalk.Configuration;

namespace MTWalk.Terminal;

/// <summary>
/// Interface for executing the terminal.
/// </summary>
interface ITerminalExecutor
{
    Task Execute(IConfigurationParameters parameters, CancellationToken cancellationToken = default);
}
