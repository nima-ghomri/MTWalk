namespace MTWalk.Windowing;

/// <summary>
/// Defines a request for windowing.
/// </summary>
/// <param name="Start">The start time of the whole test.</param>
/// <param name="End">The end time of the whole test.</param>
/// <param name="ForwardSize">The size of the forward window.</param>
abstract record WindowingRequest(DateTime Start, DateTime End, TimeSpan ForwardSize);
