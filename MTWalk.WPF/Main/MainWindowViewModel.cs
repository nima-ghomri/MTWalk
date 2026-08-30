namespace MTWalk.WPF.Main;

partial class MainWindowViewModel : ObservableObject
{
    public string[] TabItems { get; set; } = ["1", "2"];
    public string Text { get; set; } = "My Text";
}