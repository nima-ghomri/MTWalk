namespace MTWalk.Reporting;

class SingleReportReader(string dataFolder, string reportFolderRelative, TimeRange timeRange)
    : ReportReader<SingleReport>(dataFolder, reportFolderRelative, "Forward", "htm", timeRange)
{
    static readonly HtmReportParser htmReportParser = new();

    protected override SingleReport Read(string content)
        => htmReportParser.Parse(content);
}
