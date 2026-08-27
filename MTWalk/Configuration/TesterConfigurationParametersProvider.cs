using MTWalk.Reporting;

namespace MTWalk.Configuration;

class TesterConfigurationParametersProvider(IReportProvider reportProvider) 
    : ITesterConfigurationParametersProvider
{
    public IConfigurationParameters CreateForward(TesterRequest terminal, TimeRange timeRange)
        => new TesterConfigurationParameters(Expert: terminal.Expert,
                                             ExpertParameters: default,
                                             Symbol: terminal.Symbol,
                                             Period: terminal.TimeFrame,
                                             Login: default,
                                             Deposit: terminal.Deposit,
                                             Leverage: terminal.Leverage,
                                             Model: terminal.Model,
                                             ExecutionMode: terminal.ExecutionMode,
                                             Optimization: Optimization.Disabled,
                                             OptimizationCriterion: default,
                                             FromDate: timeRange.Start,
                                             ToDate: timeRange.End,
                                             ForwardMode: default,
                                             ForwardDate: default,
                                             Report: reportProvider.GetReportFileName(timeRange),
                                             ReplaceReport: true,
                                             ShutdownTerminal: true);

    public IConfigurationParameters CreateOptimization(TesterRequest terminal, TimeRange timeRange)
        => new TesterConfigurationParameters(Expert: terminal.Expert,
                                             ExpertParameters: default,
                                             Symbol: terminal.Symbol,
                                             Period: terminal.TimeFrame,
                                             Login: default,
                                             Deposit: terminal.Deposit,
                                             Leverage: terminal.Leverage,
                                             Model: terminal.Model,
                                             ExecutionMode: terminal.ExecutionMode,
                                             Optimization: terminal.Optimization,
                                             OptimizationCriterion: terminal.OptimizationCriterion,
                                             FromDate: timeRange.Start,
                                             ToDate: timeRange.End,
                                             ForwardMode: default,
                                             ForwardDate: default,
                                             Report: default,
                                             ReplaceReport: default,
                                             ShutdownTerminal: true);
}