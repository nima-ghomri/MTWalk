using MTWalk.Serialization;

namespace MTWalk.IO;

interface IFileProvider
{
    IMTFile GetWalkParametersFile();
    IFormatFile<XmlFormat> GetOptimizationReportFile(TimeRange timeRange);
    IFormatFile<SetFormat> GetParametersFile(TimeRange timeRange);
    IFormatFile<HtmFomat> GetForwardReportFile(TimeRange timeRange);
    IMTFile GetForwardConfigurationFile(TimeRange timeRange);
    IMTFile GetOptimizationConfigurationFile(TimeRange timeRange);
}
