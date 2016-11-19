namespace FOMOD.Creator.Views
{
    using FOMOD.Creator.ViewModels;

    public partial class ProjectView
    {
        public ProjectView(ProjectViewModel projectViewModel)
        {
            InitializeComponent();
            DataContext = projectViewModel;
        }
    }
}
