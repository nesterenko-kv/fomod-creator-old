namespace FOMOD.Creator.Views
{
    using FOMOD.Creator.ViewModels;

    public partial class EditorView
    {
        public EditorView(EditorViewModel editorViewModel)
        {
            DataContext = ViewModel = editorViewModel;
            InitializeComponent();
        }

        public EditorViewModel ViewModel { get; }
    }
}
