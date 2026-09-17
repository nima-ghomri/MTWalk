using MTWalk.WPF.Controls.Tree;
using System.Collections;

namespace MTWalk.WPF.Settings.Providers;

interface IExpertsProvider
{
    ITreeNode[] GetExperts();
}


class ExpertViewModel(string name) : ITreeNode
{
    public override string ToString()
    {
        return name;
    }
}

class DirectoryViewModel(string name, IEnumerable<ITreeNode> children) : ITree
{
    public List<ITreeNode> Children { get; } = children.ToList();

    IEnumerable<ITreeNode> ITree.Children => Children;

    public override string ToString()
    {
        return name;
    }

}

class ExpertsProvider : IExpertsProvider
{
    public ITreeNode[] GetExperts()
    {
        return
        [
            new ExpertViewModel(@"Examples\Moving Average\Moving Average.ex5"),
            new DirectoryViewModel("Examples",
            [
                new DirectoryViewModel("D",
                    [
                        new DirectoryViewModel("D2",[]),
                        new ExpertViewModel("MACD.ex5"),
                    ]),
                new ExpertViewModel("Moving Average.ex5"),
                new ExpertViewModel("MACD.ex5"),
                new ExpertViewModel("RSI.ex5"),
            ]),
            new ExpertViewModel("Moving Average.ex5"),
        ];
    }
}