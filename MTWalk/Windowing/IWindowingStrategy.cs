using Microsoft.Extensions.DependencyInjection;

namespace MTWalk.Windowing;

/// <summary>
/// Defines a strategy for generating windows.
/// </summary>
interface IWindowingStrategy
{
    Window[] GetWindows(DateTime start, DateTime end, WindowingRequest request);
}