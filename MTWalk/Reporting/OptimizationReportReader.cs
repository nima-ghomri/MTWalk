namespace MTWalk.Reporting;

class OptimizationReportReader(string dataFolder, string reportFolderRelative, TimeRange timeRange)
    : ReportReader<OptimizationReport>(dataFolder, reportFolderRelative, "Optimization", "xml", timeRange)
{
    static readonly XmlReportParser xmlReportParser = new();
    protected override OptimizationReport Read(string content)
        => xmlReportParser.Parser(content);
}
