using Microsoft.Extensions.DependencyInjection;
using MTWalk.Serialization;

namespace MTWalk.IO;

class WalkDirectory(IServiceProvider serviceProvider) : IFileProvider
{
    public required string FullPath { get; internal set; }
    public IMTFile GetWalkParametersFile()
        => GetFile<IMTFile>("0_parameters_walk.set");

    public IMTFile GetOptimizationConfigurationFile(TimeRange timeRange)
        => GetFile<IMTFile>(GetRelativePath(timeRange, "1_optimization_configuration", "ini"));

    public IFormatFile<XmlFormat> GetOptimizationReportFile(TimeRange timeRange)
        => GetFile<IFormatFile<XmlFormat>>(GetRelativePath(timeRange, "2_optimization_report", "xml"));

    public IFormatFile<SetFormat> GetParametersFile(TimeRange timeRange)
        => GetFile<IFormatFile<SetFormat>>(GetRelativePath(timeRange, "3_best_parameters", "set"));

    public IMTFile GetForwardConfigurationFile(TimeRange timeRange)
        => GetFile<IMTFile>(GetRelativePath(timeRange, "4_forward_configuration", "ini"));

    public IFormatFile<HtmFomat> GetForwardReportFile(TimeRange timeRange)
        => GetFile<IFormatFile<HtmFomat>>(GetRelativePath(timeRange, "5_forward_report", "htm"));

    T GetFile<T>(string filename) where T : IMTFile
    {
        var file = serviceProvider.GetRequiredService<T>();
        if (file is not MTFile f)
            throw new InvalidOperationException($"Service {typeof(T).Name} is not a File");
        f.FullPath = Path.Combine(FullPath, filename);
        return file;
    }

    static string GetRelativePath(TimeRange timeRange, string label, string extension)
         => $"{label}_{timeRange.Start:yyyyMMdd}_{timeRange.End:yyyyMMdd}.{extension}";
}