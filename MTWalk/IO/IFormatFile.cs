namespace MTWalk.IO;

interface IFormatFile<T> : IMTFile where T : IFormat
{
    T Get();
    void Write(T file);
}
