namespace MTWalk.Configuration;

/// <summary>
/// Represents a builder that constructs configuration ini files for terminal runs based on provided parameters.
/// </summary>
interface IConfigurationBuilder
{
    IConfigurationBuilder AddParameters(IConfigurationParameters parameters);
    string Build();
}