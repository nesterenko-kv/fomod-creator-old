namespace FOMOD.Creator.Views
{
    using FOMOD.Creator.ViewModels;

    public partial class InstallStepView
    {
        public InstallStepView(InstallStepViewModel installStepViewModel)
        {
            InitializeComponent();
            DataContext = installStepViewModel;
        }
    }
}
