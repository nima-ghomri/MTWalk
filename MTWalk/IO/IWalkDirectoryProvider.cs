using Microsoft.Extensions.DependencyInjection;

namespace MTWalk.IO;

interface IWalkDirectoryProvider
{
    WalkDirectory GetWalkDirectory(TesterRequest testerRequest);
}

interface IFileProvider
{
    string RelativePath { get; set; }
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
    string RelativeToDataFolderPath { get; }
    string RelativeToTesterPath { get; }
    //string BaseDirectoryPath { get; }
    void Write(string content);
}

interface IFormatFile<T> : IFile where T : IFormat
{
    T Get();
    void Write(T file);
}

class DirectoryProvider(ITerminalProvider terminalProvider, IServiceProvider serviceProvider) : IWalkDirectoryProvider
{
    public WalkDirectory GetWalkDirectory(TesterRequest testerRequest)
    {
        var fileProvider = serviceProvider.GetRequiredService<WalkDirectory>();
        var relativePath = GetReportFolderRelative(testerRequest);
        Directory.CreateDirectory(Path.Combine(terminalProvider.GetDataFolder(), relativePath));
        fileProvider.RelativePath = relativePath;
        return fileProvider;
    }

    const string BaseDirectoryRelative = @"MQL5\Profiles\Tester\MTWalk";
    static string GetReportFolderRelative(TesterRequest r)
    {
        var expertFolder = Path.GetFileNameWithoutExtension(r.Expert);
        var walkFolder = $"{r.Symbol}_{r.TimeFrame}_{r.Model}_{r.Start:yyyyMMdd}_{r.End:yyyyMMdd}";
        return Path.Combine(BaseDirectoryRelative, expertFolder, walkFolder);
    }
}

class WalkDirectory(IServiceProvider serviceProvider) : IFileProvider
{
    public required string RelativePath { get; set; }

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
        //f.BaseDirectoryPath = terminalProvider.GetDataFolder();
        //f.RelativePath = Path.Combine(RelativePath, filename);
        f.RelativeToDataFolderPath = Path.Combine(RelativePath, filename);
        return file;
    }

    static string GetRelativePath(TimeRange timeRange, string label, string extension)
         => $"{timeRange.Start:yyyyMMdd}_{timeRange.End:yyyyMMdd}_{label}.{extension}";
}

class File(ITerminalProvider terminalProvider) : IFile
{
    public required string RelativeToDataFolderPath { get; internal set; }
    public string RelativeToTesterPath
        => field ??= Path.GetRelativePath(Path.Combine(terminalProvider.GetDataFolder(), @"MQL5\Profiles\Tester"), FullPath);
    //public required string BaseDirectoryPath { get; internal set; }
    public string FullPath => field ??= Path.Combine(terminalProvider.GetDataFolder(), RelativeToDataFolderPath);
    public string Read() => System.IO.File.ReadAllText(FullPath);
    public void Write(string content) => System.IO.File.WriteAllText(FullPath, content);
}

class FormatFile<T>(ITerminalProvider terminalProvider, ISerializer<T> serializer)
    : File(terminalProvider), IFormatFile<T> where T : IFormat
{
    public T Get() => serializer.Deserialize(Read());
    public void Write(T file) => Write(serializer.Serialize(file));
}
