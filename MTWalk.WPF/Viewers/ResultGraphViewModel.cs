using MTWalk.Reporting;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace MTWalk.WPF.Viewers;

partial class ResultGraphViewModel : ObservableRecipient, IRecipient<WalkProgressReport>
{

    [ObservableProperty]
    public partial PlotModel PlotModel { get; set; } = CreateBalanceModel();

    public ResultGraphViewModel()
    {
        Messenger.RegisterAll(this);
    }

    public static PlotModel CreateBalanceModel()
    {
        var model = new PlotModel
        {
            Background = OxyColors.White,
            TextColor = OxyColors.Black,
            PlotAreaBorderColor = OxyColors.Gray,
        };

        // 1. X-Axis: Time
        var timeAxis = new DateTimeAxis
        {
            Position = AxisPosition.Bottom,
            StringFormat = "yyyy.MM.dd",
            MajorGridlineStyle = LineStyle.Dot,
            MajorGridlineColor = OxyColors.DimGray,
            TicklineColor = OxyColors.Gray
        };

        model.Axes.Add(timeAxis);

        // 2. Y-Axis: Balance (Positioned on the Right like MT5)
        var balanceAxis = new LinearAxis
        {
            Position = AxisPosition.Right,
            MajorGridlineStyle = LineStyle.Dot,
            MajorGridlineColor = OxyColors.DimGray,
            TicklineColor = OxyColors.Gray,

            // Optional: Add some padding so the line doesn't hit the absolute top/bottom
            MinimumPadding = 0.1,
            MaximumPadding = 0.1
        };
        model.Axes.Add(balanceAxis);

        // 3. Series: StepSeries creates the classic MT5 blocky staircase look
        var balanceSeries = new TwoColorLineSeries
        {
            Color = OxyColors.DodgerBlue,
            StrokeThickness = 2,
            Title = "Balance",
            TrackerFormatString = "{0}\nTime: {2:yyyy.MM.dd HH:mm}\nBalance: {4:0.00}"
        };

        model.Series.Add(balanceSeries);

        return model;
    }

    void IRecipient<WalkProgressReport>.Receive(WalkProgressReport message)
    {
        var balanceSeries = PlotModel.Series.FirstOrDefault() as TwoColorLineSeries
            ?? throw new InvalidOperationException("Balance series not found");

        // OxyPlot requires DateTimes to be converted using DateTimeAxis.ToDouble()
        foreach (var deal in message.Report.Deals)
        {
            var xValue = DateTimeAxis.ToDouble(deal.Date);
            var yValue = deal.Balance;

            balanceSeries.Points.Add(new DataPoint(xValue, yValue));
        }
        PlotModel.InvalidatePlot(true);
    }
}
