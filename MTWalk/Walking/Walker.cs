using System.Runtime.CompilerServices;

namespace MTWalk.Walking;

/// <summary>
/// 
/// </summary>
/// <param name="windowingStrategy"></param>
/// <remarks>
/// https://www.mql5.com/en/articles/3279
/// </remarks>

class Walker(IWindowingStrategy windowingStrategy)
{
    public async Task Run(CancellationToken token)
    {
    }
}
