using MTWalk.Serialization;

namespace MTWalk.IO;

interface IFileProvider
{
    IFile GetWalkParametersFile();
    IFormatFile<XmlFormat> GetOptimizationReportFile(TimeRange timeRange);
    IFormatFile<SetFormat> GetParametersFile(TimeRange timeRange);
    IFormatFile<HtmFomat> GetForwardReportFile(TimeRange timeRange);
    IFile GetForwardConfigurationFile(TimeRange timeRange);
    IFile GetOptimizationConfigurationFile(TimeRange timeRange);
}
