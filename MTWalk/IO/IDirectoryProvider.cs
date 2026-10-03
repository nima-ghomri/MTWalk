using Microsoft.Extensions.DependencyInjection;

namespace MTWalk.IO;

interface IDirectoryProvider
{
    IFileProvider GetWalkDirectory(TesterRequest testerRequest);
}

interface IFileProvider
{
    string RelativePath { get; set; }

    IFileStream GetWalkParametersFile();
    IFileStream<XmlFile> GetOptimizationReportFile(TimeRange timeRange);
    IFileStream<SetFile> GetParametersFile(TimeRange timeRange);
    IFileStream<HtmFile> GetForwardReportFile(TimeRange timeRange);
}

interface IFileStream
{
    string RelativePath { get; set; }
    string BaseDirectoryPath { get; set; }
    void Write(string content);
}

interface IFileStream<T> : IFileStream where T : IFile
{
    T Get();
    void Write(T file);
}

class DirectoryProvider(ITerminalProvider terminalProvider, IServiceProvider serviceProvider) : IDirectoryProvider
{
    public IFileProvider GetWalkDirectory(TesterRequest testerRequest)
    {
        var fileProvider = serviceProvider.GetRequiredService<IFileProvider>();
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

class FileProvider(ITerminalProvider terminalProvider, IServiceProvider serviceProvider) : IFileProvider
{
    public required string RelativePath { get; set; }

    public IFileStream GetWalkParametersFile()
        => GetFileStream<IFileStream>("parameters_walk.set");

    public IFileStream<XmlFile> GetOptimizationReportFile(TimeRange timeRange)
        => GetFileStream<IFileStream<XmlFile>>(GetRelativePath(timeRange, "optimization", "xml"));

    public IFileStream<SetFile> GetParametersFile(TimeRange timeRange)
        => GetFileStream<IFileStream<SetFile>>(GetRelativePath(timeRange, "parameters", "set"));

    public IFileStream<HtmFile> GetForwardReportFile(TimeRange timeRange)
        => GetFileStream<IFileStream<HtmFile>>(GetRelativePath(timeRange, "forward", "htm"));

    T GetFileStream<T>(string filename) where T : IFileStream
    {
        var fileStream = serviceProvider.GetRequiredService<T>();
        fileStream.BaseDirectoryPath = terminalProvider.GetDataFolder();
        fileStream.RelativePath = Path.Combine(RelativePath, filename);
        return fileStream;
    }

    static string GetRelativePath(TimeRange timeRange, string label, string extension)
         => $"{label}_{timeRange.Start:yyyyMMdd}_{timeRange.End:yyyyMMdd}.{extension}";
}

class FileStream : IFileStream
{
    public required string RelativePath { get; set; }
    public required string BaseDirectoryPath { get; set; }
    string FullPath => Path.Combine(BaseDirectoryPath, RelativePath);

    public string Read() => File.ReadAllText(FullPath);
    public void Write(string content) => File.WriteAllText(FullPath, content);
}

class FileStream<T>(ISerializer<T> serializer) : FileStream, IFileStream<T> where T : IFile
{
    public T Get() => serializer.Deserialize(Read());

    public void Write(T file) => Write(serializer.Serialize(file));
}