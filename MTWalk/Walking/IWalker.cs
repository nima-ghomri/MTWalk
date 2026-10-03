namespace MTWalk.Walking;

public interface IWalker
{
    Task Start(WindowingRequest windowingRequest,
               TesterRequest testerRequest,
               InputsRequest inputsRequest,
               IProgress<WalkReport>? progress = null,
               CancellationToken token = default);
}
