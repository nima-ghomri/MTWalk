namespace MTWalk.Reporting;

interface IReportContextFactory
{
    ContextReport CreateContext(TesterRequest testerRequest);
}