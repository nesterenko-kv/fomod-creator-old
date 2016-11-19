namespace FOMOD.Creator.Behaviors
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public static class DropBehavior
    {
        public static readonly DependencyProperty PreviewDropCommandProperty = DependencyProperty.RegisterAttached("PreviewDropCommand", typeof(ICommand), typeof(DropBehavior), new PropertyMetadata(PreviewDropCommandPropertyChangedCallBack));

        public static void SetPreviewDropCommand(this DependencyObject inUiElement, ICommand inCommand)
        {
            if (inUiElement == null)
                throw new ArgumentNullException(nameof(inUiElement));
            if (inCommand == null)
                throw new ArgumentNullException(nameof(inCommand));
            inUiElement.SetValue(PreviewDropCommandProperty, inCommand);
        }

        private static ICommand GetPreviewDropCommand(DependencyObject inUiElement)
        {
            return (ICommand) inUiElement.GetValue(PreviewDropCommandProperty);
        }

        private static void PreviewDropCommandPropertyChangedCallBack(DependencyObject inDependencyObject, DependencyPropertyChangedEventArgs inEventArgs)
        {
            var uiElement = inDependencyObject as UIElement;
            if (uiElement == null)
                return;
            uiElement.Drop += (sender, args) =>
            {
                GetPreviewDropCommand(uiElement).Execute(args.Data);
                args.Handled = true;
            };
        }
    }
}
