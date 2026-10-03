namespace MTWalk.Walking;

record OptimizationResult(IFileStream<SetFile> BestSetFile);

interface IOptimizationService
{
    Task<OptimizationResult> RunAsync(
        TesterRequest testerRequest,
        TimeRange timeRange,
        IFileProvider directory,
        string expertParameters,
        double deposit,
        CancellationToken token = default);
}
