namespace MTWalk.Reporting;

abstract class ReportStream<T>(string dataFolder,
                   string reportFolderRelative,
                   string label,
                   string extension,
                   TimeRange timeRange,
                   IReportSerializer<T> serializer) : IReportReader<T> where T : IReport
{
    public string RelativePath { get; } = Path.Combine(reportFolderRelative,
        @$"{label}_{timeRange.Start:yyyyMMdd}_{timeRange.End:yyyyMMdd}.{extension}");
    string FullPath => Path.Combine(dataFolder, RelativePath);

    public T Read()
    {
        var content = File.ReadAllText(FullPath);
        return serializer.Deserialize(content);
    }

    public void Save(T report)
    {
        var content = serializer.Serialize(report);
        File.WriteAllText(FullPath, content);
    }
}
