namespace MTWalk.Reporting;

class ReportProvider(ITerminalProvider terminalProvider, IHtmReportParser reportParser) : IReportProvider
{
    const string RelativeDirectory = @"MQL5\Profiles\Tester";

    public SingleReport GetForwardReport(TimeRange timeRange)
    {
        var directory = terminalProvider.GetDataFolder();
        var path = Path.Combine(directory, GetReportFileName(timeRange));
        var content = File.ReadAllText(path);
        return reportParser.Parse(content);
    }

    public string GetReportFileName(TimeRange timeRange)
        => Path.Combine(RelativeDirectory, @$"Forward_{timeRange.Start:yyyyMMdd}_{timeRange.End:yyyyMMdd}.htm");
}
