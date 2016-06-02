using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace FomodInfrastructure.Converters
{
    public class ColorToSolidBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = (string)value;
            uint number;
            var color = uint.TryParse(s, NumberStyles.HexNumber, null, out number) ? Color.FromRgb((byte)(number >> 16), (byte)(number >> 8), (byte)number) : Color.FromRgb(0, 0, 0);
            var solid = new SolidColorBrush { Color = color };
            return solid;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}