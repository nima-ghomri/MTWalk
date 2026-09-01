using Microsoft.Extensions.DependencyInjection;
using MTWalk.Reporting;

namespace MTWalk.Walking;

public interface IWalker
{
    Task Start(WindowingRequest windowingRequest,
               TesterRequest testerRequest,
               IProgress<WalkProgressReport>? progress = null,
               CancellationToken token = default);
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWalker(this IServiceCollection services)
    {
        services.AddSingleton<IHtmReportParser, HtmReportParser>();
        services.AddSingleton<IReportProvider, ReportProvider>();
        services.AddSingleton<IConfigurationBuilder, ConfigurationBuilder>();
        services.AddSingleton<ITerminalExecutor, TerminalExecutor>();
        services.AddSingleton<ITerminalProvider, TerminalProvider>();
        services.AddSingleton<IWindowingStrategy, RollingWindowingStrategy>();
        return services.AddSingleton<IWalker, Walker>();
    }
}
