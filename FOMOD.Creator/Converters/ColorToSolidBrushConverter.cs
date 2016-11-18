namespace FOMOD.Creator.Converters
{
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Media;

    public class ColorToSolidBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = (string) value;
            var color = uint.TryParse(s, NumberStyles.HexNumber, null, out uint number)
                ? Color.FromRgb((byte) (number >> 16), (byte) (number >> 8), (byte) number)
                : Color.FromRgb(0, 0, 0);
            var solid = new SolidColorBrush
            {
                Color = color
            };
            return solid;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
