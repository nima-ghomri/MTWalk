using System.Diagnostics.Metrics;

namespace MTWalk.Configuration;

/// <summary>
/// Represents the configuration parameters for the [Tester] section of a MetaTrader 5 .ini file.
/// This record is used to automatically configure and launch the MT5 Strategy Tester via the command line.
/// </summary>
/// <param name="Expert">The file name of the Expert Advisor to run (e.g., "Examples\MACD\MACD Sample.ex5").</param>
/// <param name="ExpertParameters">The name of the .set file containing EA parameters. Must be located in the MQL5\Profiles\Tester folder.</param>
/// <param name="Symbol">The name of the symbol that will be used for testing (e.g., "EURUSD").</param>
/// <param name="Period">The timeframe for the testing chart (e.g., "M1", "H1").</param>
/// <param name="Login">The emulated account number communicated to the Expert Advisor during the test.</param>
/// <param name="Deposit">The initial deposit amount for testing and optimization.</param>
/// <param name="Leverage">The leverage used for testing (e.g., "1:100").</param>
/// <param name="Model">Tick generation mode (0 = Every tick, 1 = 1 minute OHLC, 2 = Open price only, 3 = Math calculations, 4 = Every tick based on real ticks).</param>
/// <param name="ExecutionMode">Trading execution mode (0 = Normal, -1 = Random delay, >0 = Trade execution delay in milliseconds).</param>
/// <param name="Optimization">Optimization type (0 = Disabled, 1 = Slow complete algorithm, 2 = Fast genetic algorithm, 3 = All Market Watch symbols).</param>
/// <param name="OptimizationCriterion">Optimization target (0 = Max balance, 1 = Balance*Profitability, 5 = Balance*Sharpe Ratio, 6 = Custom OnTester, etc.).</param>
/// <param name="FromDate">The starting date of the testing/optimization range.</param>
/// <param name="ToDate">The end date of the testing/optimization range.</param>
/// <param name="ForwardMode">Forward testing split (0 = Off, 1 = 1/2 period, 2 = 1/3 period, 3 = 1/4 period, 4 = Custom interval specified by ForwardDate).</param>
/// <param name="ForwardDate">The starting date of forward testing. Only valid if ForwardMode is set to 4.</param>
/// <param name="Report">The file name/path to save the testing (.htm) or optimization (.xml) report.</param>
/// <param name="ReplaceReport">If true, overwrites the report file if it already exists; otherwise, adds a number suffix (e.g., report[1].xml).</param>
/// <param name="ShutdownTerminal">If true, automatically closes the MetaTrader 5 terminal upon completion of the test or optimization.</param>
record TestConfigurationParameters(
    string Expert,
    string? ExpertParameters,
    string Symbol,
    TimeFrame Period,
    int Login,
    double Deposit,
    int Leverage,
    Model Model,
    ExecutionMode ExecutionMode,
    Optimization Optimization,
    OptimizationCriterion OptimizationCriterion,
    DateTime FromDate,
    DateTime ToDate,
    ForwardMode ForwardMode,
    DateTime? ForwardDate,
    string? Report,
    bool ReplaceReport,
    bool ShutdownTerminal) : IConfigurationParameters
{
    string IConfigurationParameters.Parse() => $"""
            [Tester]
            Expert={Expert}
            ExpertParameters={ExpertParameters}
            Symbol={Symbol}
            Period={Period}
            Login={Login}
            Deposit={Deposit}
            Leverage=1:{Leverage}
            Model={(int)Model}
            ExecutionMode={(int)ExecutionMode}
            Optimization={(int)Optimization}
            OptimizationCriterion={(int)OptimizationCriterion}
            FromDate={FromDate:yyyy.MM.dd}
            ToDate={ToDate:yyyy.MM.dd}
            ForwardMode={(int)ForwardMode}
            ForwardDate={ForwardDate:yyyy.MM.dd}
            Report={Report}
            ReplaceReport={(ReplaceReport ? 1 : 0)}
            ShutdownTerminal={(ShutdownTerminal ? 1 : 0)}
            """;
}
