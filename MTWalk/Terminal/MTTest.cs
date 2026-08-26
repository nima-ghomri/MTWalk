using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace MTWalk.Terminal;

interface ITerminalExecutor
{
    Task ExecuteTest(Test parameters, CancellationToken cancellationToken = default);
}

class TerminalExecutor(ITerminalProvider terminalProvider, IConfigurationBuilder configurationBuilder) : ITerminalExecutor
{
    public async Task ExecuteTest(Test parameters, CancellationToken cancellationToken = default)
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

interface IConfigurationBuilder
{
    string BuildConfiguration(Test parameters);
}

interface ITerminalProvider
{
    public string GetTerminalPath();
}

record Test(TerminalParameters TerminalParameters, WalkingParamters WalkingParameters);

record TerminalParameters(
    string Expert,
    string Symbol,
    string TimeFrame,
    DateTime Start,
    DateTime End,
    DateTime Forward,
    Delays Delays,
    Modelling Modelling,
    double Deposit,
    string Currency,
    int Leverage);

enum Delays
{
    Zero,
    Random,
    Custom,
}
enum Modelling
{
    EveryTick,
    EveryTickBasedOnRealTicks,
    OneMinuteOHLC,
    OpenPricesOnly,
    MathCalculations,
}
record WalkingParamters();