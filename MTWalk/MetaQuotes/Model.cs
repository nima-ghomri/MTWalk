namespace MTWalk.MetaQuotes;

/// <summary>
/// Model — tick generation mode (0 — "Every tick", 1 — "1 minute OHLC", 2 — "Open price only", 3 — "Math calculations", 4 — "Every tick based on real ticks"). If this parameter is not specified, Every Tick mode is used.
/// </summary>
public enum Model
{
    EveryTick,
    OneMinuteOHLC,
    OpenPricesOnly,
    MathCalculations,
    EveryTickBasedOnRealTicks,
}
