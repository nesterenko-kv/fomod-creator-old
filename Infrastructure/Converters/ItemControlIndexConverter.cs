using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace FomodInfrastructure.Converters
{
    public class ItemControlIndexConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var lbi = (ListBoxItem)value;
            var listBox = lbi.GetPatent<ItemsControl>();
            if (listBox == null) return null;
            var index = listBox.ItemContainerGenerator.IndexFromContainer(lbi);
            return index + 1;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) { throw new NotImplementedException(); }
    }


    public static class STATIC
    {
        public static T GetPatent<T>(this DependencyObject THIS) where T : FrameworkElement
        {
            var parent = VisualTreeHelper.GetParent(THIS);
            if (parent == null) return null;
            if (parent is T)
                return (T)parent;
            else
                return parent.GetPatent<T>();
        }
    }
}
