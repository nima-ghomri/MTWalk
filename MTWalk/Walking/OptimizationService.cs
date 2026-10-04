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
        var reportFile = directory.GetOptimizationReportFile(timeRange);
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
            Report: reportFile.RelativeToDataFolderPath);
        configurationBuilder.Clear();
        var configuration = configurationBuilder
            .AddParameters(parameters)
            .AddParameters(eventsParameters)
            .ToString();
        var configurationFile = directory.GetOptimizationConfigurationFile(timeRange);
        configurationFile.Write(configuration);
        await terminalExecutor.ExecuteByPath(configurationFile.FullPath, token);
        var report = reportFile.Get();
        var bestPass = report.Passes.MaxBy(x => x[testerRequest.OptimizationCriterion]);
        var setFile = directory.GetParametersFile(timeRange);
        var setFormat = SetFormat.Create(bestPass?.Inputs);
        setFile.Write(setFormat);
        return new(setFile);
    }
}







