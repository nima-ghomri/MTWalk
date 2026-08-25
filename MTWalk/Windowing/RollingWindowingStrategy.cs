namespace MTWalk.Windowing;

/// <summary>
/// Implements a rolling windowing strategy that generates windows based on a rolling forward size and optimization size.
/// </summary>
class RollingWindowingStrategy : IWindowingStrategy
{
    public Window[] GetWindows(WindowingRequest request)
    {
        if (request is not RollingWindowingRequest rolling)
            throw new ArgumentException("Invalid request type for RollingWindowStrategy", nameof(request));

        var result = new List<Window>();
        var start = rolling.Start;
        while (start < rolling.End)
        {
            var end = start + rolling.ForwardSize > rolling.End ? rolling.End : start + rolling.ForwardSize;
            TimeRange optimization = new(rolling.OptimizationSize, start);
            TimeRange forward = new(start, end);
            result.Add(new Window(optimization, forward));
            start += rolling.ForwardSize;
        }
        return result.ToArray();
    }
}
