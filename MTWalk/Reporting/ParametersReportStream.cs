namespace MTWalk.Reporting;

class ParametersReportStream(string dataFolder, string reportFolderRelative, TimeRange timeRange)
    : ReportStream<SetFile>(dataFolder,
                                 reportFolderRelative,
                                 "Parameters",
                                 "set",
                                 timeRange,
                                 new SetSerializer());
