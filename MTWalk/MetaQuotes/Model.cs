namespace MTWalk.MetaQuotes;

/// <summary>
/// Represents the different modelling approaches that can be used in the test.
/// </summary>
public enum Model
{
    EveryTick,
    EveryTickBasedOnRealTicks,
    OneMinuteOHLC,
    OpenPricesOnly,
    MathCalculations,
}
