using System.Diagnostics;

namespace MTWalk.Tests;

class TerminalCommandLineTests
{
    const string Symbol = "EURUSD";
    const string EAName = @"Examples\MACD\MACD Sample.ex5";
    const string TerminalPath = @"C:\Program Files\MetaTrader 5\terminal64.exe";

    [Test]
    public async Task SingleTest_SimpleConfiguration_GeneratesReport()
    {
        var start = new DateTime(2025, 1, 1);
        var end = new DateTime(2025, 2, 1);

        // 1. Run Optimization
        var directory = Directory.GetCurrentDirectory();
        var setFileName = "test_params.set";
        var setPath = Path.Combine(directory, setFileName);
        var configurationPath = Path.Combine(directory, "test_configuration.ini");
        var reportPath = Path.Combine("MQL5", "Profiles", "Tester", "test_report.xml");

        // 2. Generate the .set file for optimization
        var setContent = $"""
        InpLots=0.1||0.1||0.01||1||Y
        InpTakeProfit=50||50||1||500||N
        InpTrailingStop=30||30||1||300||N
        InpMACDOpenLevel=3||3||1||30||N
        InpMACDCloseLevel=2||2||1||20||N
        InpMATrendPeriod=26||26||1||260||N
        """;
        File.WriteAllText(setPath, setContent);

        // Generate the .ini file pointing to the .set file
        var configurationContent = $"""
        [Tester]
        Expert={EAName}
        ExpertParameters={setFileName}
        Symbol={Symbol}
        Period=H1
        Model=1
        Optimization=0
        FromDate={start:yyyy.MM.dd}
        ToDate={end:yyyy.MM.dd}
        Report={reportPath}
        ReplaceReport=1
        ShutdownTerminal=1
        """;
        File.WriteAllText(configurationPath, configurationContent);

        var processInfo = new ProcessStartInfo
        {
            FileName = TerminalPath,
            Arguments = $"/config:\"{configurationPath}\"",
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using var process = Process.Start(processInfo);
        if (process != null)
        {
            await Task.Run(process.WaitForExit);
        }

        Assert.That(File.Exists(reportPath), Is.True, "Report file was not generated.");
    }
}
