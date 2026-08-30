using OxyPlot;
using OxyPlot.Series;

namespace MTWalk.WPF.Viewers;

partial class ResultGraphViewModel : ObservableRecipient
{
    [ObservableProperty]
    public partial PlotModel PlotModel { get; set; } = new();

    public ResultGraphViewModel()
    {
        PlotModel.Series.Add(new FunctionSeries(Math.Cos, 0, 10, 0.1, "cos(x)"));
    }
}
