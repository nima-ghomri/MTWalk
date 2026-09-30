namespace MTWalk.Reporting;

abstract class ReportReader<T>(string dataFolder,
                   string reportFolderRelative,
                   string prefix,
                   string extension,
                   TimeRange timeRange) : IReportReader<T> where T : IReport
{
    public string RelativePath { get; } = Path.Combine(reportFolderRelative,
        @$"{prefix}_{timeRange.Start:yyyyMMdd}_{timeRange.End:yyyyMMdd}.{extension}");
    string FullPath => Path.Combine(dataFolder, RelativePath);

    protected abstract T Read(string content);

    public T Read()
    {
        var content = File.ReadAllText(FullPath);
        return Read(content);
    }

}
