namespace MTWalk.Reporting;

class ContextReport(string dataFolder, string reportFolderRelative) 
{
    readonly string reportFolder = Path.Combine(dataFolder, reportFolderRelative);

    public OptimizationReportReader CreateOptimizationReportReader(TimeRange timeRange)
    {
        Directory.CreateDirectory(reportFolder);
        return new(dataFolder, reportFolderRelative, timeRange);
    }

    public SingleReportReader CreateSingleReportReader(TimeRange timeRange)
    {
        Directory.CreateDirectory(reportFolder);
        return new(dataFolder, reportFolderRelative, timeRange);
    }
}
