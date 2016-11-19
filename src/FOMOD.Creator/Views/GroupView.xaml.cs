namespace FOMOD.Creator.Views
{
    using FOMOD.Creator.ViewModels;

    public partial class GroupView
    {
        public GroupView(GroupViewModel groupViewModel)
        {
            InitializeComponent();
            DataContext = groupViewModel;
        }
    }
}
