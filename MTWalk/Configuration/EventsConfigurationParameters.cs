namespace MTWalk.Configuration;

/// <summary>
/// 
/// </summary>
/// <param name="Enable"></param>
record EventsConfigurationParameters(
    bool Enable) : IConfigurationParameters
{
    string IConfigurationParameters.Parse() => $"""
            [Events]
            Enable={(Enable ? 1 : 0)}
            """;
}