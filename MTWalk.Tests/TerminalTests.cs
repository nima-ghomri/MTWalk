using MTWalk.Terminal;
using System.Xml.Linq;

namespace MTWalk.Tests;

class TerminalTests
{
    [Test]
    public async Task TerminalExecutor_ExecuteTest_InitializesTerminal()
    {
        // Arrange
        var terminalExecutor = new TerminalExecutor(new DummyTerminalProvider(), new DummyConfigurationBuilder());
        // Act
        var test = new Test(
               new TerminalParameters(@"Examples\MACD\MACD Sample.ex5",
               "EURUSD",
               "H1",
               new(2025, 1, 1),
               new(2025, 3, 1),
               new(2025, 2, 1),
               Delays.Zero,
               Modelling.EveryTick,
               0.0,
               "USD",
               100),
               new WalkingParamters());
        Assert.DoesNotThrowAsync(async () => await terminalExecutor.ExecuteTest(test));
    }l

    class DummyTerminalProvider : ITerminalProvider
    {
        public string GetTerminalPath() => @"C:\Program Files\MetaTrader 5\terminal64.exe";
    }

    class DummyConfigurationBuilder : IConfigurationBuilder
    {
        public string BuildConfiguration(Test parameters)
        {
            var configurationContent = $"""
            [Tester]
            Expert={parameters.TerminalParameters.Expert}
            Symbol={parameters.TerminalParameters.Symbol}
            Period=H1
            Model=1
            Optimization=0
            FromDate={parameters.TerminalParameters.Start:yyyy.MM.dd}
            ToDate={parameters.TerminalParameters.End:yyyy.MM.dd}
            ReplaceReport=1
            ShutdownTerminal=1
            """;
            var configurationPath = Path.Combine(Path.GetTempPath(), "dummy_config.ini");
            File.WriteAllText(configurationPath, configurationContent);
            return configurationPath;
        }
    }
}
