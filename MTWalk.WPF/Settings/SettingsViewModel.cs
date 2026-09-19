namespace MTWalk.WPF.Settings;

partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty] public partial string Expert { get; set; }
    [ObservableProperty] public partial string Symbol { get; set; }
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
    [ObservableProperty] public partial WindowType WindowType { get; set; }
    [ObservableProperty] public partial int OptimizationSize { get; set; }
    [ObservableProperty] public partial int ForwardSize { get; set; }
    [ObservableProperty] public partial DateTime AnchorDate { get; set; } = new(2025, 10, 2);

    public FileBaseViewModel[] Experts { get; set; }
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
    public WindowType[] WindowTypes { get; set; } = Enum.GetValues<WindowType>();
    public int[] OptimizationSizes { get; set; } = [7, 14, 30, 60, 90];
    public int[] ForwardSizes { get; set; } = [3, 7, 14, 30, 60, 90];
    public SettingsViewModel(IExpertProvider expertProvider, IServiceProvider serviceProvider)
    {
        var root = serviceProvider.GetRequiredService<DirectoryViewModel>();
        Experts = root.GetChildren();
        Expert = Experts.FirstOrDefault()?.ToString() ?? string.Empty;
        Symbol = Symbols.FirstOrDefault() ?? string.Empty;
        TimeFrame = TimeFrames.FirstOrDefault();
        Optimization = Optimizations.FirstOrDefault();
        DateMode = DateMode.CustomPeriod;
        FromDate = DateTime.Now.AddMonths(-2);
        ToDate = DateTime.Now;
        ExecutionMode = ExecutionMode.ZeroDelay;
        Model = Model.OneMinuteOHLC;
        Deposit = Deposits.FirstOrDefault();
        Currency = Currencies.FirstOrDefault() ?? string.Empty;
        Leverage = 100;
        OptimizationCriterion = OptimizationCriteria.FirstOrDefault();
        ForwardSize = ForwardSizes.FirstOrDefault();
        OptimizationSize = OptimizationSizes.FirstOrDefault();
    }

    public bool TryGetRequests([NotNullWhen(true)] out TesterRequest? testerRequest,
       [NotNullWhen(true)] out WindowingRequest? windowingRequest)
    {
        if (string.IsNullOrWhiteSpace(Expert)
            || string.IsNullOrWhiteSpace(Symbol)
            )
        {
            testerRequest = null;
            windowingRequest = null;
            return false;
        }
        windowingRequest = WindowType switch
        {
            WindowType.Rolling => new RollingWindowingRequest
                (
                    OptimizationSize: TimeSpan.FromDays(OptimizationSize),
                    ForwardSize: TimeSpan.FromDays(ForwardSize)
                ),
            WindowType.Anchored => new AnchoredWindowingRequest
                (
                    AnchorDate: AnchorDate,
                    ForwardSize: TimeSpan.FromDays(ForwardSize)
                ),
            _ => throw new NotImplementedException()
        };
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