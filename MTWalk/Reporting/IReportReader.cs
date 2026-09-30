namespace MTWalk.Reporting;

interface IReportReader<T>
{
    string RelativePath { get; }

    T Read();
}
