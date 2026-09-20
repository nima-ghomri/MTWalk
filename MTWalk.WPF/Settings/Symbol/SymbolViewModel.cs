using MTWalk.WPF.Controls.Tree;

namespace MTWalk.WPF.Settings.Symbol;

record SymbolViewModel(string Name, string Description) : ITreeNode
{
    public override string ToString() => Name;
}

record SymbolCategoryViewModel(string Name, string Description, SymbolViewModel[] Children)
    : SymbolViewModel(Name, Description), ITree
{
    IEnumerable<ITreeNode> ITree.Children => Children;
    public override string ToString() => Name;
}

static class SymbolProviderExtensions
{
    public static SymbolViewModel[] ToViewModels(this Terminal.Symbol[] source) => [.. source.Select(Convert)];

    static SymbolViewModel Convert(Terminal.Symbol symbol)
        => symbol switch
        {
            SymbolCategory x => new SymbolCategoryViewModel(x.Name, x.Description, [.. x.Children.Select(Convert)]),
            Terminal.Symbol x => new SymbolViewModel(x.Name, x.Description),
        };
}