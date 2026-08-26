using MTWalk.Configuration;
using System.Diagnostics;

namespace MTWalk.Terminal;

/// <summary>
/// Implementation of ITerminalExecutor that executes the terminal with the provided configuration parameters.
/// </summary>
/// <param name="terminalProvider"></param>
/// <param name="configurationBuilder"></param>
class TerminalExecutor(ITerminalProvider terminalProvider, IConfigurationBuilder configurationBuilder) : ITerminalExecutor
{
    public async Task Execute(IConfigurationParameters parameters, CancellationToken cancellationToken = default)
    {

        var processInfo = new ProcessStartInfo
        {
            FileName = terminalProvider.GetTerminalPath(),
            Arguments = $"/config:\"{configurationBuilder.BuildConfiguration(parameters)}\"",
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(processInfo)
            ?? throw new InvalidOperationException("Failed to start the terminal process.");
        await Task.Run(process.WaitForExit, cancellationToken);
    }
}
