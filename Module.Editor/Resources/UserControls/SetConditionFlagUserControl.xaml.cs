using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Module.Editor.Resources.UserControls
{
    public partial class SetConditionFlagUserControl
    {
        public SetConditionFlagUserControl()
        {
            InitializeComponent();
        }

        public ConditionFlagList ConditionFlagList
        {
            get { return (ConditionFlagList)GetValue(ConditionFlagListProperty); }
            set { SetValue(ConditionFlagListProperty, value); }
        }

        public static readonly DependencyProperty ConditionFlagListProperty =
            DependencyProperty.Register("ConditionFlagList", typeof(ConditionFlagList), typeof(SetConditionFlagUserControl), new FrameworkPropertyMetadata
            {
                DefaultValue = null,
                BindsTwoWayByDefault = true
            });


        private ICommand _addDependencyCommand;
        public ICommand AddDependencyCommand
        {
            get
            {
                return _addDependencyCommand ?? (_addDependencyCommand = new RelayCommand(() =>
                {
                    if (ConditionFlagList==null)
                        ConditionFlagList = new ConditionFlagList { Flag = new ObservableCollection<SetConditionFlag>()};
                    ConditionFlagList.Flag.Add(SetConditionFlag.Create());
                }));
            }
        }

        private ICommand _removeDependencyCommand;
        public ICommand RemoveDependencyCommand
        {
            get
            {
                return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<SetConditionFlag>(param =>
                {
                    if (ConditionFlagList == null || ConditionFlagList.Flag == null) return;
                    ConditionFlagList.Flag.Remove(param);
                    if (ConditionFlagList.Flag.Count == 0)
                        ConditionFlagList = null;
                }));
            }
        }
    }
}
