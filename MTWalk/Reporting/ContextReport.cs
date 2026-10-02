namespace MTWalk.Reporting;

class ContextReport(string dataFolder, string reportFolderRelative) 
{
    readonly string reportFolder = Path.Combine(dataFolder, reportFolderRelative);

    public OptimizationReportStream CreateOptimizationReportStream(TimeRange timeRange)
    {
        Directory.CreateDirectory(reportFolder);
        return new(dataFolder, reportFolderRelative, timeRange);
    }

    public SingleReportStream CreateSingleReportStream(TimeRange timeRange)
    {
        Directory.CreateDirectory(reportFolder);
        return new(dataFolder, reportFolderRelative, timeRange);
    }

    public ParametersReportStream CreateParametersReportStream(TimeRange timeRange)
    {
        Directory.CreateDirectory(reportFolder);
        return new(dataFolder, reportFolderRelative, timeRange);
    }
}
