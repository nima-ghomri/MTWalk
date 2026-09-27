namespace MTWalk.MetaQuotes;

/// <summary>
/// optimization criterion: (0 — the maximum balance value, 1 — the maximum value of product of the balance and profitability, 2 — the product of the balance and expected payoff, 3 — the maximum value of the expression (100% - Drawdown)*Balance, 4 — the product of the balance and the recovery factor, 5 — the product of the balance and the Sharpe Ratio, 6 — a custom optimization criterion received from the OnTester() function in the Expert Advisor), 7 — the maximum of complex criterion.
/// </summary>
public enum OptimizationCriterion
{
    /// <summary>
    /// The highest value of the balance.
    /// </summary>    
    BalanceMax,

    /// <summary>
    /// The highest value of the profit factor.
    /// </summary>
    ProfitFactorMax,
    /// <summary>
    /// The highest value of the expected payoff.
    /// </summary>
    ExpectedPayoffMax,

    /// <summary>
    /// in this case, the relative drawdown of balance in percentage terms is taken into account.
    /// </summary>
    DrawdownMin,
    /// <summary>
    /// The highest value of the recovery factor.
    /// </summary>
    RecoveryFactorMax,

    /// <summary>
    /// The highest value of the Sharpe ratio.
    /// </summary>
    SharpeRatioMax,

    /// <summary>
    /// The optimization criterion here is the value of the OnTester() function in the Expert Advisor. This parameter allows using any custom value for the optimization of 
    /// </summary>
    CustomMax,
}