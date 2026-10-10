using System.Globalization;
using System.Windows.Data;

namespace MTWalk.WPF.Converters;

public class DoubleToStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is double doubleValue)
        {
            return parameter is not string format || string.IsNullOrEmpty(format)
                ? doubleValue.ToString(culture)
                : doubleValue.ToString(format, culture);
        }
        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string stringValue && !string.IsNullOrWhiteSpace(stringValue))
        {
            if (double.TryParse(stringValue, NumberStyles.Any, culture, out double result))
            {
                return result;
            }
        }

        return Binding.DoNothing;
    }
}