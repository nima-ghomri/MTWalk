using MTWalk.Configuration;
using MTWalk.IO;
using MTWalk.MetaQuotes;
using MTWalk.Reporting;
using MTWalk.Services;
using MTWalk.Terminal;
using MTWalk.Walking;
using MTWalk.Windowing;

namespace MTWalk.Tests;

class WalkingTests
{
    [Test]
    public async Task Walker_Start_ExecutesWithoutExceptions()
    {
        var testerRequest = new TesterRequest(
                Expert: @"Examples\Moving Average\Moving Average",
                Symbol: "EURUSD",
                TimeFrame: TimeFrame.H1,
                Start: new(2025, 2, 3),
                End: new(2025, 4, 3),
                ForwardMode: default,
                ForwardDate: default,
                ExecutionMode: ExecutionMode.ZeroDelay,
                Model: Model.OneMinuteOHLC,
                Deposit: 1000.0,
                Currency: "USD",
                Leverage: 100,
                Optimization: Optimization.FastGeneticAlgorithm,
                OptimizationCriterion: OptimizationCriterion.ComplexCriterionMax
            );
        var windowingRequest = new RollingWindowingRequest(TimeSpan.FromDays(7), TimeSpan.FromDays(2));
        // Arrange
        var inputsRequest = new InputsRequest(default!);
        ReportContextFactory reportFactory = new(new TerminalProvider());
        TerminalExecutor terminalExecuter = new(new DummyTerminalProvider());
        RollingWindowingStrategy rollingWindowingStrategy = new();
        ConfigurationBuilder configurationBuilder = new();
        IOptimizationService optimizationService = new OptimizationService();
        var walker = new Walker(rollingWindowingStrategy, terminalExecuter, reportFactory, optimizationService);
        // Act & Assert
        await walker.Start(windowingRequest, testerRequest, inputsRequest);
        //Assert.DoesNotThrowAsync(async () => await walker.Start(windowingRequest, testerRequest, inputsRequest));
    }

    class DummyTerminalProvider : ITerminalProvider
    {
        public string GetDataFolder() => throw new NotImplementedException();
        public string GetTerminalPath() => @"C:\Program Files\MetaTrader 5\terminal64.exe";
    }
}
