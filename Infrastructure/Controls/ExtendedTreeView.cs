using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace FomodInfrastructure.Controls
{
    public class MyTreeView : TreeView
    {
        public MyTreeView() : base()
        {
            this.SelectedItemChanged += new RoutedPropertyChangedEventHandler<object>(SetSelectedItem);
        }

        void SetSelectedItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (base.SelectedItem != null)
            {
                SetValue(SelectedItemProperty, base.SelectedItem);
            }
        }

        public new object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        public static new readonly DependencyProperty SelectedItemProperty = DependencyProperty.Register("SelectedItem", typeof(object), typeof(MyTreeView), new FrameworkPropertyMetadata
        {
            BindsTwoWayByDefault = true,
            //PropertyChangedCallback = new PropertyChangedCallback(CustomNestedValueChangedCallback)
        });
    }
}
