namespace FOMOD.Creator.Views
{
    using FOMOD.Creator.ViewModels;

    public partial class RecentView
    {
        public RecentView(RecentViewModel lastProjectsViewModel)
        {
            InitializeComponent();
            DataContext = lastProjectsViewModel;
        }
    }
}
