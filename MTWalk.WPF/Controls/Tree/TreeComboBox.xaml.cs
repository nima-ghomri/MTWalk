using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace MTWalk.WPF.Controls.Tree;

/// <summary>
/// Interaction logic for TreeComboBox.xaml
/// </summary>
public partial class TreeComboBox : UserControl
{
    public TreeComboBox() => InitializeComponent();


    public static readonly DependencyProperty TreeColorProperty =
        DependencyProperty.Register(
            nameof(TreeColor),
            typeof(Brush),
            typeof(TreeComboBox),
            new PropertyMetadata(Brushes.Black));

    public static readonly DependencyProperty NodeColorProperty =
        DependencyProperty.Register(
            nameof(NodeColor),
            typeof(Brush),
            typeof(TreeComboBox),
            new PropertyMetadata(Brushes.Black));

    public static readonly DependencyProperty NodeIconProperty =
        DependencyProperty.Register(
            nameof(NodeIcon),
            typeof(string),
            typeof(TreeComboBox),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty TreeIconProperty =
        DependencyProperty.Register(
            nameof(TreeIcon),
            typeof(string),
            typeof(TreeComboBox),
            new PropertyMetadata(string.Empty));

    public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register(
            nameof(ItemsSource),
            typeof(IEnumerable<ITreeNode>),
            typeof(TreeComboBox));

    public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register(
            nameof(Text),
            typeof(string),
            typeof(TreeComboBox),
            new FrameworkPropertyMetadata(default, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    public static readonly DependencyProperty DisplayMemberPathProperty =
        DependencyProperty.Register(nameof(DisplayMemberPath),
            typeof(string),
            typeof(TreeComboBox),
            new PropertyMetadata(default));


    public Brush TreeColor
    {
        get => (Brush)GetValue(TreeColorProperty);
        set => SetValue(TreeColorProperty, value);
    }

    public Brush NodeColor
    {
        get => (Brush)GetValue(NodeColorProperty);
        set => SetValue(NodeColorProperty, value);
    }

    public string NodeIcon
    {
        get => (string)GetValue(NodeIconProperty);
        set => SetValue(NodeIconProperty, value);
    }

    public string TreeIcon
    {
        get => (string)GetValue(TreeIconProperty);
        set => SetValue(TreeIconProperty, value);
    }

    public IEnumerable<ITreeNode> ItemsSource
    {
        get => (IEnumerable<ITreeNode>)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public string? Text
    {
        get => (string?)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string? DisplayMemberPath
    {
        get { return (string?)GetValue(DisplayMemberPathProperty); }
        set { SetValue(DisplayMemberPathProperty, value); }
    }

    void TreeViewItem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        if (sender is TreeViewItem { DataContext: ITreeNode item } element)
        {
            // If this item represents a tree/directory, treat click as expand/collapse.
            if (item is ITree)
            {
                if (FindAncestor<TreeViewItem>(element) is TreeViewItem tvi)
                {
                    tvi.IsExpanded ^= true; // Toggle expansion state
                }
                e.Handled = true;
                return;
                //Swallow the event so the ComboBox doesn't close
            }

            // Leaf node: finalize selection and close dropdown
            Text = DisplayMemberPath is null ? item.ToString()
                : item.GetType().GetProperty(DisplayMemberPath)?.GetValue(item)?.ToString();
            MainComboBox.IsDropDownOpen = false;
            // Prevent the click from bubbling up and causing unintended visual tree behavior
            e.Handled = true;
        }
    }

    void TreeViewItem_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is TreeViewItem { DataContext: ITree } item)
            e.Handled = true;
    }

    static T? FindAncestor<T>(DependencyObject? current) where T : DependencyObject
    {
        while (current is not null)
        {
            if (current is T match)
                return match;
            current = VisualTreeHelper.GetParent(current);
        }
        return null;
    }
}
