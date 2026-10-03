namespace MTWalk.Reporting;

class SingleReportStream(string dataFolder, string reportFolderRelative, TimeRange timeRange)
    : ReportStream<HtmFile>(dataFolder,
                                 reportFolderRelative,
                                 "Forward",
                                 "htm",
                                 timeRange,
                                 new HtmSerializer());