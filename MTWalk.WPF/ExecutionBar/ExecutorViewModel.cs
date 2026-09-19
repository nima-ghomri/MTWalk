using CommunityToolkit.Mvvm.Messaging.Messages;

namespace MTWalk.WPF.ExecutionBar;

partial class ExecutorViewModel
    : ObservableRecipient, IRecipient<PropertyChangedMessage<string>>
{
    readonly IWalker walker;
    readonly SettingsViewModel settings;
    TesterRequest? testerRequest;
    WindowingRequest? windowingRequest;
    CancellationTokenSource? cancellationTokenSource;

    public ExecutorViewModel(IWalker walker, SettingsViewModel settings)
    {
        this.walker = walker;
        this.settings = settings;
        Messenger.RegisterAll(this);
    }


    bool CanStartWalker => settings.TryGetRequests(out testerRequest, out windowingRequest);

    public void Receive(PropertyChangedMessage<string> message)
    {
        StartWalkerCommand.NotifyCanExecuteChanged();
    }

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