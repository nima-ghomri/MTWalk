using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Globalization;
using System.Text;
using System.Xml;
using HtmlAgilityPack;

namespace MTWalk.Reporting;

interface IReportProvider
{
    string GetReportFileName(TimeRange timeRange);
    DealRecord[] GetBalanceData(TimeRange timeRange);
}

class ReportProvider(ITerminalProvider terminalProvider, IHtmReportParser reportParser) : IReportProvider
{
    const string RelativeDirectory = @"MQL5\Profiles\Tester";

    public DealRecord[] GetBalanceData(TimeRange timeRange)
    {
        var directory = terminalProvider.GetDataFolder();
        var path = Path.Combine(directory, GetReportFileName(timeRange));
        var content = File.ReadAllText(path);
        return reportParser.Parse(content).Deals;
    }

    public string GetReportFileName(TimeRange timeRange)
        => Path.Combine(RelativeDirectory, @$"Forward_{timeRange.Start:yyyyMMdd}_{timeRange.End:yyyyMMdd}.htm");
}

interface IHtmReportParser
{
    SingleReport Parse(string content);
}

record SingleReport(DealRecord[] Deals);
record DealRecord(DateTime Date, double Balance);
class HtmReportParser : IHtmReportParser
{
    public SingleReport Parse(string content)
    {
        var results = new List<DealRecord>();
        var doc = new HtmlDocument();
        doc.LoadHtml(content);

        // 1. Find the <th> element containing "Deals"
        var dealsHeader = doc.DocumentNode
            .Descendants("th")
            .FirstOrDefault(th => th.InnerText.Trim().Equals("Deals", StringComparison.OrdinalIgnoreCase))
            ?? throw new InvalidOperationException("Deals header not found");

        // 2. Get the table containing the Deals header
        var table = dealsHeader.Ancestors("table").FirstOrDefault()
            ?? throw new InvalidOperationException("Table containing deals not found");

        // 3. Find all rows in this table
        var rows = table.Elements("tr").ToList();
        int dealsHeaderIndex = rows.IndexOf(dealsHeader.ParentNode);

        // Rows after the header:
        // rows[dealsHeaderIndex + 1] is the table column titles (Time, Deal, Symbol, etc.)
        // rows[dealsHeaderIndex + 2...] are the actual deal entries
        for (int i = dealsHeaderIndex + 2; i < rows.Count; i++)
        {
            var row = rows[i];
            var cells = row.Elements("td").ToList();

            // Deal rows have 13 columns:
            // Cell[0] = Time, Cell[11] = Balance
            if (cells.Count == 13)
            {
                string rawTime = cells[0].InnerText.Trim();
                string rawBalance = cells[11].InnerText.Trim();

                // Clean spaces in balance (e.g., "10 000.00" -> "10000.00")
                rawBalance = rawBalance.Replace(" ", "").Replace("&nbsp;", "");

                if (DateTime.TryParseExact(rawTime, "yyyy.MM.dd HH:mm:ss",
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time) &&
                    double.TryParse(rawBalance, NumberStyles.Any, CultureInfo.InvariantCulture, out double balance))
                {
                    results.Add(new DealRecord(time, balance));
                }
            }
        }

        return new SingleReport(results.ToArray());
    }
}

