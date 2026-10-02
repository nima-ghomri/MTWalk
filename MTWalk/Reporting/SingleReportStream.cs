namespace MTWalk.Reporting;

class SingleReportStream(string dataFolder, string reportFolderRelative, TimeRange timeRange)
    : ReportStream<SingleReport>(dataFolder,
                                 reportFolderRelative,
                                 "Forward",
                                 "htm",
                                 timeRange,
                                 new HtmReportSerializer());