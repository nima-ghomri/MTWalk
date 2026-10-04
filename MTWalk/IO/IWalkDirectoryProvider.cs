namespace MTWalk.IO;

interface IWalkDirectoryProvider
{
    WalkDirectory GetWalkDirectory(TesterRequest testerRequest);
}
