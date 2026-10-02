using System.Globalization;
using System.Text;

namespace MTWalk.IO;

class SetParametersSerializer : IReportSerializer<InputSet>
{
    public InputSet Deserialize(string content)
    {
        throw new NotImplementedException();
    }

    public string Serialize(InputSet report)
    {
        var sb = new StringBuilder();

        foreach (var group in report.Groups)
        {
            // Optional: Group headers in .set files are usually represented as comments
            // or specific group variables depending on how the sinput is defined in MQL5.
            if (group.Name is not null)
                sb.AppendLine($"; --- {group.Name} ---");

            foreach (var input in group.Children)
            {
                string stringValue = FormatMqlValue(input.Value);

                // MT5 .set files use standard Key=Value formatting
                // Note: If you need to support Strategy Tester optimization parameters,
                // the format expands to Key=Value||Start||Step||Stop
                sb.AppendLine($"{input.Name}={stringValue}");
            }

            sb.AppendLine(); // Add a blank line between groups for readability
        }

        return sb.ToString().TrimEnd();
    }

    string FormatMqlValue(InputValue value)
    {
        // Pattern match against the union types
        return value switch
        {
            bool b => b ? "true" : "false",
            uint u => u.ToString(CultureInfo.InvariantCulture),
            long l => l.ToString(CultureInfo.InvariantCulture),

            // Doubles must use invariant culture so they write "1.5" instead of "1,5"
            double d => d.ToString(CultureInfo.InvariantCulture),

            string s => s,

            // MetaTrader standard DateTime format is YYYY.MM.DD HH:mm:ss
            DateTime dt => dt.ToString("yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture),

            _ => throw new ArgumentOutOfRangeException(nameof(value), "Unsupported MQL input type.")
        };
    }
}
