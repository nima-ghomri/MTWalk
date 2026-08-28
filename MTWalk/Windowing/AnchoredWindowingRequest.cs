namespace MTWalk.Windowing;

/// <summary>
/// Defines a request for generating windows based on an anchor date.
/// </summary>
/// <param name="Start">The start time of the whole test.</param>
/// <param name="End">The end time of the whole test.</param>
/// <param name="AnchorDate">The anchor date for window generation.</param>
/// <param name="ForwardSize">The size of the forward window.</param>
record AnchoredWindowingRequest(DateTime AnchorDate, TimeSpan ForwardSize)
    : WindowingRequest(ForwardSize);
