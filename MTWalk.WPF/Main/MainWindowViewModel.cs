namespace MTWalk.WPF.Main;

partial class MainWindowViewModel(IWalker walker) : ObservableObject
{
    [ObservableProperty]
    string toggleWalkerText = "Toggle Walker";

    [RelayCommand]
    async Task ToggleWalker()
    {
        var start = new DateTime(2025, 1, 1);
        var end = new DateTime(2025, 12, 31);
        var windowingRequest = new RollingWindowingRequest
            (
            Start: DateTime.Now.AddDays(-90),
            End: DateTime.Now,
            OptimizationSize: TimeSpan.FromDays(30),
            ForwardSize: TimeSpan.FromDays(2)
            );
        var testerRequest = new TesterRequest(
                 Expert: @"Examples\MACD\MACD Sample.ex5",
                 Symbol: "EURUSD",
                 TimeFrame: TimeFrame.H1,
                 Start: start,
                 End: end,
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
        await walker.Start(windowingRequest, testerRequest);
    }
}
