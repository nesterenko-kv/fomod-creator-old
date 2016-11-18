namespace FOMOD.Creator.Attaches
{
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public class TreeViewHelper
    {
        public static readonly DependencyProperty SelectedItemProperty = DependencyProperty.RegisterAttached("SelectedItem", typeof(object), typeof(TreeViewHelper), new PropertyMetadata(null, OnSelectedItemChanged));

        public static object GetSelectedItem(TreeView treeView)
        {
            return treeView.GetValue(SelectedItemProperty);
        }

        public static void SetSelectedItem(TreeView treeView, object value)
        {
            treeView.SetValue(SelectedItemProperty, value);
        }

        private static void OnSelectedItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            var treeView = d as TreeView;
            if (treeView == null)
                return;
            treeView.SelectedItemChanged -= TreeViewItemChanged;
            var treeViewItem = SelectTreeViewItemForBinding(args.NewValue, treeView);
            if (treeViewItem != null)
                treeViewItem.IsSelected = true;
            treeView.SelectedItemChanged += TreeViewItemChanged;
        }

        private static TreeViewItem SelectTreeViewItemForBinding(object dataItem, ItemsControl ic)
        {
            if (ic == null || dataItem == null)
                return null;
            IItemContainerGenerator generator = ic.ItemContainerGenerator;
            using (generator.StartAt(generator.GeneratorPositionFromIndex(-1), GeneratorDirection.Forward))
            {
                foreach (var item in ic.Items)
                {
                    var tvi = generator.GenerateNext(out bool isNewlyRealized);
                    if (isNewlyRealized)
                        generator.PrepareItemContainer(tvi);
                    if (item == dataItem)
                        return tvi as TreeViewItem;
                    var temp = SelectTreeViewItemForBinding(dataItem, tvi as ItemsControl);
                    if (temp != null)
                        return temp;
                }
            }
            return null;
        }

        private static void TreeViewItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ((TreeView) sender).SetValue(SelectedItemProperty, e.NewValue);
        }
    }
}
