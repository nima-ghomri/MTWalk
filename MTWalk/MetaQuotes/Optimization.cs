namespace MTWalk.MetaQuotes;

/// <summary>
/// enable/disable optimization, its type (0 — optimization disabled, 1 — "Slow complete algorithm", 2 — "Fast genetic based algorithm", 3 — "All symbols selected in Market Watch").
/// </summary>
public enum Optimization
{
    Disabled,
    SlowCompleteAlgorithm,
    FastGeneticAlgorithm,
    AllMarketWatchSymbols,
}
