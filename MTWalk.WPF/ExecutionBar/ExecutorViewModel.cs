using CommunityToolkit.Mvvm.Messaging.Messages;
using System.IO;

namespace MTWalk.WPF.ExecutionBar;

partial class ExecutorViewModel
    : ObservableRecipient, IRecipient<PropertyChangedMessage<string>>
{
    readonly IWalker walker;
    readonly SettingsViewModel settings;
    readonly InputsViewModel inputs;
    TesterRequest? testerRequest;
    WindowingRequest? windowingRequest;
    CancellationTokenSource? cancellationTokenSource;

    public ExecutorViewModel(IWalker walker, SettingsViewModel settings, InputsViewModel inputs)
    {
        this.walker = walker;
        this.settings = settings;
        this.inputs = inputs;
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
                               new(inputs.SetFilePath, inputs.SetFileContent),
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