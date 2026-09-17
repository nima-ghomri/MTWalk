using System.Windows.Controls;

namespace MTWalk.WPF.Controls.Tree;

public class TreeNodeTemplateSelector : DataTemplateSelector
{
    public required HierarchicalDataTemplate TreeTemplate { get; set; }
    public required DataTemplate NodeTemplate { get; set; }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
        => item switch
        {
            ITree => TreeTemplate,
            ITreeNode => NodeTemplate,
            _ => base.SelectTemplate(item, container)
        };
}