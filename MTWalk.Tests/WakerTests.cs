using MTWalk.WFO;

namespace MTWalk.Tests;

class WakerTests
{
    [Test]
    public void RollingWindowProvider_GeneratesValidWindows()
    {
        var start = new DateTime(2025, 1, 1);
        var end = new DateTime(2025, 1, 15);

        var optimizationWindow = TimeSpan.FromDays(7);
        var stepSize = TimeSpan.FromDays(2);
        IStepsProvider provider = new RollingWindowProvider(optimizationWindow, stepSize);
        var steps = provider.GetSteps(start, end).ToList();
        Assert.That(steps, Has.Count.EqualTo(2));
        using (Assert.EnterMultipleScope())
        {
            Assert.That(steps.Select(x => x.Optimization.Duration),
                    Is.All.EqualTo(optimizationWindow));
            Assert.That(steps.Zip(steps.Skip(1), (prev, current) => current.Forward.Start == prev.Forward.End),
                Is.All.True);
        }
    }

    [Test]
    public async Task Walker_Optimization_CombinesReports()
    {
        var start = new DateTime(2025, 1, 1);
        var end = new DateTime(2025, 2, 1);

    }
}
