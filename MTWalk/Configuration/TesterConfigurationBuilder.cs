namespace MTWalk.Configuration;

class TesterConfigurationBuilder : IConfigurationBuilder
{
    public string BuildConfiguration(IConfigurationParameters parameters)
    {
        if (parameters is not TesterConfigurationParameters tester)
            throw new NotImplementedException("TestConfigurationBuilder does not support the provided parameter type.");
        var configurationContent = $"""
            [Tester]
            Expert={tester.Expert}
            Symbol={tester.Symbol}
            Period={tester.Period}
            Model={(int)tester.Model}
            Optimization={(int)tester.Optimization}
            FromDate={tester.FromDate:yyyy.MM.dd}
            ToDate={tester.ToDate:yyyy.MM.dd}
            Report={tester.Report}
            ReplaceReport={(tester.ReplaceReport ? 1 : 0)}
            ShutdownTerminal={(tester.ShutdownTerminal ? 1 : 0)}
            """;
        var configurationPath = Path.Combine(Path.GetTempPath(), "dummy_config.ini");
        File.WriteAllText(configurationPath, configurationContent);
        return configurationPath;
    }
}

/*
;+------------------------------------------------------------------------------+
;| Start Expert Advisor testing or optimization                                 |
;+------------------------------------------------------------------------------+

[Tester]
;--- The Expert Advisor is located in platform_data_directory\MQL5\Experts\Examples\MACD\
Expert=Examples\MACD\MACD Sample
;--- The Expert Advisor parameters are available in platform_installatoin_directory\MQL5\Profiles\Tester\
ExpertParameters=macd sample.set
;--- The symbol for testing/optimization
Symbol=EURUSD
;--- The timeframe for testing/optimization
Period=M1
;--- Emulated account number
Login=123456
;--- Initial deposit
Deposit=10000
;--- Leverage for testing
Leverage=1:100
;--- The "All Ticks" mode
Model=0
;--- Execution of trade orders with a random delay
ExecutionMode=1
;--- Genetic optimization
Optimization=2
;--- Optimization criterion - Maximum balance value
OptimizationCriterion=0
;--- Dates of beginning and end of the testing range
FromDate=2011.01.01
ToDate=2011.04.01
;--- Custom mode of forward testing
ForwardMode=4
;--- Start date of forward testing
ForwardDate=2011.03.01
;--- A file with a report will be saved to the folder platform_installation_directory
Report=test_macd
;--- If the specified report already exists, it will be overwritten
ReplaceReport=1
;--- Set automatic platform shutdown upon completion of testing/optimization
ShutdownTerminal=1
 */