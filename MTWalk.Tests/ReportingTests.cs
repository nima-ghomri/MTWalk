using MTWalk.Reporting;
using MTWalk.Windowing;

namespace MTWalk.Tests;

class ReportingTests
{
    [Test]
    public void ReportProvider_TimeRange_CreateFileName()
    {
        var reportProvider = new ReportProvider();
        var report = reportProvider.GetReportFileName(new(new DateTime(2025, 1, 1), new DateTime(2025, 1, 31)));
        Assert.That(report, Is.EqualTo("Report_2025-01-01_2025-01-31.mqh"));
    }
}
