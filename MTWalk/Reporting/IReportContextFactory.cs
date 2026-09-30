namespace MTWalk.Reporting;

interface IReportContextFactory
{
    IReportContext CreateContext(TesterRequest testerRequest);
}