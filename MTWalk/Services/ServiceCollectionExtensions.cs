using Microsoft.Extensions.DependencyInjection;
using MTWalk.Serialization;
using MTWalk.Walking;

namespace MTWalk.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddWalker(this IServiceCollection services)
    {
        services.AddSingleton<ITerminalExecutor, TerminalExecutor>();
        services.AddSingleton<ITerminalProvider, TerminalProvider>();
        services.AddSingleton<IWindowingStrategy, RollingWindowingStrategy>();
        services.AddSingleton<IExpertProvider, ExpertProvider>();
        services.AddSingleton<ISymbolsProvider, SymbolsProvider>();
        services.AddSingleton<IOptimizationService, OptimizationService>();
        services.AddSingleton<IWalker, Walker>();
        services.AddSingleton<IWalkDirectoryProvider, DirectoryProvider>();
        services.AddTransient<WalkDirectory>();
        services.AddSingleton<ISerializer<HtmFomat>, HtmSerializer>();
        services.AddSingleton<ISerializer<XmlFormat>, XmlSerializer>();
        services.AddSingleton<ISerializer<SetFormat>, SetSerializer>();
        services.AddTransient<IFile, IO.File>();
        services.AddTransient<IFormatFile<XmlFormat>, FormatFile<XmlFormat>>();
        services.AddTransient<IFormatFile<HtmFomat>, FormatFile<HtmFomat>>();
        services.AddTransient<IFormatFile<SetFormat>, FormatFile<SetFormat>>();
        services.AddSingleton<IForwardService, ForwardService>();
        services.AddSingleton<IOptimizationService, OptimizationService>();
        return services;
    }
}
