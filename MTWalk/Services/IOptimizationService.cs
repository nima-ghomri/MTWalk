namespace MTWalk.Services;

interface IOptimizationService
{
    OptimizationPass? GetBestPass(IEnumerable<OptimizationPass> passes, OptimizationCriterion optimizationCriterion);
}

class OptimizationService : IOptimizationService
{
    public OptimizationPass? GetBestPass(IEnumerable<OptimizationPass> passes, OptimizationCriterion optimizationCriterion)
          => passes.MaxBy(x => optimizationCriterion switch
          {
              OptimizationCriterion.BalanceMax => x.Profit,
              OptimizationCriterion.ProfitFactorMax => x.ProfitFactor,
              OptimizationCriterion.ExpectedPayoffMax => x.ExpectedPayoff,
              OptimizationCriterion.DrawdownMin => x.EquityDrawdown,
              OptimizationCriterion.RecoveryFactorMax => x.RecoveryFactor,
              OptimizationCriterion.SharpeRatioMax => x.SharpeRatio,
              OptimizationCriterion.CustomMax => x.Custom,
              OptimizationCriterion.ComplexCriterionMax => x.Result,
              _ => throw new NotImplementedException(),
          });
}
