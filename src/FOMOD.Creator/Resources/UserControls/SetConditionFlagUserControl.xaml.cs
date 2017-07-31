namespace FOMOD.Creator.Resources.UserControls
{
    using System.Windows;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;

    public partial class SetConditionFlagUserControl
    {
        public static readonly DependencyProperty ConditionFlagListProperty = DependencyProperty.Register("ConditionFlagList", typeof(ConditionFlagList), typeof(SetConditionFlagUserControl), new FrameworkPropertyMetadata
        {
            DefaultValue = null,
            BindsTwoWayByDefault = true
        });

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(SetConditionFlagUserControl));

        private ICommand _addDependencyCommand;

        private ICommand _removeDependencyCommand;

        public SetConditionFlagUserControl()
        {
            InitializeComponent();
        }

        public ICommand AddDependencyCommand
        {
            get
            {
                return _addDependencyCommand ?? (_addDependencyCommand = new RelayCommand(AddDependency));
            }
        }

        public ConditionFlagList ConditionFlagList
        {
            get
            {
                return (ConditionFlagList) GetValue(ConditionFlagListProperty);
            }
            set
            {
                SetValue(ConditionFlagListProperty, value);
            }
        }

        public string Header
        {
            get
            {
                return (string) GetValue(HeaderProperty);
            }
            set
            {
                SetValue(HeaderProperty, value);
            }
        }

        public ICommand RemoveDependencyCommand
        {
            get
            {
                return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<SetConditionFlag>(RemoveDependency));
            }
        }

        private void AddDependency()
        {
            if (ConditionFlagList == null)
                ConditionFlagList = ConditionFlagList.Create();
            ConditionFlagList.Flag.Add(SetConditionFlag.Create());
        }

        private void RemoveDependency(SetConditionFlag param)
        {
            if (ConditionFlagList == null)
                return;
            if (ConditionFlagList.Flag == null)
                return;
            ConditionFlagList.Flag.Remove(param);
            if (ConditionFlagList.Flag.Count == 0)
                ConditionFlagList = null;
        }
    }
}
