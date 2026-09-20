using MTWalk.Configuration;
using MTWalk.Terminal;

namespace MTWalk.Tests;

class TerminalTests
{
    [Test]
    public void TerminalProvider_GetDataFolder_ReturnsCorrectPath()
    {
        // Arrange
        var knownInstallPath = @"C:\Program Files\MetaTrader 5";
        if (Directory.Exists(knownInstallPath) is false)
            Assert.Ignore($"Known install path '{knownInstallPath}' does not exist. Test ignored.");
        var terminalProvider = new TerminalProvider();
        // Act
        var actualDataFolder = terminalProvider.GetDataFolder();
        // Assert
        Assert.That(actualDataFolder, Is.Not.Null);
    }

    [Test]
    public async Task TerminalExecutor_ExecuteTest_InitializesTerminal()
    {
        // Arrange
        var terminalExecutor = new TerminalExecutor(new DummyTerminalProvider());
        // Act
        var parameteres = new DummyConfigurationParameters(new(2025, 1, 1), new(2025, 3, 1));
        var configurationBuilder = new ConfigurationBuilder().AddParameters(parameteres);
        Assert.DoesNotThrowAsync(async () => await terminalExecutor.Execute(configurationBuilder.ToString()));
    }

    [Test]
    public async Task ExpertProvider_SubDirectories_GetFileInfos()
    {
        var expertProvider = new ExpertProvider(new DummyTerminalProvider());
        var s = expertProvider.GetFileInfos();
    }

    class DummyTerminalProvider : ITerminalProvider
    {
        public string GetDataFolder()
        {
            var appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var terminalDirectory = Path.Combine(appData, @"MetaQuotes\Terminal");
            var dataFolder = new DirectoryInfo(terminalDirectory).EnumerateDirectories().First(x => x.EnumerateFiles("origin.txt").Any());
            return dataFolder.FullName;
        }

        public string GetTerminalPath() => @"C:\Program Files\MetaTrader 5\terminal64.exe";
    }

    record DummyConfigurationParameters(DateTime FromDate, DateTime ToDate) : IConfigurationParameters
    {
        public string Parse() => throw new NotImplementedException();
    }
}
