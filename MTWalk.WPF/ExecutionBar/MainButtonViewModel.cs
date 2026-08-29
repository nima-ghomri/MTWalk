namespace MTWalk.WPF.ExecutionBar;
partial class MainButtonViewModel(IWalker walker) : ObservableObject
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
                ForwardSize: TimeSpan.FromDays(2)
            );
        var testerRequest = new TesterRequest(
                 Expert: @"Examples\MACD\MACD Sample.ex5",
                 Symbol: "EURUSD",
                 TimeFrame: TimeFrame.H1,
                 Start: new DateTime(2025, 1, 1),
                 End: new DateTime(2025, 2, 1),
                 ForwardMode: ForwardMode.Custom,
                 ForwardDate: default,
                 ExecutionMode: ExecutionMode.ZeroDelay,
                 Model: Model.OneMinuteOHLC,
                 Deposit: 1000.0,
                 Currency: "USD",
                 Leverage: 100,
                 Optimization: Optimization.FastGeneticAlgorithm,
                 OptimizationCriterion: OptimizationCriterion.BalanceMax
             );
        try
        {
            await walker.Start(windowingRequest, testerRequest, cancellationTokenSource.Token);
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
