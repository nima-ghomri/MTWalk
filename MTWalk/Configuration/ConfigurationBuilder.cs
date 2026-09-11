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

    public string Build() => builder.ToString();
}
