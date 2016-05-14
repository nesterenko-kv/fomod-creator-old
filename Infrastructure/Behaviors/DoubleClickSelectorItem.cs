using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace FomodInfrastructure.Behaviors
{
    /// <summary>
    ///     Class implements a <seealso cref="Selector" /> double click
    ///     to command binding attached behaviour.
    /// </summary>
    public class DoubleClickSelectorItem
    {
        #region Fields

        private static readonly DependencyProperty DoubleClickItemCommandProperty = DependencyProperty.RegisterAttached("DoubleClickItemCommand", typeof(ICommand), typeof(DoubleClickSelectorItem), new PropertyMetadata(null, OnDoubleClickItemCommand));

        #endregion Fields

        #region methods

        #region attached dependency property methods

        public static ICommand GetDoubleClickItemCommand(DependencyObject obj)
        {
            return (ICommand) obj.GetValue(DoubleClickItemCommandProperty);
        }

        public static void SetDoubleClickItemCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(DoubleClickItemCommandProperty, value);
        }

        #endregion attached dependency property methods

        private static void OnDoubleClickItemCommand(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uiElement = d as Selector;
            if (uiElement != null)
                uiElement.MouseDoubleClick -= UIElement_MouseDoubleClick; // Remove the handler if it exist to avoid memory leaks
            var command = e.NewValue as ICommand;
            if (command == null) return;
            if (uiElement != null)
                uiElement.MouseDoubleClick += UIElement_MouseDoubleClick; // the property is attached so we attach the Drop event handler
        }

        private static void UIElement_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var uiElement = sender as Selector;
            if (uiElement == null) // Sanity check just in case this was somehow send by something else
                return;
            if (uiElement.SelectedIndex == -1) // Is there a selected item that was double clicked?
                return;
            var doubleclickCommand = GetDoubleClickItemCommand(uiElement);
            if (doubleclickCommand == null) // There may not be a command bound to this after all
                return;
            if (doubleclickCommand is RoutedCommand) // Check whether this attached behaviour is bound to a RoutedCommand
                (doubleclickCommand as RoutedCommand).Execute(uiElement.SelectedItem, uiElement); // Execute the routed command
            else
                doubleclickCommand.Execute(uiElement.SelectedItem); // Execute the Command as bound delegate
        }

        #endregion methods
    }
}