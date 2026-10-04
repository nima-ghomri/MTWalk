using Microsoft.Extensions.DependencyInjection;

namespace MTWalk.IO;

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
