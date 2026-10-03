namespace MTWalk.Reporting;

class OptimizationReportStream(string dataFolder, string reportFolderRelative, TimeRange timeRange)
    : ReportStream<XmlFile>(dataFolder,
                                       reportFolderRelative,
                                       "Optimization",
                                       "xml",
                                       timeRange,
                                       new XmlSerializer());
