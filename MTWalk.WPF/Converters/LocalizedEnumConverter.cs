using MTWalk.WPF.Localization;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace MTWalk.WPF.Converters;

public class LocalizedEnumConverter : TypeConverter
{
    public override object? ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
    {
        if (destinationType == typeof(string) && value is Enum enumValue)
        {
            var localized = Strings.ResourceManager.GetString(enumValue.ToString(), culture);
            return string.IsNullOrEmpty(localized) ? enumValue.ToString() : localized;
        }
        return base.ConvertTo(context, culture, value, destinationType);
    }

    static readonly TypeConverterAttribute converterAttribute = new(typeof(LocalizedEnumConverter));

    public static void AddAssembly(Assembly assembly) 
        => assembly.GetTypes()
            .Where(t => t.IsEnum)
            .ToList()
            .ForEach(AddType);

    public static void AddType(Type enumType) => TypeDescriptor.AddAttributes(enumType, converterAttribute);

    public static void AddType<TEnum>() where TEnum : Enum
        => AddType(typeof(TEnum));
}