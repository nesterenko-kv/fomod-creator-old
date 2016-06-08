using System.Windows;
using System.Windows.Input;

namespace FomodInfrastructure.MvvmLibrary.Behavior
{
    /// <summary>
    ///     This is an Attached Behavior and is intended for use with
    ///     XAML objects to enable binding a drag and drop event to
    ///     an ICommand.
    /// </summary>
    public static class DropBehavior
    {
        public static readonly DependencyProperty PreviewDropCommandProperty = DependencyProperty.RegisterAttached("PreviewDropCommand", typeof(ICommand), typeof(DropBehavior), new PropertyMetadata(PreviewDropCommandPropertyChangedCallBack));

        private static void PreviewDropCommandPropertyChangedCallBack(DependencyObject inDependencyObject, DependencyPropertyChangedEventArgs inEventArgs)
        {
            var uiElement = inDependencyObject as UIElement;
            if (null == uiElement)
                return;

            uiElement.Drop += (sender, args) =>
            {
                GetPreviewDropCommand(uiElement).Execute(args.Data);
                args.Handled = true;
            };
        }

        public static void SetPreviewDropCommand(this DependencyObject inUiElement, ICommand inCommand)
        {
            inUiElement.SetValue(PreviewDropCommandProperty, inCommand);
        }
        
        private static ICommand GetPreviewDropCommand(DependencyObject inUiElement)
        {
            return (ICommand)inUiElement.GetValue(PreviewDropCommandProperty);
        }
        
    }
}