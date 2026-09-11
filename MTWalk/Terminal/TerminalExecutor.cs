using MTWalk.Configuration;
using System.Diagnostics;

namespace MTWalk.Terminal;

/// <summary>
/// Implementation of ITerminalExecutor that executes the terminal with the provided configuration parameters.
/// </summary>
/// <param name="terminalProvider"></param>
/// <param name="configurationBuilder"></param>
class TerminalExecutor(ITerminalProvider terminalProvider) : ITerminalExecutor
{
    public async Task Execute(string configuration, CancellationToken cancellationToken = default)
    {
        var configurationPath = Path.Combine(Path.GetTempPath(), "configuration.ini");
        File.WriteAllText(configurationPath, configuration);
        var processInfo = new ProcessStartInfo
        {
            FileName = terminalProvider.GetTerminalPath(),
            Arguments = $"/config:\"{configurationPath}\"",
            WindowStyle = ProcessWindowStyle.Minimized,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        using var process = Process.Start(processInfo)
            ?? throw new InvalidOperationException("Failed to start the terminal process.");

        try
        {
            await Task.Run(process.WaitForExit, cancellationToken);
        }
        finally
        {
            process.Kill();
        }
    }
}
