namespace MTWalk.IO;

interface IFile
{
    string FullPath { get; }
    string RelativeToTesterPath { get; }
    string RelativeToDataFolderPath { get; }
    void Write(string content);
}
