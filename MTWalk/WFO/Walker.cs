using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace MTWalk.WFO;

/// <summary>
/// 
/// </summary>
/// <param name="stepsProvider"></param>
/// <remarks>
/// https://www.mql5.com/en/articles/3279
/// </remarks>

class Walker(IWindowingStrategy windowingStrategy)
{
    public async Task Run(CancellationToken token)
    {
    }
}

[DebuggerDisplay("{Start} - {End}")]
sealed record TimeRange
{
    public TimeRange(DateTime Start, TimeSpan Duration)
    {
        this.Start = Start;
        this.Duration = Duration;
        this.End = Start + Duration;
    }

    public TimeRange(DateTime Start, DateTime End)
    {
        this.Start = Start;
        this.Duration = End - Start;
        this.End = End;
    }

    public TimeRange(TimeSpan Duration, DateTime End)
    {
        this.Start = End - Duration;
        this.Duration = Duration;
        this.End = End;
    }

    public DateTime Start { get; init; }
    public TimeSpan Duration { get; init; }
    public DateTime End { get; init; }
}

[DebuggerDisplay("{Optimization.Start} - {Forward.Start} - {Forward.End}")]
record Window(TimeRange Optimization, TimeRange Forward);

abstract record WindowingRequest(DateTime Start, DateTime End, TimeSpan ForwardSize);
record RollingWindowingRequest(DateTime Start, DateTime End, TimeSpan OptimizationSize, TimeSpan ForwardSize)
    : WindowingRequest(Start, End, ForwardSize);
record AnchoredWindowingRequest(DateTime Start, DateTime End, DateTime AnchorDate, TimeSpan ForwardSize)
    : WindowingRequest(Start, End, ForwardSize);
interface IWindowingStrategy
{
    Window[] GetWindows(WindowingRequest request);
}

enum WindowType
{
    Rolling,
    Anchored,
}

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
