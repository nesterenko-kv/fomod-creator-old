namespace FOMOD.Creator.Resources.UserControls
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;

    public partial class CompositeDependencyUserControl
    {
        public static readonly DependencyProperty CompositeDependencyProperty = DependencyProperty.Register("CompositeDependency", typeof(CompositeDependency), typeof(CompositeDependencyUserControl), new FrameworkPropertyMetadata
        {
            BindsTwoWayByDefault = true,
            DefaultValue = null
        });

        private ICommand _createDependencyCommand;

        private ICommand _removeDependencyCommand;

        public CompositeDependencyUserControl()
        {
            InitializeComponent();
        }

        public CompositeDependency CompositeDependency
        {
            get
            {
                return (CompositeDependency) GetValue(CompositeDependencyProperty);
            }
            set
            {
                SetValue(CompositeDependencyProperty, value);
            }
        }

        public ICommand CreateDependencyCommand
        {
            get
            {
                return _createDependencyCommand ?? (_createDependencyCommand = new RelayCommand<CompositeDependency>(CreateDependency));
            }
        }

        public ICommand RemoveDependencyCommand
        {
            get
            {
                return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<CompositeDependency>(RemoveDependency));
            }
        }

        private void CreateDependency(CompositeDependency param)
        {
            if (param == null && CompositeDependency == null)
                CompositeDependency = CompositeDependency.Create();
            else
            {
                if (param != null)
                    param.Dependencies = CompositeDependency.Create();
                else
                    throw new ArgumentException();
            }
        }

        private void RemoveDependency(CompositeDependency param)
        {
            if (param.Parent == null && CompositeDependency != null)
                CompositeDependency = null;
            else
            {
                if (param.Parent != null)
                    param.Parent.Dependencies = null;
                else
                    throw new ArgumentException();
            }
        }
    }
}
