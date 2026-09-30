using System.Threading.Channels;

namespace MTWalk.Reporting;

class ReportContextFactory(ITerminalProvider terminalProvider) : IReportContextFactory
{
    const string BaseDirectoryRelative = @"MQL5\Profiles\Tester\MTWalk";
    static string GetReportFolderRelative(TesterRequest r)
    {
        var expertFolder = Path.GetFileNameWithoutExtension(r.Expert);
        var walkFolder = $"{r.Symbol}_{r.TimeFrame}_{r.Model}_{r.Start:yyyyMMdd}_{r.End:yyyyMMdd}";
        return Path.Combine(BaseDirectoryRelative, expertFolder, walkFolder);
    }

    IReportContext IReportContextFactory.CreateContext(TesterRequest testerRequest)
        => new ContextReport(terminalProvider.GetDataFolder(), GetReportFolderRelative(testerRequest));
}

interface IReportContext
{
    OptimizationReportReader CreateOptimizationReportReader(TimeRange timeRange);
    SingleReportReader CreateSingleReportReader(TimeRange timeRange);
}

class ContextReport(string dataFolder, string reportFolderRelative) : IReportContext
{
    readonly string reportFolder = Path.Combine(dataFolder, reportFolderRelative);

    public OptimizationReportReader CreateOptimizationReportReader(TimeRange timeRange)
    {
        Directory.CreateDirectory(reportFolder);
        return new(dataFolder, reportFolderRelative, timeRange);
    }

    public SingleReportReader CreateSingleReportReader(TimeRange timeRange)
    {
        Directory.CreateDirectory(reportFolder);
        return new(dataFolder, reportFolderRelative, timeRange);
    }
}

interface IReportReader<T>
{
    string RelativePath { get; }

    T Read();
}

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

class OptimizationReportReader(string dataFolder, string reportFolderRelative, TimeRange timeRange)
    : ReportReader<OptimizationReport>(dataFolder, reportFolderRelative, "Optimization", "xml", timeRange)
{
    protected override OptimizationReport Read(string content)
    {
        throw new NotImplementedException();
    }
}

class SingleReportReader(string dataFolder, string reportFolderRelative, TimeRange timeRange)
    : ReportReader<SingleReport>(dataFolder, reportFolderRelative, "Forward", "htm", timeRange)
{
    static readonly HtmReportParser htmReportParser = new();

    protected override SingleReport Read(string content)
    {
        return htmReportParser.Parse(content);
    }
}

interface IReport;

public record SingleReport(DealRecord[] Deals) : IReport;

public record OptimizationReport() : IReport;
