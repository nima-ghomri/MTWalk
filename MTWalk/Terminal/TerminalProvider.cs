namespace MTWalk.Terminal;

class TerminalProvider : ITerminalProvider
{
    string terminalPath = @"C:\Program Files\MetaTrader 5\terminal64.exe";

    /// <summary>
    /// Finds the dynamic AppData folder for a specific MT5 installation.
    /// </summary>
    /// <param name="installPath">The path where terminal64.exe is located (e.g., @"C:\Program Files\MetaTrader 5")</param>
    /// <returns>The full path to the terminal's data folder, or null if not found.</returns>
    public string GetDataFolder()
    {
        string installPath = GetTerminalPath();
        // 1. Get the path to %APPDATA%\MetaQuotes\Terminal
        var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        var terminalBasePath = Path.Combine(appData, "MetaQuotes", "Terminal");

        if (!Directory.Exists(terminalBasePath))
            throw new DirectoryNotFoundException("Terminal base path not found.");

        // 2. Iterate through all the 32-character hash directories
        foreach (var directory in Directory.GetDirectories(terminalBasePath))
        {
            var originFilePath = Path.Combine(directory, "origin.txt");

            if (File.Exists(originFilePath))
            {
                // 3. Read the text inside origin.txt (it contains the installation path)
                // MT5 writes it in UTF-16, but File.ReadAllText handles the BOM automatically
                var originContent = File.ReadAllText(originFilePath).Trim();

                // 4. Compare it against your known install path
                // StringComparison.OrdinalIgnoreCase is crucial because Windows paths are not case-sensitive
                if (originContent.Equals(installPath.Trim(), StringComparison.OrdinalIgnoreCase))
                    return directory;
            }
        }

        throw new DirectoryNotFoundException("Matching terminal directory not found.");
    }

    public string GetTerminalPath()
    {
        if (!File.Exists(terminalPath))
            throw new FileNotFoundException($"Terminal executable not found in '{terminalPath}'.");
        return terminalPath;
    }
}