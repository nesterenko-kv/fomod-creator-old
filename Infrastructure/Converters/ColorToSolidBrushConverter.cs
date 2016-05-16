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
            var solid = new SolidColorBrush();
            var color = value as dynamic;
            solid.Color = new Color { B = color.B, G = color.G, R = color.R , A=255};
            
            return solid; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}