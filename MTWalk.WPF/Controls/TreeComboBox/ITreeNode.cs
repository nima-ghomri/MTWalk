namespace MTWalk.WPF.Controls.TreeComboBox;

interface ITreeNode
{
    string Name { get; }
}

interface ITree<T> : ITreeNode where T : ITreeNode
{
    T[] Children { get; }
}
