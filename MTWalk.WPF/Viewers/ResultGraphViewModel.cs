using MTWalk.Reporting;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace MTWalk.WPF.Viewers;

partial class ResultGraphViewModel : ObservableRecipient, IRecipient<WalkReport>, IRecipient<WalkInitialized>
{
    readonly ResultGraphModel graph = new();
    public PlotModel PlotModel => graph.PlotModel;

    public ResultGraphViewModel()
    {
        Messenger.RegisterAll(this);
    }

    void IRecipient<WalkInitialized>.Receive(WalkInitialized message)
    {
        graph.ResetTimeAxis(message.StartDate, message.EndDate);
    }

    void IRecipient<WalkReport>.Receive(WalkReport message)
    {
        graph.AddDeals(message.Report.Deals);
    }
}

class ResultGraphModel
{
    public TwoColorLineSeries BalanceSeries { get; } = new()
    {
        Color = OxyColors.DodgerBlue,
        StrokeThickness = 2,
        Title = "Balance",
        TrackerFormatString = "{0}\nTime: {2:yyyy.MM.dd HH:mm}\nBalance: {4:0.00}"
    };
    public DateTimeAxis TimeAxis { get; } = new()
    {
        Position = AxisPosition.Bottom,
        StringFormat = "yyyy.MM.dd",
        MajorGridlineStyle = LineStyle.Dot,
        MajorGridlineColor = OxyColors.DimGray,
        TicklineColor = OxyColors.Gray
    };
    public PlotModel PlotModel { get; } = new()
    {
        Background = OxyColors.White,
        TextColor = OxyColors.Black,
        PlotAreaBorderColor = OxyColors.Gray,
    };
    public LinearAxis BalanceAxis { get; } = new()
    {
        Position = AxisPosition.Right,
        MajorGridlineStyle = LineStyle.Dot,
        MajorGridlineColor = OxyColors.DimGray,
        TicklineColor = OxyColors.Gray,

        // Optional: Add some padding so the line doesn't hit the absolute top/bottom
        MinimumPadding = 0.1,
        MaximumPadding = 0.1
    };

    public ResultGraphModel()
    {
        PlotModel.Series.Add(BalanceSeries);
        PlotModel.Axes.Add(TimeAxis);
        PlotModel.Axes.Add(BalanceAxis);
    }

    internal void AddDeals(DealRecord[] deals)
    {
        foreach (var deal in deals)
        {
            var xValue = DateTimeAxis.ToDouble(deal.Date);
            var yValue = deal.Balance;

            BalanceSeries.Points.Add(new DataPoint(xValue, yValue));
        }
        PlotModel.InvalidatePlot(true);
    }

    internal void ResetTimeAxis(DateTime startDate, DateTime endDate)
    {
        TimeAxis.Minimum = DateTimeAxis.ToDouble(startDate);
        TimeAxis.Maximum = DateTimeAxis.ToDouble(endDate);
        PlotModel.InvalidatePlot(true);
    }
}
