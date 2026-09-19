using MTWalk.WPF.Controls.Tree;

namespace MTWalk.WPF.Settings;

abstract class FileBaseViewModel : ITreeNode
{
    public required string Name { get; set; }
    public required string RelativePath { get; set; }
    public override string ToString() => Name;
}

class FileViewModel : FileBaseViewModel;
