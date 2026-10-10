using System.Globalization;
using System.Windows.Data;

namespace MTWalk.WPF.Converters;

public class IntToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        => value is int intValue ? intValue.ToString(culture) : string.Empty;

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string stringValue && !string.IsNullOrWhiteSpace(stringValue))
        {
            if (int.TryParse(stringValue, NumberStyles.Integer, culture, out int result))
            {
                return result;
            }
        }

        return Binding.DoNothing;
    }
}
