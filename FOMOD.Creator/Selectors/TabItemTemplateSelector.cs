namespace FOMOD.Creator.Selectors
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    public class TabItemTemplateSelector : StyleSelector
    {
        public Style DefaultStyle { get; set; }

        // ReSharper disable once CollectionNeverUpdated.Global
        public ObservableCollection<SelectorItem> TemplateList { get; set; } = new ObservableCollection<SelectorItem>();

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item == null)
                return DefaultStyle;
            var fe = item as FrameworkElement;
            var findType = fe?.DataContext.GetType() ?? item.GetType();
            var t = TemplateList.FirstOrDefault(i => i.DataType == findType);
            return t != null
                ? t.Style
                : DefaultStyle;
        }
    }
}
