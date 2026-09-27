using MTWalk.IO;
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
        Assert.That(report, Is.EqualTo("Forward_20250101_20250131.xml"));
    }

    class DummyReportParser : IHtmReportParser
    {
        public SingleReport Parse(string content) => throw new NotImplementedException();
    }
}
