namespace MTWalk.Reporting;

class ParametersReportStream(string dataFolder, string reportFolderRelative, TimeRange timeRange)
    : ReportStream<InputSet>(dataFolder,
                                 reportFolderRelative,
                                 "Parameters",
                                 "set",
                                 timeRange,
                                 new SetParametersSerializer());
