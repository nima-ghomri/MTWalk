using OxyPlot;

namespace MTWalk.WPF;

partial class ResultGraphViewModel : ObservableObject
{
    [ObservableProperty]
    public partial PlotModel PlotModel { get; set; } = new()
    {
        
    };
}
