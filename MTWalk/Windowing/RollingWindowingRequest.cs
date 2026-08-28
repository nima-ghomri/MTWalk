namespace MTWalk.Windowing;

/// <summary>
/// Defines a request for rolling windowing.
/// </summary>
/// <param name="Start">The start time of the whole test.</param>
/// <param name="End">The end time of the whole test.</param>
/// <param name="OptimizationSize">The size of the optimization window.</param>
/// <param name="ForwardSize">The size of the forward window.</param>
public record RollingWindowingRequest(TimeSpan OptimizationSize, TimeSpan ForwardSize)
    : WindowingRequest(ForwardSize);
