namespace MTWalk.Configuration;

record EventsConfigurationParameters(bool Enable) : IConfigurationParameters
{
    string IConfigurationParameters.Parse() => $"""
            [Events]
            Enable={(Enable ? 1 : 0)}
            """;
}