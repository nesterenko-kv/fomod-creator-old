using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace FomodInfrastructure.Converters
{
    public class ItemControlIndexConverter : IValueConverter
    {
        public static T GetParent<T>(DependencyObject dependencyObject) where T : FrameworkElement
        {
            var temp = dependencyObject;
            while (true)
            {
                var parent = VisualTreeHelper.GetParent(temp);
                if (parent == null)
                    return null;
                var pattern = parent as T;
                if (pattern != null)
                    return pattern;
                temp = parent;
            }
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var item = value as ListBoxItem;
            if (item == null)
                return DependencyProperty.UnsetValue;
            var list = GetParent<ItemsControl>(item);
            if (list == null)
                return DependencyProperty.UnsetValue;
            return list.ItemContainerGenerator.IndexFromContainer(item) + 1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
}
