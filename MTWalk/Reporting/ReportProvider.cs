using System;
using System.Collections.Generic;
using System.Text;

namespace MTWalk.Reporting;

interface IReportProvider
{
    string GetReportFileName(TimeRange timeRange);
    (DateTime Date, double Balance)[] GetBalanceData(TimeRange timeRange);
}

class ReportProvider() : IReportProvider
{
    const string RelativeDirectory = @"MQL5\Profiles\Tester";

    public (DateTime Date, double Balance)[] GetBalanceData(TimeRange timeRange)
    {
        throw new NotImplementedException();
    }

    public string GetReportFileName(TimeRange timeRange)
        => @$"{RelativeDirectory}\Forward_{timeRange.Start:yyyyMMdd}_{timeRange.End:yyyyMMdd}.mqh";
}
