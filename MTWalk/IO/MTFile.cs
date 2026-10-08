using MTWalk.Serialization;

namespace MTWalk.IO;

class MTFile(ITerminalProvider terminalProvider) : IMTFile
{
    public string RelativeToDataFolderPath
        => field ??= Path.GetRelativePath(terminalProvider.GetDataFolder(), FullPath);
    public string RelativeToTesterPath
        => field ??= Path.GetRelativePath(terminalProvider.GetTesterFolder(), FullPath);
    public required string FullPath { get; internal set; }
    public string Read() => File.ReadAllText(FullPath);
    public void Write(string content) => File.WriteAllText(FullPath, content);
}


class FormatFile<T>(ITerminalProvider terminalProvider, ISerializer<T> serializer)
    : MTFile(terminalProvider), IFormatFile<T> where T : IFormat
{
    public T Get() => serializer.Deserialize(Read());
    public void Write(T file) => Write(serializer.Serialize(file));
}
