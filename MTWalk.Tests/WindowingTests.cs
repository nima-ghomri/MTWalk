using MTWalk.Windowing;

namespace MTWalk.Tests;

class WindowingTests
{
    [Test]
    [TestCase("2025-02-01", "2025-02-05", 7, 2, 2)]
    [TestCase("2025-02-01", "2025-02-06", 7, 2, 3)]
    public void RollingWindowProvider_GeneratesValidWindows
        (string startStr, string endStr, int optimizationDays, int forwardDays, int expectedCount)
    {
        var start = DateTime.Parse(startStr);
        var end = DateTime.Parse(endStr);
        var optimizationSize = TimeSpan.FromDays(optimizationDays);
        var forwardSize = TimeSpan.FromDays(forwardDays);
        var request = new RollingWindowingRequest(start, end, optimizationSize, forwardSize);

        IWindowingStrategy provider = new RollingWindowingStrategy();
        var windows = provider.GetWindows(request).ToList();
        Assert.That(windows, Has.Count.EqualTo(expectedCount));
        using (Assert.EnterMultipleScope())
        {
            Assert.That(windows.Select(x => x.Optimization.Duration),
                    Is.All.EqualTo(optimizationSize));
            Assert.That(windows.Zip(windows.Skip(1), (prev, current) => current.Forward.Start == prev.Forward.End),
                Is.All.True);
            Assert.That(windows.Last().Forward.End, Is.EqualTo(end));
        }
    }
}
