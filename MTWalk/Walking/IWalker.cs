using Microsoft.Extensions.DependencyInjection;

namespace MTWalk.Walking;

public interface IWalker
{
    Task Start(WindowingRequest windowingRequest, TesterRequest testerRequest, CancellationToken token = default);
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWalker(this IServiceCollection services)
        => services.AddSingleton<IWalker, Walker>();
}
