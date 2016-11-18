namespace FOMOD.Creator.Views
{
    using FOMOD.Creator.ViewModels;

    public partial class PluginView
    {
        public PluginView(PluginViewModel pluginViewModel)
        {
            InitializeComponent();
            DataContext = pluginViewModel;
        }
    }
}
