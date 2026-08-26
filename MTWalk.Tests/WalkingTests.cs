using MTWalk.Configuration;
using MTWalk.Terminal;
using MTWalk.Walking;
using MTWalk.Windowing;

namespace MTWalk.Tests;

class WalkingTests
{
    [Test]
    public async Task Walker_Start_ExecutesWithoutExceptions()
    {
        DateTime start = new(2025, 2, 1);
        DateTime end = new(2025, 2, 5);
        TimeSpan optimizationSize = TimeSpan.FromDays(7);
        TimeSpan forwardSize = TimeSpan.FromDays(2);
        var testerParameters = new TesterParameters(
                Expert: @"Examples\MACD\MACD Sample.ex5",
                Symbol: "EURUSD",
                TimeFrame: TimeFrame.H1,
                Start: start,
                End: end,
                ForwardMode: ForwardMode.Custom,
                ForwardDate: default,
                ExecutionMode: ExecutionMode.ZeroDelay,
                Model: Model.OneMinuteOHLC,
                Deposit: 1000.0,
                Currency: "USD",
                Leverage: 100,
                Optimization: Optimization.FastGeneticAlgorithm,
                OptimizationCriterion: OptimizationCriterion.BalanceMax
            );
        var windowingRequest = new RollingWindowingRequest(
                start,
                end,
                optimizationSize,
                forwardSize);
        // Arrange
        var terminalExecuter = new TerminalExecutor(new DummyTerminalProvider(), new TesterConfigurationBuilder());
        var walker = new Walker(new RollingWindowingStrategy(), terminalExecuter);
        // Act & Assert
        Assert.DoesNotThrowAsync(async () => await walker.Start(windowingRequest, testerParameters));
    }

    class DummyTerminalProvider : ITerminalProvider
    {
        public string GetTerminalPath() => @"C:\Program Files\MetaTrader 5\terminal64.exe";
    }
}
