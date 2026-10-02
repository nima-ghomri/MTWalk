using System.Numerics;

namespace MTWalk.Reporting;

public class OptimizationPass
{
    public int Pass { get; set; }
    public double Result { get; set; }
    public double Profit { get; set; }
    public double ExpectedPayoff { get; set; }
    public double ProfitFactor { get; set; }
    public double RecoveryFactor { get; set; }
    public double SharpeRatio { get; set; }
    public double Custom { get; set; }
    public double EquityDrawdown { get; set; }
    public int Trades { get; set; }

    public List<Input> Inputs { get; set; } = [];
}
