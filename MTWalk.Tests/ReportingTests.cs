using MTWalk.IO;
using MTWalk.MetaQuotes;
using MTWalk.Reporting;
using MTWalk.Terminal;

namespace MTWalk.Tests;

class ReportingTests
{
    [Test]
    public void ReportFactory_TimeRange_CreateFileName()
    {
        var testerRequest = new TesterRequest(
                Expert: @"Examples\MACD\MACD Sample.ex5",
                Symbol: "EURUSD",
                TimeFrame: TimeFrame.H1,
                Start: new(2025, 01, 01),
                End: new(2025, 03, 01),
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
        IReportContextFactory reportFactory = new ReportContextFactory(new TerminalProvider());
        var context = reportFactory.CreateContext(testerRequest);
        var report = context.CreateSingleReportReader(new(new DateTime(2025, 1, 1), new DateTime(2025, 1, 31)));
        Assert.That(report.RelativePath, Contains.Value("Forward_20250101_20250131.xml"));
    }
}
