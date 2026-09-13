using MTWalk.WPF.Controls.TreeComboBox;

namespace MTWalk.WPF.Settings.Providers;

interface IExpertsSettingProvider
{
    ExpertNode[] GetExperts();
}

record ExpertNode(string Name) : ITreeNode;
record ExpertFolder(string Name, ExpertNode[] Children) : ExpertNode(Name), ITree<ExpertNode>;

class ExpertsSettingProvider : IExpertsSettingProvider
{
    public ExpertNode[] GetExperts()
    {
        return
        [
            new ExpertFolder(@"Examples\Moving Average\Moving Average.ex5", []),
            new ExpertFolder("Examples", new ExpertNode[]
            {
                new ExpertNode("Moving Average.ex5"),
                new ExpertNode("MACD.ex5"),
                new ExpertNode("RSI.ex5"),
            }),
        ];
    }
}