using System.Windows;
using System.Windows.Controls;

namespace FomodInfrastructure.Controls
{
    public class MyTreeView : TreeView
    {
        public MyTreeView()
        {
            SelectedItemChanged += SetSelectedItem;
        }

        private void SetSelectedItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (base.SelectedItem != null)
                SetValue(SelectedItemProperty, base.SelectedItem);
        }

        public new object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public new static readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(MyTreeView), new FrameworkPropertyMetadata
        {
            BindsTwoWayByDefault = true,
            //PropertyChangedCallback = new PropertyChangedCallback(CustomNestedValueChangedCallback)
        });
    }
}
