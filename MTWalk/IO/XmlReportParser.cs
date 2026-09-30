using System.Globalization;
using System.Xml.Linq;

namespace MTWalk.IO;

class XmlReportParser
{
    public OptimizationReport Parser(string content)
    {
        XDocument doc = XDocument.Parse(content);
        XNamespace ns = "urn:schemas-microsoft-com:office:spreadsheet";

        var rows = doc.Descendants(ns + "Row").ToList();
        if (rows.Count < 2) return new([]);

        // 1. Extract the Headers
        var headers = rows[0].Elements(ns + "Cell")
                             .Select(c => c.Element(ns + "Data")?.Value ?? "")
                             .ToList();

        // MT5 inputs always come AFTER the "Trades" column
        int tradesIndex = headers.IndexOf("Trades");
        if (tradesIndex == -1)
            return new([]); // Safety check to prevent out-of-bounds errors

        var results = new List<OptimizationPass>();

        // 2. Parse the Data Rows
        foreach (var row in rows.Skip(1))
        {
            var cells = row.Elements(ns + "Cell")
                           .Select(c => c.Element(ns + "Data")?.Value ?? "0")
                           .ToList();

            // Ignore incomplete rows or footer summaries
            if (cells.Count <= tradesIndex) continue;

            // Safely parse standard columns using InvariantCulture to handle '.' decimals universally
            var optPass = new OptimizationPass
            {
                Pass = int.TryParse(cells[headers.IndexOf("Pass")], out int pass) ? pass : 0,
                Result = double.TryParse(cells[headers.IndexOf("Result")], NumberStyles.Any, CultureInfo.InvariantCulture, out double res) ? res : 0,
                Profit = double.TryParse(cells[headers.IndexOf("Profit")], NumberStyles.Any, CultureInfo.InvariantCulture, out double prof) ? prof : 0,
                ExpectedPayoff = double.TryParse(cells[headers.IndexOf("Expected Payoff")], NumberStyles.Any, CultureInfo.InvariantCulture, out double ep) ? ep : 0,
                ProfitFactor = double.TryParse(cells[headers.IndexOf("Profit Factor")], NumberStyles.Any, CultureInfo.InvariantCulture, out double pf) ? pf : 0,
                RecoveryFactor = double.TryParse(cells[headers.IndexOf("Recovery Factor")], NumberStyles.Any, CultureInfo.InvariantCulture, out double rf) ? rf : 0,
                SharpeRatio = double.TryParse(cells[headers.IndexOf("Sharpe Ratio")], NumberStyles.Any, CultureInfo.InvariantCulture, out double sr) ? sr : 0,
                EquityDrawdown = double.TryParse(cells[headers.IndexOf("Equity DD %")], NumberStyles.Any, CultureInfo.InvariantCulture, out double dd) ? dd : 0,
                Custom = double.TryParse(cells[headers.IndexOf("Custom")], NumberStyles.Any, CultureInfo.InvariantCulture, out double c) ? c : 0,
                Trades = int.TryParse(cells[tradesIndex], out int trades) ? trades : 0
            };

            // 3. Dynamically capture and infer types for inputs
            for (int i = tradesIndex + 1; i < cells.Count; i++)
            {
                // Prevent out-of-bounds if XML has trailing empty cells without headers
                if (i >= headers.Count) break;

                string inputName = headers[i];
                string inputValue = cells[i];

                object typedValue;

                // Attempt to parse as a number using invariant culture
                if (double.TryParse(inputValue, NumberStyles.Any, CultureInfo.InvariantCulture, out double number))
                {
                    // Check if there is a fractional remainder
                    if (number % 1 == 0)
                    {
                        // It's a whole number (covers ints, bools, and enums)
                        typedValue = (int)number;
                    }
                    else
                    {
                        // It's a floating-point number
                        typedValue = number;
                    }
                }
                else
                {
                    // Fallback: If it's not a number, keep it as a string
                    typedValue = inputValue;
                }

                optPass.Inputs[inputName] = typedValue;
            }

            results.Add(optPass);
        }

        return new(results.ToArray());
    }
}
