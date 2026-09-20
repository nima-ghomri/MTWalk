using System.Windows.Markup;

namespace MTWalk.WPF.Extensions;

public class ResolveExtension : MarkupExtension
{
    public Type? Type { get; set; }

    public override object? ProvideValue(IServiceProvider serviceProvider)
        => Type is null ? null : App.Current?.Services.GetRequiredService(Type);
}
