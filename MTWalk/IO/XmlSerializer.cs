using System.Globalization;
using System.Xml.Linq;

namespace MTWalk.IO;

public record OptimizationPass(int Pass,
                               double Result,
                               double Profit,
                               double ExpectedPayoff,
                               double ProfitFactor,
                               double RecoveryFactor,
                               double SharpeRatio,
                               double Custom,
                               double EquityDrawdown,
                               int Trades,
                               Input[] Inputs)
{
    public object this[OptimizationCriterion value]
            => value switch
            {
                OptimizationCriterion.BalanceMax => Profit,
                OptimizationCriterion.ProfitFactorMax => ProfitFactor,
                OptimizationCriterion.ExpectedPayoffMax => ExpectedPayoff,
                OptimizationCriterion.DrawdownMin => EquityDrawdown,
                OptimizationCriterion.RecoveryFactorMax => RecoveryFactor,
                OptimizationCriterion.SharpeRatioMax => SharpeRatio,
                OptimizationCriterion.CustomMax => Custom,
                OptimizationCriterion.ComplexCriterionMax => Result,
                _ => throw new NotImplementedException(),
            };
}

record XmlFormat(OptimizationPass[] Passes) : IFormat;

class XmlSerializer : ISerializer<XmlFormat>
{
    public XmlFormat Deserialize(string content)
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


            // 3. Dynamically capture and infer types for inputs
            List<Input> inputs = [];
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
                        inputs.Add(new Input(inputName, (int)number));
                    }
                    else
                    {
                        // It's a floating-point number
                        typedValue = number;
                        inputs.Add(new Input(inputName, number));
                    }
                }
                else
                {
                    // Fallback: If it's not a number, keep it as a string
                    typedValue = inputValue;
                    inputs.Add(new Input(inputName, inputValue));
                }

            }


            // Safely parse standard columns using InvariantCulture to handle '.' decimals universally
            var optPass = new OptimizationPass(int.TryParse(cells[headers.IndexOf("Pass")], out int pass) ? pass : 0,
                                               double.TryParse(cells[headers.IndexOf("Result")], NumberStyles.Any, CultureInfo.InvariantCulture, out double res) ? res : 0,
                                               double.TryParse(cells[headers.IndexOf("Profit")], NumberStyles.Any, CultureInfo.InvariantCulture, out double prof) ? prof : 0,
                                               double.TryParse(cells[headers.IndexOf("Expected Payoff")], NumberStyles.Any, CultureInfo.InvariantCulture, out double ep) ? ep : 0,
                                               double.TryParse(cells[headers.IndexOf("Profit Factor")], NumberStyles.Any, CultureInfo.InvariantCulture, out double pf) ? pf : 0,
                                               double.TryParse(cells[headers.IndexOf("Recovery Factor")], NumberStyles.Any, CultureInfo.InvariantCulture, out double rf) ? rf : 0,
                                               double.TryParse(cells[headers.IndexOf("Sharpe Ratio")], NumberStyles.Any, CultureInfo.InvariantCulture, out double sr) ? sr : 0,
                                               double.TryParse(cells[headers.IndexOf("Custom")], NumberStyles.Any, CultureInfo.InvariantCulture, out double c) ? c : 0,
                                               double.TryParse(cells[headers.IndexOf("Equity DD %")], NumberStyles.Any, CultureInfo.InvariantCulture, out double dd) ? dd : 0,
                                               int.TryParse(cells[tradesIndex], out int trades) ? trades : 0,
                                               inputs.ToArray());

            results.Add(optPass);
        }

        return new(results.ToArray());
    }

    public string Serialize(XmlFormat report)
    {
        throw new NotImplementedException();
    }
}
