using Microsoft.Extensions.DependencyInjection;

namespace MTWalk.IO;

interface IWalkDirectoryProvider
{
    WalkDirectory GetWalkDirectory(TesterRequest testerRequest);
}

interface IFileProvider
{
    IFile GetWalkParametersFile();
    IFormatFile<XmlFormat> GetOptimizationReportFile(TimeRange timeRange);
    IFormatFile<SetFormat> GetParametersFile(TimeRange timeRange);
    IFormatFile<HtmFomat> GetForwardReportFile(TimeRange timeRange);
    IFile GetForwardConfigurationFile(TimeRange timeRange);
    IFile GetOptimizationConfigurationFile(TimeRange timeRange);
}

interface IFile
{
    string FullPath { get; }
    string RelativeToTesterPath { get; }
    string RelativeToDataFolderPath { get; }
    void Write(string content);
}

interface IFormatFile<T> : IFile where T : IFormat
{
    T Get();
    void Write(T file);
}

class DirectoryProvider(ITerminalProvider terminalProvider, IServiceProvider serviceProvider) : IWalkDirectoryProvider
{
    const string BaseFolderName = "MTWalk";

    public WalkDirectory GetWalkDirectory(TesterRequest testerRequest)
    {
        var fileProvider = serviceProvider.GetRequiredService<WalkDirectory>();
        fileProvider.FullPath = GetWalkDirectoryPath(testerRequest);
        return fileProvider;
    }

    string GetWalkDirectoryPath(TesterRequest r)
    {
        var expertFolder = Path.GetFileNameWithoutExtension(r.Expert);
        var walkFolder = $"{r.Symbol}_{r.TimeFrame}_{r.Model}_{r.Start:yyyyMMdd}_{r.End:yyyyMMdd}";
        return Path.Combine(terminalProvider.GetTesterFolder(), BaseFolderName, expertFolder, walkFolder);
    }
}

class WalkDirectory(IServiceProvider serviceProvider) : IFileProvider
{
    public required string FullPath { get; internal set; }
    public IFile GetWalkParametersFile()
        => GetFile<IFile>("0_parameters_walk.set");

    public IFile GetOptimizationConfigurationFile(TimeRange timeRange)
        => GetFile<IFile>(GetRelativePath(timeRange, "1_optimization_configuration", "ini"));

    public IFormatFile<XmlFormat> GetOptimizationReportFile(TimeRange timeRange)
        => GetFile<IFormatFile<XmlFormat>>(GetRelativePath(timeRange, "2_optimization_report", "xml"));

    public IFormatFile<SetFormat> GetParametersFile(TimeRange timeRange)
        => GetFile<IFormatFile<SetFormat>>(GetRelativePath(timeRange, "3_best_parameters", "set"));

    public IFile GetForwardConfigurationFile(TimeRange timeRange)
        => GetFile<IFile>(GetRelativePath(timeRange, "4_forward_configuration", "ini"));

    public IFormatFile<HtmFomat> GetForwardReportFile(TimeRange timeRange)
        => GetFile<IFormatFile<HtmFomat>>(GetRelativePath(timeRange, "5_forward_report", "htm"));

    T GetFile<T>(string filename) where T : IFile
    {
        var file = serviceProvider.GetRequiredService<T>();
        if (file is not File f)
            throw new InvalidOperationException($"Service {typeof(T).Name} is not a File");
        f.FullPath = Path.Combine(FullPath, filename);
        return file;
    }

    static string GetRelativePath(TimeRange timeRange, string label, string extension)
         => $"{label}_{timeRange.Start:yyyyMMdd}_{timeRange.End:yyyyMMdd}.{extension}";
}

class File(ITerminalProvider terminalProvider) : IFile
{
    public string RelativeToDataFolderPath
        => field ??= Path.GetRelativePath(terminalProvider.GetDataFolder(), FullPath);
    public string RelativeToTesterPath
        => field ??= Path.GetRelativePath(terminalProvider.GetTesterFolder(), FullPath);
    public required string FullPath { get; internal set; }
    public string Read() => System.IO.File.ReadAllText(FullPath);
    public void Write(string content) => System.IO.File.WriteAllText(FullPath, content);
}

class FormatFile<T>(ITerminalProvider terminalProvider, ISerializer<T> serializer)
    : File(terminalProvider), IFormatFile<T> where T : IFormat
{
    public T Get() => serializer.Deserialize(Read());
    public void Write(T file) => Write(serializer.Serialize(file));
}
