using System;
using System.Collections.Generic;
using System.Text;

namespace MTWalk.WPF.Settings;

partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty] public partial string? Expert { get; set; }
    [ObservableProperty] public partial string? Symbol { get; set; }
    [ObservableProperty] public partial TimeFrame TimeFrame { get; set; }
    [ObservableProperty] public partial DateMode DateMode { get; set; }
    [ObservableProperty] public partial DateTime FromDate { get; set; }
    [ObservableProperty] public partial DateTime ToDate { get; set; }
    [ObservableProperty] public partial ExecutionMode ExecutionMode { get; set; }
    [ObservableProperty] public partial Model Model { get; set; }
    [ObservableProperty] public partial double Deposit { get; set; }
    [ObservableProperty] public partial string Currency { get; set; }
    [ObservableProperty] public partial int Leverage { get; set; }
    [ObservableProperty] public partial Optimization Optimization { get; set; }
    [ObservableProperty] public partial OptimizationCriterion OptimizationCriterion { get; set; }

    public string[] Experts { get; set; } = [@"Examples\MACD\MACD Sample.ex5"];
    public string[] Symbols { get; set; } = ["EURUSD", "AUDUSD"];
    public TimeFrame[] TimeFrames { get; set; } = Enum.GetValues<TimeFrame>();
    public ExecutionMode[] ExecutionModes { get; set; } = Enum.GetValues<ExecutionMode>();
    public DateMode[] DateModes { get; set; } = Enum.GetValues<DateMode>();
    public Model[] Models { get; set; } = Enum.GetValues<Model>();
    public Optimization[] Optimizations { get; set; } = Enum.GetValues<Optimization>()
        .Except([Optimization.Disabled])
        .ToArray();
    public double[] Deposits { get; set; } = [3000, 5000, 10_000, 25_000, 50_000, 100_000];
    public int[] Leverages { get; set; } = [1, 10, 50, 100, 200, 500];
    public string[] Currencies { get; set; } = ["USD", "EUR", "GBP", "JPY"];
    public OptimizationCriterion[] OptimizationCriteria { get; set; } = Enum.GetValues<OptimizationCriterion>();

    public SettingsViewModel()
    {
        Expert = Experts.FirstOrDefault();
        Symbol = Symbols.FirstOrDefault();
        TimeFrame = TimeFrames.FirstOrDefault();
        Optimization = Optimizations.FirstOrDefault();
        DateMode = DateMode.CustomPeriod;
        FromDate = DateTime.Now.AddDays(-30);
        ToDate = DateTime.Now;
        ExecutionMode = ExecutionMode.ZeroDelay;
        Model = Models.FirstOrDefault();
        Deposit = Deposits.FirstOrDefault();
        Currency = Currencies.FirstOrDefault();
        Leverage = 100;
    }

    public bool TryGetRequests(out TesterRequest? testerRequest, out WindowingRequest? windowingRequest)
    {
        if (string.IsNullOrWhiteSpace(Expert)
            || string.IsNullOrWhiteSpace(Symbol)
            )
        {
            testerRequest = null;
            windowingRequest = null;
            return false;
        }
        windowingRequest = new RollingWindowingRequest
                (
                    OptimizationSize: TimeSpan.FromDays(30),
                    ForwardSize: TimeSpan.FromDays(14)
                );
        testerRequest = new TesterRequest(
                 Expert: Expert,
                 Symbol: Symbol,
                 TimeFrame: TimeFrame,
                 Start: FromDate,
                 End: ToDate,
                 ForwardMode: default,
                 ForwardDate: default,
                 ExecutionMode: ExecutionMode,
                 Model: Model,
                 Deposit: Deposit,
                 Currency: Currency,
                 Leverage: Leverage,
                 Optimization: Optimization,
                 OptimizationCriterion: OptimizationCriterion
             );
        return true;
    }
}
