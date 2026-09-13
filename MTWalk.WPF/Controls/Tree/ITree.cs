namespace MTWalk.WPF.Controls.Tree;

public interface ITreeNode;

public interface ITree : ITreeNode
{
    public IEnumerable<ITreeNode> Children { get; }
}