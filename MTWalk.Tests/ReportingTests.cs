using MTWalk.Reporting;
using MTWalk.Terminal;

namespace MTWalk.Tests;

class ReportingTests
{
    [Test]
    public void ReportProvider_TimeRange_CreateFileName()
    {
        var reportProvider = new ReportProvider(new TerminalProvider(), new DummyReportParser());
        var report = reportProvider.GetReportFileName(new(new DateTime(2025, 1, 1), new DateTime(2025, 1, 31)));
        Assert.That(report, Is.EqualTo("Report_20250101_20250131.xml"));
    }

    [Test]
    public void ReportProvider_TimeRange_GetBalanceData()
    {
        var reportProvider = new ReportProvider(new TerminalProvider(), new HtmReportParser());
        var balanceData = reportProvider.GetBalanceData(new(new DateTime(2025, 2, 3), new DateTime(2025, 2, 5)));
        Assert.That(balanceData, Is.Not.Null);
        Assert.That(balanceData.Length, Is.GreaterThan(0));
    }

    class DummyReportParser : IHtmReportParser
    {
        public SingleReport Parse(string content) => throw new NotImplementedException();
    }
}
