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
        var terminalExecutor = new TerminalExecutor(new DummyTerminalProvider(), new DummyConfigurationBuilder());
        // Act
        var configuration = new DummyConfigurationParameters(new(2025, 1, 1), new(2025, 3, 1));
        Assert.DoesNotThrowAsync(async () => await terminalExecutor.Execute(configuration));
    }

    class DummyTerminalProvider : ITerminalProvider
    {
        public string GetDataFolder() => throw new NotImplementedException();

        public string GetTerminalPath() => @"C:\Program Files\MetaTrader 5\terminal64.exe";
    }

    record DummyConfigurationParameters(DateTime FromDate, DateTime ToDate) : IConfigurationParameters;

    class DummyConfigurationBuilder : IConfigurationBuilder
    {
        public string BuildConfiguration(IConfigurationParameters parameters)
        {
            if(parameters is not DummyConfigurationParameters dummyParams)
                throw new ArgumentException("Invalid configuration parameters type.");
            var configurationContent = $"""
            [Tester]
            Expert=Examples\MACD\MACD Sample.ex5
            Symbol=EURUSD
            Period=H1
            Model=1
            Optimization=0
            FromDate={dummyParams.FromDate:yyyy.MM.dd}
            ToDate={dummyParams.ToDate:yyyy.MM.dd}
            Forward=2025.02.01
            ReplaceReport=1
            ShutdownTerminal=1
            """;
            var configurationPath = Path.Combine(Path.GetTempPath(), "dummy_config.ini");
            File.WriteAllText(configurationPath, configurationContent);
            return configurationPath;
        }
    }
}
