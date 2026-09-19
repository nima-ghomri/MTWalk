namespace MTWalk.Terminal;

public interface IExpertProvider
{
    FileSystemInfo[] GetFileInfos(string? relativePath = default);
}
