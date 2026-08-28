namespace MTWalk.Windowing;

/// <summary>
/// Implements a rolling windowing strategy that generates windows based on a rolling forward size and optimization size.
/// </summary>
class RollingWindowingStrategy : IWindowingStrategy
{
    public Window[] GetWindows(DateTime start, DateTime end, WindowingRequest request)
    {
        if (request is not RollingWindowingRequest rolling)
            throw new ArgumentException("Invalid request type for RollingWindowStrategy", nameof(request));

        var result = new List<Window>();
        var current = start;
        while (current < end)
        {
            var currentEnd = current + rolling.ForwardSize > end ? end : current + rolling.ForwardSize;
            TimeRange optimization = new(rolling.OptimizationSize, current);
            TimeRange forward = new(current, currentEnd);
            result.Add(new Window(optimization, forward));
            current += rolling.ForwardSize;
        }
        return result.ToArray();
    }
}
