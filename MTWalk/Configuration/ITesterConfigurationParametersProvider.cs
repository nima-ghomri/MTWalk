namespace MTWalk.Configuration;

interface ITesterConfigurationParametersProvider
{
    public IConfigurationParameters CreateForward(TesterRequest terminal, TimeRange timeRange);
    public IConfigurationParameters CreateOptimization(TesterRequest terminal, TimeRange timeRange);
}
