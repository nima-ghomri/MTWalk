using System.Diagnostics;

namespace MTWalk.WFO;

/// <summary>
/// Represents a range of time with a start and end.
/// </summary>
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
