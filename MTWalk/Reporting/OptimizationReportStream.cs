namespace MTWalk.Reporting;

class OptimizationReportStream(string dataFolder, string reportFolderRelative, TimeRange timeRange)
    : ReportStream<OptimizationReport>(dataFolder,
                                       reportFolderRelative,
                                       "Optimization",
                                       "xml",
                                       timeRange,
                                       new XmlReportSerializer());
