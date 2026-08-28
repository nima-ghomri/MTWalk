using MTWalk.Configuration;
using MTWalk.MetaQuotes;
using MTWalk.Reporting;
using MTWalk.Terminal;
using MTWalk.Walking;
using MTWalk.Windowing;

namespace MTWalk.Tests;

class WalkingTests
{
    [Test]
    public async Task Walker_Start_ExecutesWithoutExceptions()
    {
        DateTime start = new(2025, 2, 3);
        DateTime end = new(2025, 2, 7);
        TimeSpan optimizationSize = TimeSpan.FromDays(7);
        TimeSpan forwardSize = TimeSpan.FromDays(2);
        var testerRequest = new TesterRequest(
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
        var windowingRequest = new RollingWindowingRequest(optimizationSize, forwardSize);
        // Arrange
        ReportProvider reportProvider = new(new TerminalProvider(), new DummyReportParser());
        TerminalExecutor terminalExecuter = new(new DummyTerminalProvider(), new TesterConfigurationBuilder());
        RollingWindowingStrategy rollingWindowingStrategy = new();
        TesterConfigurationParametersProvider configurationParametersProvider = new(reportProvider);
        var walker = new Walker(rollingWindowingStrategy, terminalExecuter, configurationParametersProvider);
        // Act & Assert
        Assert.DoesNotThrowAsync(async () => await walker.Start(windowingRequest, testerRequest));
    }

    class DummyTerminalProvider : ITerminalProvider
    {
        public string GetDataFolder() => throw new NotImplementedException();
        public string GetTerminalPath() => @"C:\Program Files\MetaTrader 5\terminal64.exe";
    }

    class DummyReportParser : IHtmReportParser
    {
        public SingleReport Parse(string content) => throw new NotImplementedException();
    }
}
