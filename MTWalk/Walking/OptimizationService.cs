namespace MTWalk.Walking;

class OptimizationService(ITerminalExecutor terminalExecutor) : IOptimizationService
{
    readonly ConfigurationBuilder configurationBuilder = new();
    readonly EventsConfigurationParameters eventsParameters = new(Enable: false);

    public async Task<OptimizationResult> RunAsync(
        TesterRequest testerRequest,
        TimeRange timeRange,
        IFileProvider directory,
        string expertParameters,
        double deposit,
        CancellationToken token = default)
    {
        var stream = directory.GetOptimizationReportFile(timeRange);
        var parameters = new OptimizationConfigurationParameters(
            Expert: testerRequest.Expert,
            ExpertParameters: expertParameters,
            Symbol: testerRequest.Symbol,
            Period: testerRequest.TimeFrame,
            Login: default,
            Deposit: deposit,
            Leverage: testerRequest.Leverage,
            Model: testerRequest.Model,
            ExecutionMode: testerRequest.ExecutionMode,
            Optimization: testerRequest.Optimization,
            OptimizationCriterion: testerRequest.OptimizationCriterion,
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
        var bestPass = report.Passes.MaxBy(x => x[testerRequest.OptimizationCriterion]);
        var setStream = directory.GetParametersFile(timeRange);
        var setFile = SetFile.Create(bestPass?.Inputs);
        setStream.Write(setFile);
        return new(setStream);
    }
}







