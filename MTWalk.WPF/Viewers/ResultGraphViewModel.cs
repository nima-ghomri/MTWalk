using OxyPlot;

namespace MTWalk.WPF.Viewers;

partial class ResultGraphViewModel : ObservableObject
{
    [ObservableProperty]
    public partial PlotModel PlotModel { get; set; } = new()
    {
        
    };
}
