namespace MTWalk.Reporting;

class ReportContextFactory(ITerminalProvider terminalProvider) : IReportContextFactory
{
    const string BaseDirectoryRelative = @"MQL5\Profiles\Tester\MTWalk";
    static string GetReportFolderRelative(TesterRequest r)
    {
        var expertFolder = Path.GetFileNameWithoutExtension(r.Expert);
        var walkFolder = $"{r.Symbol}_{r.TimeFrame}_{r.Model}_{r.Start:yyyyMMdd}_{r.End:yyyyMMdd}";
        return Path.Combine(BaseDirectoryRelative, expertFolder, walkFolder);
    }

    public ContextReport CreateContext(TesterRequest testerRequest)
        => new ContextReport(terminalProvider.GetDataFolder(), GetReportFolderRelative(testerRequest));
}
