using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MainApplication.Selectors
{
    public class TabItemTemplateSelector : StyleSelector
    {
        public Style DefaultStyle { get; set; }

        public ObservableCollection<SelectorItem> TemplateList { get; set; } = new ObservableCollection<SelectorItem>();

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item == null)
                return DefaultStyle;

            Type findType;
            var fe = item as FrameworkElement;
            if (fe != null)
                findType = fe.DataContext.GetType();
            else
                findType = item.GetType();


            var t = TemplateList.FirstOrDefault(i => i.DataType == findType);
            if (t != null) return t.Style;

            return DefaultStyle;
        }
    }



    public class SelectorItem
    {
        public Type DataType { get; set; }
        public Style Style { get; set; }
    }
}
