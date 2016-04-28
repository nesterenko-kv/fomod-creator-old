using System.Windows;

namespace Module.InfoEditor.View
{
    public partial class InfoEditorView
    {
        public InfoEditorView()
        {
            InitializeComponent();
        }

        private void ClickMe(object sender, RoutedEventArgs e)
        {
            (this.DataContext as ViewModel.InfoEditorViewModel).ModuleInformation.Author = "***ИЗМЕНЕНИЯ ВНЕ МОДЕЛИ***";
        }
    }
}
