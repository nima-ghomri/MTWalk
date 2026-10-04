using System.Globalization;
using System.Text;

namespace MTWalk.IO;

public readonly union InputValue(bool, uint, long, double, string, DateTime);
public record Input(string Name, InputValue Value);
public record InputGroup(string? Name, Input[] Children);
public record SetFormat(InputGroup[] Groups) : IFormat
{
    public static SetFormat Create(IEnumerable<Input>? inputs)
        => new(Groups: [new InputGroup(null, Children: inputs?.ToArray() ?? [])]);
}

class SetSerializer : ISerializer<SetFormat>
{
    public SetFormat Deserialize(string content)
    {
        var groups = new List<InputGroup>();
        var currentChildren = new List<Input>();
        string? currentGroupName = null;

        var lines = content.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        foreach (var line in lines)
        {
            string trimmed = line.Trim();
            if (string.IsNullOrWhiteSpace(trimmed)) continue;

            // 1. Parse Group Headers (e.g., "; --- GroupName ---")
            if (trimmed.StartsWith(";"))
            {
                if (trimmed.StartsWith("; ---") && trimmed.EndsWith("---"))
                {
                    // If we already have accumulated children or a named group, finalize it before starting the next
                    if (currentChildren.Count > 0 || currentGroupName != null)
                    {
                        groups.Add(new InputGroup(currentGroupName, currentChildren.ToArray()));
                        currentChildren.Clear();
                    }

                    currentGroupName = trimmed.Trim(';', '-', ' ').Trim();
                }
                continue;
            }

            // 2. Parse Key/Value Pairs
            var parts = trimmed.Split('=', 2);
            if (parts.Length < 2) continue;

            string key = parts[0].Trim();
            string rawValue = parts[1].Trim();

            // Extract the base value by stripping out MT5 optimization constraints (Value||Start||Step||Stop||Flag)
            string valuePart = rawValue.Split("||")[0];

            currentChildren.Add(new Input(key, new InputValue(valuePart)));
        }

        // Finalize the last remaining group
        if (currentChildren.Count > 0 || currentGroupName != null)
        {
            groups.Add(new InputGroup(currentGroupName, currentChildren.ToArray()));
        }

        return new SetFormat(groups.ToArray());
    }

    public string Serialize(SetFormat report)
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

    static string FormatMqlValue(InputValue value)
          => value switch
          {
              bool b => b ? "true" : "false",
              uint u => u.ToString(CultureInfo.InvariantCulture),
              long l => l.ToString(CultureInfo.InvariantCulture),
              double d => d.ToString(CultureInfo.InvariantCulture),
              string s => s,
              DateTime dt => dt.ToString("yyyy.MM.dd HH:mm:ss", CultureInfo.InvariantCulture),
              _ => throw new ArgumentOutOfRangeException(nameof(value), "Unsupported MQL input type.")
          };
}

