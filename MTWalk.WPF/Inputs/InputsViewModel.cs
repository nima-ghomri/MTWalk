using Microsoft.Win32;
using System.IO;

namespace MTWalk.WPF.Inputs;

partial class InputsViewModel : ObservableObject
{
    [ObservableProperty] public partial string SetFileContent { get; set; }
    [ObservableProperty] public partial string SetFilePath { get; set; }

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
    }
}
