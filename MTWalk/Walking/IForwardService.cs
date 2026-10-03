namespace MTWalk.Walking;

record ForwardResult(double Balance, HtmFile Report);

interface IForwardService
{
    Task<ForwardResult> RunAsync(
        TesterRequest testerRequest,
        TimeRange timeRange,
        IFileProvider directory,
        string expertParameters,
        double deposit,
        CancellationToken token = default);
}
