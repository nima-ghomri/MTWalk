using Microsoft.Win32;
using MTWalk.Serialization;
using System.Collections.ObjectModel;
using System.IO;

namespace MTWalk.WPF.Inputs;

partial class InputsViewModel(ISerializer<SetFormat> serializer) : ObservableObject
{
    [ObservableProperty] public partial string SetFileContent { get; set; }
    [ObservableProperty] public partial string SetFilePath { get; set; }
    [ObservableProperty] public partial ObservableCollection<Input> Inputs { get; set; } = [];

    [RelayCommand]
    void Load()
    {
        var openFileDialog = new OpenFileDialog
        {
            Title = "Load Strategy Inputs",
            Filter = "MT5 Set Files (*.set)|*.set|All Files (*.*)|*.*",
            DefaultExt = ".set",
            Multiselect = false,
        };

        if (openFileDialog.ShowDialog() is false)
            return;

        SetFilePath = openFileDialog.FileName;
        SetFileContent = File.ReadAllText(SetFilePath);
        var set = serializer.Deserialize(SetFileContent);
        Inputs.Clear();
        set.Groups.SelectMany(g => g.Children).ToList().ForEach(Inputs.Add);
    }
}
