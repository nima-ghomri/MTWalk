using System.Text;

namespace MTWalk.Configuration;

class ConfigurationBuilder 
{
    readonly StringBuilder builder = new();

    public ConfigurationBuilder AddParameters(IConfigurationParameters parameters)
    {
        var configurationContent = parameters.Parse();
        builder.AppendLine(configurationContent);
        return this;
    }

    public override string ToString() => builder.ToString();
    public void Clear() => builder.Clear();
}
