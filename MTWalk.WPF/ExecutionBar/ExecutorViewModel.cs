using System.Diagnostics.CodeAnalysis;

namespace MTWalk.WPF.ExecutionBar;

partial class ExecutorViewModel(IWalker walker, SettingsViewModel settings) 
    : ObservableRecipient
{
    TesterRequest? testerRequest;
    WindowingRequest? windowingRequest;
    CancellationTokenSource? cancellationTokenSource;


    bool CanStartWalker => settings.TryGetRequests(out testerRequest, out windowingRequest);

    [RelayCommand(CanExecute = nameof(CanStartWalker))]
    async Task StartWalker()
    {
        cancellationTokenSource = new();
        Messenger.Send(new WalkInitialized(testerRequest!.Start, testerRequest!.End));

        try
        {
            Progress<WalkReport> progress = new(report => Messenger.Send(report));
            await walker.Start(windowingRequest!,
                               testerRequest!,
                               progress,
                               cancellationTokenSource.Token);
        }
        catch (TaskCanceledException)
        {
            // Should close terminal
        }
    }

    [RelayCommand]
    void StopWalker() => cancellationTokenSource?.Cancel();
}

record WalkInitialized(DateTime StartDate, DateTime EndDate);