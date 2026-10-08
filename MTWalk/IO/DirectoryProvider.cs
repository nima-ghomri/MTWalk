using Microsoft.Extensions.DependencyInjection;

namespace MTWalk.IO;

class DirectoryProvider(ITerminalProvider terminalProvider, IServiceProvider serviceProvider) : IWalkDirectoryProvider
{
    const string BaseFolderName = "MTWalk";

    public WalkDirectory GetWalkDirectory(TesterRequest testerRequest)
    {
        var fileProvider = serviceProvider.GetRequiredService<WalkDirectory>();
        fileProvider.FullPath = GetWalkDirectoryPath(testerRequest);
        if (Directory.CreateDirectory(fileProvider.FullPath) is null)
            throw new InvalidOperationException("Failed to create walk directory.");
        return fileProvider;
    }

    string GetWalkDirectoryPath(TesterRequest r)
    {
        var expertFolder = Path.GetFileNameWithoutExtension(r.Expert);
        var walkFolderPath = $"{r.Symbol}_{r.TimeFrame}_{r.Model}_{r.Start:yyyyMMdd}_{r.End:yyyyMMdd}";
        return Path.Combine(terminalProvider.GetTesterFolder(), BaseFolderName, expertFolder, walkFolderPath);
    }
}
