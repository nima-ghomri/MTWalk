using Microsoft.Extensions.DependencyInjection;
using MTWalk.Reporting;

namespace MTWalk.Walking;

public interface IWalker
{
    Task Start(WindowingRequest windowingRequest,
               TesterRequest testerRequest,
               InputsRequest inputsRequest,
               IProgress<WalkReport>? progress = null,
               CancellationToken token = default);
}

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWalker(this IServiceCollection services)
    {
        services.AddSingleton<IHtmReportParser, HtmReportParser>();
        services.AddSingleton<IReportProvider, ReportProvider>();
        services.AddSingleton<ITerminalExecutor, TerminalExecutor>();
        services.AddSingleton<ITerminalProvider, TerminalProvider>();
        services.AddSingleton<IWindowingStrategy, RollingWindowingStrategy>();
        services.AddSingleton<IExpertProvider, ExpertProvider>();
        services.AddSingleton<ISymbolsProvider, SymbolsProvider>();
        services.AddSingleton<IWalker, Walker>();
        return services;
    }
}
