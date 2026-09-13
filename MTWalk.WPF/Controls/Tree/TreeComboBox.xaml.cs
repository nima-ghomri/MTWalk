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

    public static readonly DependencyProperty SelectedTextProperty =
        DependencyProperty.Register(
            nameof(SelectedText),
            typeof(string),
            typeof(TreeComboBox),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


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

    public string? SelectedText
    {
        get => (string?)GetValue(SelectedTextProperty);
        set => SetValue(SelectedTextProperty, value);
    }

    void OnItemNodeClicked(object sender, MouseButtonEventArgs e)
    {
        // Extract the underlying data context from the clicked visual element
        if (sender is not FrameworkElement element
            || element.DataContext is not ITreeNode item)
            return;
        SelectedText = item.ToString();

        // Force the dropdown to close upon selection
        MainComboBox.IsDropDownOpen = false;

        // Prevent the click from bubbling up and causing unintended visual tree behavior
        e.Handled = true;
    }
}
