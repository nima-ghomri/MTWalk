using System.Windows.Controls;

namespace MTWalk.WPF.ExecutionBar;

/// <summary>
/// Interaction logic for MainButton.xaml
/// </summary>
public partial class MainButton : UserControl
{
    public MainButton()
    {
        DataContext = App.Current.Services.GetRequiredService<MainButtonViewModel>();
        InitializeComponent();
    }
}
