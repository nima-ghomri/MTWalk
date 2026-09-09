namespace MTWalk.WPF.ExecutionBar;

partial class ExecutorViewModel(IWalker walker, InputsViewModel inputs, SettingsViewModel settings) : ObservableRecipient
{
    TesterRequest? testerRequest;
    WindowingRequest? windowingRequest;
    CancellationTokenSource? cancellationTokenSource;

    [ObservableProperty]
    public partial WalkerState WalkerState { get; set; }

    bool CanStartWalker => settings.TryGetRequests(out testerRequest, out windowingRequest);

    [RelayCommand(CanExecute = nameof(CanStartWalker))]
    async Task StartWalker()
    {
        cancellationTokenSource = new();
        
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
