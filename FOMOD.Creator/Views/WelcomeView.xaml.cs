namespace FOMOD.Creator.Views
{
    using FOMOD.Creator.ViewModels;

    public partial class WelcomeView
    {
        public WelcomeView(WelcomeViewModel welcomeViewModel)
        {
            InitializeComponent();
            DataContext = welcomeViewModel;
        }
    }
}
