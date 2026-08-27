namespace MTWalk.MetaQuotes;

/// <summary>
/// Represents the different modelling approaches that can be used in the test.
/// </summary>
enum Model
{
    EveryTick,
    EveryTickBasedOnRealTicks,
    OneMinuteOHLC,
    OpenPricesOnly,
    MathCalculations,
}
