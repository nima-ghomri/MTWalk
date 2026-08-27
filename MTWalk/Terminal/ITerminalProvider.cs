namespace MTWalk.Terminal;

/// <summary>
/// Interface for providing the path to the MetaTrader terminal executable.
/// </summary>
interface ITerminalProvider
{
    string GetTerminalPath();
    string GetDataFolder();
}
