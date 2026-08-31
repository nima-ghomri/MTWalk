namespace MTWalk.Terminal;

class TerminalProvider : ITerminalProvider
{
    const string MT5RelativePath = @"MetaQuotes\Terminal";
    readonly string dataFolder;
    FileInfo terminal = new(@"C:\Program Files\MetaTrader 5\terminal64.exe");

    public TerminalProvider()
    {
        dataFolder = FindDataFolder();
    }

    string FindDataFolder()
    {
        // 1. Get the path to %APPDATA%\MetaQuotes\Terminal
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var mt5SpecialFolder = new DirectoryInfo(Path.Combine(appData, MT5RelativePath));

        if (mt5SpecialFolder.Exists is false)
            throw new DirectoryNotFoundException("Terminal base path not found.");

        // 2. Iterate through all the 32-character hash directories
        foreach (var directory in mt5SpecialFolder.GetDirectories())
        {
            var originFile = directory.GetFiles("origin.txt").SingleOrDefault();

            if (originFile is not null)
            {
                // 3. Read the text inside origin.txt (it contains the installation path)
                // MT5 writes it in UTF-16, but File.ReadAllText handles the BOM automatically
                var originContent = File.ReadAllText(originFile.FullName).Trim();

                // 4. Compare it against your known install path
                // StringComparison.OrdinalIgnoreCase is crucial because Windows paths are not case-sensitive
                if (originContent.Equals(terminal.Directory?.FullName, StringComparison.OrdinalIgnoreCase))
                    return directory.FullName;
            }
        }

        throw new DirectoryNotFoundException("Matching terminal directory not found.");
    }

    /// <summary>
    /// Finds the dynamic AppData folder for a specific MT5 installation.
    /// </summary>
    /// <param name="installPath">The path where terminal64.exe is located (e.g., @"C:\Program Files\MetaTrader 5")</param>
    /// <returns>The full path to the terminal's data folder, or null if not found.</returns>
    public string GetDataFolder() => dataFolder;

    public string GetTerminalPath()
    {
        if (terminal.Exists is false)
            throw new FileNotFoundException($"Terminal executable not found in '{terminal.FullName}'.");
        return terminal.FullName;
    }
}