using System.Text;

namespace MTWalk.Configuration;

class ConfigurationBuilder : IConfigurationBuilder
{
    readonly StringBuilder builder = new();

    public IConfigurationBuilder AddParameters(IConfigurationParameters parameters)
    {
        var configurationContent = parameters.Parse();
        builder.AppendLine(configurationContent);
        return this;
    }

    public override string ToString() => builder.ToString();
    public void Clear() => builder.Clear();
}
