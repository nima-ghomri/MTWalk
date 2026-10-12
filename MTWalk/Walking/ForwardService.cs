using MTWalk.Exceptions;

namespace MTWalk.Walking;

class ForwardService(ITerminalExecutor terminalExecutor) : IForwardService
{
    readonly static EventsConfigurationParameters eventsParameters = new(Enable: false);
    public async Task<ForwardResult> RunAsync(
        TesterRequest testerRequest,
        TimeRange timeRange,
        IFileProvider directory,
        string expertParameters,
        double deposit,
        CancellationToken token = default)
    {
        var reportFile = directory.GetForwardReportFile(timeRange);
        var parameters = new ForwardConfigurationParameters(
            Expert: testerRequest.Expert,
            ExpertParameters: expertParameters,
            Symbol: testerRequest.Symbol,
            Period: testerRequest.TimeFrame,
            Login: default,
            Deposit: deposit,
            Leverage: testerRequest.Leverage,
            Model: testerRequest.Model,
            ExecutionMode: testerRequest.ExecutionMode,
            FromDate: timeRange.Start,
            ToDate: timeRange.End,
            Report: reportFile.RelativeToDataFolderPath);
        var configuration = new ConfigurationBuilder()
            .AddParameters(parameters)
            .AddParameters(eventsParameters)
            .ToString();
        var configurationFile = directory.GetForwardConfigurationFile(timeRange);
        configurationFile.Write(configuration);
        await terminalExecutor.ExecuteByPath(configurationFile.FullPath, token);
        try
        {
            var report = reportFile.Get();
            var balance = report.Deals.LastOrDefault()?.Balance ?? deposit;
            return new(balance, report);
        }
        catch (FileNotFoundException ex)
        {
            throw new ExecutionResultNotFoundException(ex);
        }
    }
}
