using MTWalk.Windowing;

namespace MTWalk.Execution;

interface ITestExecuter
{
    Task Execute(Window step, CancellationToken token);
}
