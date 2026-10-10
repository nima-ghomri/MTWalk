using MTWalk.WPF.Settings.Expert;
using MTWalk.WPF.Settings.Symbol;
using System.Runtime.CompilerServices;

namespace MTWalk.WPF.Settings;

record SettingChangedMessage;

partial class SettingsViewModel(
    ISymbolsProvider symbolsProvider,
    DirectoryViewModel directoryViewModel) : ObservableRecipient
{
    [ObservableProperty, NotifyPropertyChangedRecipients] public partial string? Expert { get; set; } = ST.Default.Expert;
    [ObservableProperty] public partial string Symbol { get; set; } = ST.Default.Symbol;
    [ObservableProperty] public partial TimeFrame TimeFrame { get; set; } = (TimeFrame)ST.Default.TimeFrame;
    [ObservableProperty, NotifyPropertyChangedFor(nameof(IsFromAndToDateEnabled))] public partial DateMode DateMode { get; set; } = (DateMode)ST.Default.DateMode;
    [ObservableProperty] public partial DateTime FromDate { get; set; } = LoadDateTimeSetting(DateTime.Today.AddYears(-1));
    [ObservableProperty] public partial DateTime ToDate { get; set; } = LoadDateTimeSetting(DateTime.Today);
    [ObservableProperty] public partial ExecutionMode ExecutionMode { get; set; } = (ExecutionMode)ST.Default.ExecutionMode;
    [ObservableProperty] public partial Model Model { get; set; } = (Model)ST.Default.Model;
    [ObservableProperty] public partial double Deposit { get; set; } = ST.Default.Deposit;
    [ObservableProperty] public partial string Currency { get; set; } = ST.Default.Currency;
    [ObservableProperty] public partial int Leverage { get; set; } = ST.Default.Leverage;
    [ObservableProperty] public partial Optimization Optimization { get; set; } = (Optimization)ST.Default.Optimization;
    [ObservableProperty] public partial OptimizationCriterion OptimizationCriterion { get; set; } = (OptimizationCriterion)ST.Default.OptimizationCriterion;
    [ObservableProperty] public partial WindowType WindowType { get; set; } = (WindowType)ST.Default.WindowType;
    [ObservableProperty] public partial int OptimizationSize { get; set; } = ST.Default.OptimizationSize;
    [ObservableProperty] public partial int ForwardSize { get; set; } = ST.Default.ForwardSize;
    [ObservableProperty] public partial DateTime AnchorDate { get; set; } = LoadDateTimeSetting(DateTime.Today.AddYears(-1).AddDays(-7));
    public bool IsFromAndToDateEnabled => DateMode is DateMode.CustomPeriod;

    public FileBaseViewModel[] Experts { get; set; } = directoryViewModel.GetChildren();
    public SymbolViewModel[] Symbols { get; set; } = symbolsProvider.GetSymbols().ToViewModels();
    public TimeFrame[] TimeFrames { get; set; } = Enum.GetValues<TimeFrame>();
    public ExecutionMode[] ExecutionModes { get; set; } = Enum.GetValues<ExecutionMode>();
    public DateMode[] DateModes { get; set; } = Enum.GetValues<DateMode>();
    public Model[] Models { get; set; } = Enum.GetValues<Model>();
    public Optimization[] Optimizations { get; set; } = [Optimization.SlowCompleteAlgorithm, Optimization.FastGeneticAlgorithm, Optimization.AllMarketWatchSymbols];
    public double[] Deposits { get; set; } = ST.Default.Deposits.Cast<string>().Select(double.Parse).ToArray();
    public int[] Leverages { get; set; } = ST.Default.Leverages.Cast<string>().Select(int.Parse).ToArray();
    public string[] Currencies { get; set; } = ST.Default.Currencies.Cast<string>().ToArray();
    public OptimizationCriterion[] OptimizationCriteria { get; set; } = Enum.GetValues<OptimizationCriterion>();
    public WindowType[] WindowTypes { get; set; } = Enum.GetValues<WindowType>();
    public int[] OptimizationSizes { get; set; } = ST.Default.OptimizationSizes.Cast<string>().Select(int.Parse).ToArray();
    public int[] ForwardSizes { get; set; } = ST.Default.ForwardSizes.Cast<string>().Select(int.Parse).ToArray();

    partial void OnExpertChanged(string? value) => SaveSetting(value, nameof(Expert));
    partial void OnSymbolChanged(string value) => SaveSetting(value, nameof(Symbol));
    partial void OnTimeFrameChanged(TimeFrame value) => SaveSetting((int)value, nameof(TimeFrame));
    partial void OnFromDateChanged(DateTime value) => SaveSetting(value, nameof(FromDate));
    partial void OnToDateChanged(DateTime value) => SaveSetting(value, nameof(ToDate));
    partial void OnExecutionModeChanged(ExecutionMode value) => SaveSetting((int)value, nameof(ExecutionMode));
    partial void OnModelChanged(Model value) => SaveSetting((int)value, nameof(Model));
    partial void OnDepositChanged(double value) => SaveSetting(value, nameof(Deposit));
    partial void OnCurrencyChanged(string value) => SaveSetting(value, nameof(Currency));
    partial void OnLeverageChanged(int value) => SaveSetting(value, nameof(Leverage));
    partial void OnOptimizationChanged(Optimization value) => SaveSetting((int)value, nameof(Optimization));
    partial void OnOptimizationCriterionChanged(OptimizationCriterion value) => SaveSetting((int)value, nameof(OptimizationCriterion));
    partial void OnWindowTypeChanged(WindowType value) => SaveSetting((int)value, nameof(WindowType));
    partial void OnOptimizationSizeChanged(int value) => SaveSetting(value, nameof(OptimizationSize));
    partial void OnForwardSizeChanged(int value) => SaveSetting(value, nameof(ForwardSize));
    partial void OnAnchorDateChanged(DateTime value) => SaveSetting(value, nameof(AnchorDate));

    partial void OnDateModeChanged(DateMode value)
    {
        switch (value)
        {
            case DateMode.EntireHistory:
                FromDate = new(1970, 1, 1);
                ToDate = DateTime.Today;
                break;
            case DateMode.LastMonth:
                FromDate = DateTime.Today.AddMonths(-1);
                ToDate = DateTime.Today;
                break;
            case DateMode.LastYear:
                FromDate = DateTime.Today.AddYears(-1);
                ToDate = DateTime.Today;
                break;
        }
        SaveSetting((int)value, nameof(DateMode));
    }



    static void SaveSetting(object? value, string propertyName)
    {
        ST.Default[propertyName] = value;
        ST.Default.Save();
    }

    static DateTime LoadDateTimeSetting(DateTime defaultValue, [CallerMemberName] string? propertyName = default)
    {
        if (ST.Default[propertyName] is DateTime storedValue && storedValue != default)
        {
            return storedValue;
        }
        ST.Default[propertyName] = defaultValue;
        ST.Default.Save();
        return defaultValue;
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