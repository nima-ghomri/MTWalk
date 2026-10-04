namespace MTWalk.IO;

interface IFormatFile<T> : IFile where T : IFormat
{
    T Get();
    void Write(T file);
}
