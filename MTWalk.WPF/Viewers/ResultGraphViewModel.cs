using MTWalk.Reporting;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using System.ComponentModel;

namespace MTWalk.WPF.Viewers;

partial class ResultGraphViewModel : ObservableRecipient, IRecipient<WalkProgressReport>
{
    private static TwoColorLineSeries balanceSeries;

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
            Background = OxyColor.Parse("#151515"),
            TextColor = OxyColors.White,
            PlotAreaBorderColor = OxyColors.Gray
        };

        // 1. X-Axis: Time
        var timeAxis = new DateTimeAxis
        {
            Position = AxisPosition.Bottom,
            StringFormat = "yyyy.MM.dd HH:mm",
            MajorGridlineStyle = LineStyle.Dot,
            MajorGridlineColor = OxyColor.Parse("#333333"),
            TicklineColor = OxyColors.Gray
        };

        model.Axes.Add(timeAxis);

        // 2. Y-Axis: Balance (Positioned on the Right like MT5)
        var balanceAxis = new LinearAxis
        {
            Position = AxisPosition.Right,
            MajorGridlineStyle = LineStyle.Dot,
            MajorGridlineColor = OxyColor.Parse("#333333"),
            TicklineColor = OxyColors.Gray,

            // Optional: Add some padding so the line doesn't hit the absolute top/bottom
            MinimumPadding = 0.1,
            MaximumPadding = 0.1
        };
        model.Axes.Add(balanceAxis);

        // 3. Series: StepSeries creates the classic MT5 blocky staircase look
        balanceSeries = new TwoColorLineSeries
        {
            Color = OxyColor.Parse("#007ACC"), // Dodger Blue
            StrokeThickness = 2,
            Title = "Balance",
            TrackerFormatString = "{0}\nTime: {2:yyyy.MM.dd HH:mm}\nBalance: {4:0.00}"
        };


        return model;
    }

    public void Receive(WalkProgressReport message)
    {
        var p = CreateBalanceModel();
        IEnumerable<DealRecord> dealHistory = message.Report.Deals;
        // 4. Map the data points
        // OxyPlot requires DateTimes to be converted using DateTimeAxis.ToDouble()
        foreach (var deal in dealHistory)
        {
            double xValue = DateTimeAxis.ToDouble(deal.Date);
            double yValue = new Random().NextDouble() * 1000;

            balanceSeries.Points.Add(new DataPoint(xValue, yValue));
        }
        p.Series.Add(balanceSeries);


        PlotModel = p;
    }
}
