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
        services.AddSingleton<IDirectoryProvider, DirectoryProvider>();
        services.AddTransient<IFileProvider, FileProvider>();
        services.AddSingleton<ISerializer<HtmFile>, HtmSerializer>();
        services.AddSingleton<ISerializer<XmlFile>, XmlSerializer>();
        services.AddSingleton<ISerializer<SetFile>, SetSerializer>();
        services.AddTransient<IFileStream, IO.FileStream>();
        services.AddTransient<IFileStream<XmlFile>, FileStream<XmlFile>>();
        services.AddTransient<IFileStream<HtmFile>, FileStream<HtmFile>>();
        services.AddTransient<IFileStream<SetFile>, FileStream<SetFile>>();
        return services;
    }
}
