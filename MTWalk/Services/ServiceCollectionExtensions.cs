using Microsoft.Extensions.DependencyInjection;
using MTWalk.Walking;

namespace MTWalk.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWalker(this IServiceCollection services)
    {
        services.AddSingleton<IReportContextFactory, ReportContextFactory>();
        services.AddSingleton<ITerminalExecutor, TerminalExecutor>();
        services.AddSingleton<ITerminalProvider, TerminalProvider>();
        services.AddSingleton<IWindowingStrategy, RollingWindowingStrategy>();
        services.AddSingleton<IExpertProvider, ExpertProvider>();
        services.AddSingleton<ISymbolsProvider, SymbolsProvider>();
        services.AddSingleton<IOptimizationService, OptimizationService>();
        services.AddSingleton<IWalker, Walker>();
        return services;
    }
}
