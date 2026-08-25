namespace MTWalk.WFO;

/// <summary>
/// 
/// </summary>
/// <param name="stepsProvider"></param>
/// <remarks>
/// https://www.mql5.com/en/articles/3279
/// </remarks>
public class Walker(IStepsProvider stepsProvider, ITestExecuter testExecuter)
{
    public async Task Run(CancellationToken token)
    {
        foreach (var step in stepsProvider.GetSteps(DateTime.UtcNow, DateTime.UtcNow.AddDays(30)))
        {
            if (token.IsCancellationRequested)
                break;
            await testExecuter.Execute(step, token);
        }
    }
}

public interface ITestExecuter
{
    Task Execute(Step step, CancellationToken token);
}

public sealed record TimeRange(DateTime Start, TimeSpan Duration)
{
    public DateTime End => Start + Duration;
}

public record Step(TimeRange Optimization, TimeRange Forward);

public interface IStepsProvider
{
    Step[] GetSteps(DateTime start, DateTime end);
}
