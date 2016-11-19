namespace FOMOD.Creator.Views
{
    using FOMOD.Creator.ViewModels;

    public partial class ShellView
    {
        public ShellView(ShellViewModel shellViewModel)
        {
            InitializeComponent();
            DataContext = shellViewModel;
        }
    }
}
