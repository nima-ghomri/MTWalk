namespace MTWalk.Walking;

class ForwardService(ITerminalExecutor terminalExecutor) : IForwardService
{
    readonly EventsConfigurationParameters eventsParameters = new(Enable: false);
    readonly ConfigurationBuilder configurationBuilder = new();
    public async Task<ForwardResult> RunAsync(
        TesterRequest testerRequest,
        TimeRange timeRange,
        IFileProvider directory,
        string expertParameters,
        double deposit,
        CancellationToken token = default)
    {
        var stream = directory.GetForwardReportFile(timeRange);
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
            Report: stream.RelativePath);
        configurationBuilder.Clear();
        var configuration = configurationBuilder
            .AddParameters(parameters)
            .AddParameters(eventsParameters)
            .ToString();
        await terminalExecutor.Execute(configuration, token);
        var report = stream.Get();
        var balance = report.Deals.LastOrDefault()?.Balance ?? deposit;
        return new(balance, report);
    }
}
