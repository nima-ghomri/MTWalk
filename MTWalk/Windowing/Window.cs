using System.Diagnostics;

namespace MTWalk.Windowing;

/// <summary>
/// Defines a window with optimization and forward time ranges.
/// </summary>
/// <param name="Optimization">The optimization time range.</param>
/// <param name="Forward">The forward time range.</param>

[DebuggerDisplay("{Optimization.Start} - {Forward.Start} - {Forward.End}")]
record Window(TimeRange Optimization, TimeRange Forward);
