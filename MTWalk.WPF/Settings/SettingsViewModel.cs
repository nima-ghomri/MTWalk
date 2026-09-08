using System;
using System.Collections.Generic;
using System.Text;

namespace MTWalk.WPF.Settings;

partial class SettingsViewModel : ObservableObject
{
    [ObservableProperty]
    public partial string Symbol { get; set; }
}
