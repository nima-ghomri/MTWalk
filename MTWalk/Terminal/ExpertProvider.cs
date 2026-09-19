namespace MTWalk.Terminal;

class ExpertProvider(ITerminalProvider terminalProvider) : IExpertProvider
{
    const string Ex5FilePattern = "*.ex5";
    readonly Dictionary<string, FileSystemInfo[]> cache = [];
    readonly DirectoryInfo expertsFolder = new(Path.Combine(terminalProvider.GetDataFolder(), @"MQL5\Experts"));

    public DirectoryInfo GetExpertsFolder() => expertsFolder;

    public FileSystemInfo[] GetFileInfos(string? relativePath = null)
    {
        var path = string.IsNullOrWhiteSpace(relativePath) ? expertsFolder.FullName : Path.Combine(expertsFolder.FullName, relativePath);
        if (cache.TryGetValue(path, out var infos))
            return infos;
        var options = new EnumerationOptions() { RecurseSubdirectories = true, IgnoreInaccessible = true };
        var directory = new DirectoryInfo(path);
        infos = directory.EnumerateDirectories()
            .Where(x => x.EnumerateFiles(Ex5FilePattern, options).Any())
            .AsEnumerable<FileSystemInfo>()
            .Concat(directory.EnumerateFiles(Ex5FilePattern)).ToArray();
        cache.Add(path, infos);
        return infos;
    }
}
