namespace MTWalk.WPF.ExecutionBar;

partial class ExecutorViewModel(IWalker walker, InputsViewModel inputs, SettingsViewModel settings) : ObservableRecipient
{
    CancellationTokenSource? cancellationTokenSource;

    [ObservableProperty]
    public partial WalkerState WalkerState { get; set; }

    [RelayCommand]
    async Task StartWalker()
    {
        cancellationTokenSource = new();
        var windowingRequest = new RollingWindowingRequest
            (
                OptimizationSize: TimeSpan.FromDays(30),
                ForwardSize: TimeSpan.FromDays(14)
            );
        var testerRequest = new TesterRequest(
                 Expert: @"Examples\MACD\MACD Sample.ex5",
                 Symbol: settings.Symbol,
                 TimeFrame: TimeFrame.H1,
                 Start: new DateTime(2025, 1, 1),
                 End: new DateTime(2025, 3, 1),
                 ForwardMode: ForwardMode.Custom,
                 ForwardDate: default,
                 ExecutionMode: ExecutionMode.ZeroDelay,
                 Model: Model.EveryTick,
                 Deposit: 1000.0,
                 Currency: "USD",
                 Leverage: 100,
                 Optimization: Optimization.FastGeneticAlgorithm,
                 OptimizationCriterion: OptimizationCriterion.BalanceMax
             );
        try
        {
            Progress<WalkProgressReport> progress = new(report => Messenger.Send(report));
            await walker.Start(windowingRequest,
                               testerRequest,
                               progress,
                               cancellationTokenSource.Token);
        }
        catch (TaskCanceledException)
        {
            // Should close terminal
        }
    }

    [RelayCommand]
    void StopWalker()
    {
        cancellationTokenSource?.Cancel();
    }
}

enum WalkerState
{
    Completed,
    Running,
}
