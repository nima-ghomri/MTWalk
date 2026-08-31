namespace MTWalk.Reporting;

interface IReportProvider
{
    string GetReportFileName(TimeRange timeRange);
    SingleReport GetForwardReport(TimeRange timeRange);
}
