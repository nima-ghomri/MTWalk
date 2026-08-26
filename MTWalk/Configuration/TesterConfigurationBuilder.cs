namespace MTWalk.Configuration;

class TesterConfigurationBuilder : IConfigurationBuilder
{
    public string BuildConfiguration(IConfigurationParameters parameters)
    {
        if (parameters is not TesterConfigurationParameters testParameters)
            throw new NotImplementedException("TestConfigurationBuilder does not support the provided parameter type.");
        var configurationContent = $"""
            [Tester]
            Expert={testParameters.Expert}
            Symbol={testParameters.Symbol}
            Period={testParameters.Period}
            Model={(int)testParameters.Model}
            Optimization=0
            FromDate={testParameters.FromDate:yyyy.MM.dd}
            ToDate={testParameters.ToDate:yyyy.MM.dd}
            ReplaceReport=1
            ShutdownTerminal=1
            """;
        var configurationPath = Path.Combine(Path.GetTempPath(), "dummy_config.ini");
        File.WriteAllText(configurationPath, configurationContent);
        return configurationPath;
    }
}