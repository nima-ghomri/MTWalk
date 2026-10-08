using System.Globalization;
using System.Text.RegularExpressions;

namespace MTWalk.Serialization;

public static partial class Mq5SourceReader
{
    static readonly string[] DateFormats = ["yyyy.MM.dd HH:mm:ss", "yyyy.MM.dd HH:mm", "yyyy.MM.dd"];

    [GeneratedRegex(@"/\*.*?\*/", RegexOptions.Singleline)]
    private static partial Regex BlockComment();

    [GeneratedRegex(@"^\s*input\s+group\s+""([^""]*)""")]
    private static partial Regex GroupDecl();

    // Changed the end of the regex to capture the comment: (?://(.*))?$
    [GeneratedRegex(@"^\s*(?:sinput|input)\s+([A-Za-z_]\w*)\s+([A-Za-z_]\w*)\s*(?:=\s*(.+?))?\s*;\s*(?://(.*))?$")]
    private static partial Regex InputDecl();

    public static SetFormat ReadInputs(string mq5Path)
    {
        // ReadAllText honors UTF-8/UTF-16 BOMs, which MetaEditor files often have
        var text = BlockComment().Replace(System.IO.File.ReadAllText(mq5Path), "");

        var groups = new List<InputGroup>();
        string? groupName = null;
        var current = new List<Input>();

        void Flush()
        {
            if (current.Count > 0 || groupName is not null)
                groups.Add(new InputGroup(groupName, current.ToArray()));
            current = [];
        }

        foreach (var rawLine in text.Split('\n'))
        {
            var line = rawLine.TrimEnd('\r');

            if (GroupDecl().Match(line) is { Success: true } g)
            {
                Flush();
                groupName = g.Groups[1].Value;
                continue;
            }

            if (InputDecl().Match(line) is { Success: true } m)
            {
                var type = m.Groups[1].Value;
                var name = m.Groups[2].Value;
                var raw = m.Groups[3].Success ? m.Groups[3].Value : null;

                // Capture and trim the trailing comment (MQL5 label)
                var label = m.Groups[4].Success ? m.Groups[4].Value.Trim() : null;

                // Pass the label to your Input constructor
                current.Add(new Input(name, ParseValue(type, raw), label));
            }
        }

        Flush();
        return new SetFormat(groups.ToArray());
    }

    static InputValue ParseValue(string type, string? raw)
    {
        raw = raw?.Trim() ?? "";
        switch (type)
        {
            case "bool":
                if (bool.TryParse(raw, out var b)) return b;
                break;

            case "int":
            case "short":
            case "char":
            case "long":
                if (TryInteger(raw, out var l)) return l;
                break;

            case "uint":
            case "ushort":
            case "uchar":
            case "ulong":
                if (TryInteger(raw, out var u))
                    return u is >= 0 and <= uint.MaxValue ? (uint)u : u;
                break;

            case "double":
            case "float":
                if (double.TryParse(raw.TrimEnd('f', 'F'), NumberStyles.Float,
                        CultureInfo.InvariantCulture, out var d)) return d;
                break;

            case "datetime":
                if (TryDate(raw, out var dt)) return dt;
                break;

            case "string":
                return raw.Length >= 2 && raw[0] == '"' && raw[^1] == '"' ? raw[1..^1] : raw;
        }

        // enums (PERIOD_H1), named colors, #define constants, expressions:
        // kept as the raw text so the UI can still show them
        return raw;
    }

    static bool TryInteger(string s, out long v) =>
        s.StartsWith("0x", StringComparison.OrdinalIgnoreCase)
            ? long.TryParse(s.AsSpan(2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out v)
            : long.TryParse(s, NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out v);

    static bool TryDate(string s, out DateTime v)
    {
        var t = s.Trim();
        if (t.StartsWith("D'") && t.EndsWith('\'')) t = t[2..^1];
        return DateTime.TryParseExact(t, DateFormats, CultureInfo.InvariantCulture,
            DateTimeStyles.None, out v);
    }
}