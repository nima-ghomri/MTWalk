namespace MTWalk.IO;

interface IMTFile
{
    string FullPath { get; }
    string RelativeToTesterPath { get; }
    string RelativeToDataFolderPath { get; }
    void Write(string content);
}
