using System.Windows;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;

namespace Module.Editor.Resources.UserControls
{
    public partial class SetConditionFlagUserControl
    {
        public SetConditionFlagUserControl()
        {
            InitializeComponent();
        }

        #region Properties

        public static readonly DependencyProperty ConditionFlagListProperty = DependencyProperty.Register("ConditionFlagList", typeof(ConditionFlagList), typeof(SetConditionFlagUserControl), new FrameworkPropertyMetadata { DefaultValue = null, BindsTwoWayByDefault = true });

        public ConditionFlagList ConditionFlagList
        {
            get { return (ConditionFlagList)GetValue(ConditionFlagListProperty); }
            set { SetValue(ConditionFlagListProperty, value); }
        }

        #endregion

        #region Methods

        private void AddDependency()
        {
            if (ConditionFlagList == null)
                ConditionFlagList = ConditionFlagList.Create();
            ConditionFlagList.Flag.Add(SetConditionFlag.Create());
        }

        private void RemoveDependency(SetConditionFlag param)
        {
            if (ConditionFlagList?.Flag == null)
                return;
            ConditionFlagList.Flag.Remove(param);
            if (ConditionFlagList.Flag.Count == 0)
                ConditionFlagList = null;
        }

        #endregion

        #region Commands

        private ICommand _addDependencyCommand;

        public ICommand AddDependencyCommand
        {
            get { return _addDependencyCommand ?? (_addDependencyCommand = new RelayCommand(AddDependency)); }
        }

        private ICommand _removeDependencyCommand;

        public ICommand RemoveDependencyCommand
        {
            get { return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<SetConditionFlag>(RemoveDependency)); }
        }

        #endregion
    }
}