using static System.Net.Mime.MediaTypeNames;

namespace MTWalk.IO;


public readonly union MqlInputValue(bool, uint, long, double, string, DateTime);
public record MqlInput(string Name, MqlInputValue Value);
public record MqlInputGroup(string Name, MqlInput[] Children);

class IniParametersParser
{
    public MqlInputGroup[] Parse(string content)
    {
        List<MqlInputGroup> groups = [];
        string groupName;
        List<MqlInput> inputs = [];
        foreach (var line in content.AsSpan().EnumerateLines())
        {
            if (line.IsWhiteSpace())
                continue;
            if (line.StartsWith(';'))
            {
                groupName = line.Trim(';').Trim().ToString();
                inputs = [];
            }
            else
            {


                //var input = new MqlInput();
                //inputs.Add(input);
            }
        }
        return [];
    }
}
