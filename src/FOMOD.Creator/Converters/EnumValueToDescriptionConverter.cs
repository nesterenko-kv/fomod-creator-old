namespace FOMOD.Creator.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;

    public sealed class EnumValueToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;
            var type = value.GetType();
            if (!type.IsEnum && type != typeof(bool))
                return null;
            var field = type == typeof(bool) ? value.ToString() : type.GetField(value.ToString()).Name;
            return Localize.JsonLocalizeProvider.Default.GetLocalizedObject($"{type.Name}-{field}");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
