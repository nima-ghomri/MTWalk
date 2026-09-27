namespace MTWalk.MetaQuotes;

/// <summary>
/// Forward testing mode (0 — off, 1 — 1/2 of the testing period, 2 — 1/3 of the testing period, 3 — 1/4 of the testing period, 4 — custom interval specified using the ForwardDate parameter).
/// </summary>
public enum ForwardMode
{
    No,
    _1_2,
    _1_3,
    _1_4,
    Custom,
}

/*
Expert — the file name of the Expert Advisor that will automatically run in the testing (optimization) mode. If this parameter is not present, testing will not run.
ExpertParameters — the name of the file that contains Expert Advisor parameters. This file must be located in the MQL5\Profiles\Tester folder of the platform installation directory.
Symbol — the name of the symbol that will be used as the main testing symbol. If this parameter is not added, the last selected symbol in the tester is used.
Login — this parameter communicates to the Expert Advisor the value of an account, on which testing is allegedly performed. The need for this parameter is set in the source MQL5 code of the Expert Advisor (in the AccountInfoInteger function).
ExecutionMode — trading mode emulated by the strategy tester (0 — normal, -1 — with a random delay in the execution of trading orders, >0 — trade execution delay in milliseconds, it cannot exceed 600 000).
OptimizationCriterion — optimization criterion: (0 — the maximum balance value, 1 — the maximum value of product of the balance and profitability, 2 — the product of the balance and expected payoff, 3 — the maximum value of the expression (100% - Drawdown)*Balance, 4 — the product of the balance and the recovery factor, 5 — the product of the balance and the Sharpe Ratio, 6 — a custom optimization criterion received from the OnTester() function in the Expert Advisor), 7 — the maximum of complex criterion.
FromDate — starting date of the testing range in format YYYY.MM.DD. If this parameter is not set, the date from the corresponding field of the strategy tester will be used.
ToDate — end date of the testing range in format YYYY.MM.DD. If this parameter is not set, the date from the corresponding field of the strategy tester will be used.
ForwardDate — starting date of forward testing in the format YYYY.MM.DD. The parameter is valid only if ForwardMode=4.
Report — the name of the file to save the report on testing or optimization results. The file is created in the trading platform directory. You can specify a path to save the file, relative to this directory, for example, \reports\tester.htm. The subdirectory where the report is saved should exist. If no extension is specified in the file name, the ".htm" extension is automatically used for testing reports, and ".xml" is used for optimization reports. If this parameter is not set, the testing report will not be saved as a file. If forward testing is enabled, its results will be saved in a separate file with the ".forward" suffix. For example, tester.forward.htm.
ReplaceReport — enable/disable overwriting of the report file (0 — disable, 1 — enable). If overwriting is forbidden and a file with the same name already exists, a number in square brackets will be added to the file name. For example, tester[1].htm. If this parameter is not set, default 0 is used (overwriting is not allowed).
ShutdownTerminal — enable/disable platform shutdown after completion of testing (0 — disable, 1 — enable). If this parameter is not set, the "0" value is used (shutdown disabled). If the testing/optimization process is manually stopped by a user, the value of this parameter is automatically reset to 0.
Deposit — initial deposit for testing optimization. The amount is specified in the account deposit currency. If the parameter is not specified, a value from the appropriate field of the strategy tester is used.
Currency — deposit currency for testing/optimization purposes. Specified as a three-letter name, e.g. EUR, USD, CHF etc. Please note that cross rates for converting profit and margin to the specified deposit currency must be available on the account, to ensure proper testing. If the parameter is not specified, a value from the appropriate field of the strategy tester is used.
Leverage — leverage for testing/optimization. For example, 1:100. If the parameter is not specified, a leverage from the appropriate field of the strategy tester is used.
UseLocal — enable/disable the use of local agents for testing and optimization (0 — disable, 1 — enable). If the parameter is not specified, current platform settings are used.
UseRemote — enable/disable use of remote agents for testing and optimization (0 — disable, 1 — enable). If the parameter is not specified, current platform settings are used.
UseCloud — enable/disable use of agents from the MQL5 Cloud Network (0 — disable, 1 — enable). If the parameter is not specified, current platform settings are used.
Visual — enable (1) or disable (0) the visual test mode. If the parameter is not specified, the current setting is used.
Port — the port, on which the local testing agent is running. The port should be specified for the parallel start of testing on different agents. For example, you can run parallel tests of the same Expert Advisor with different parameters. During a single test port can be omitted.
 */