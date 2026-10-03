namespace MTWalk.Walking;

public interface IWalker
{
    Task Start(WalkRequest walkRequest, IProgress<WalkReport>? progress = default, CancellationToken token = default);
}

public record WalkRequest(WindowingRequest Windowing, TesterRequest Tester, InputsRequest Inputs);