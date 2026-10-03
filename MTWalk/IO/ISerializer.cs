namespace MTWalk.IO;

interface IFile;

interface ISerializer<T> where T : IFile
{
    string Serialize(T report);
    T Deserialize(string content);
}
