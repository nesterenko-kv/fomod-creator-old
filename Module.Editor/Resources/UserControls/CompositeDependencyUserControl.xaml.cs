using System;
using System.Windows;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;

namespace Module.Editor.Resources.UserControls
{
    public partial class CompositeDependencyUserControl
    {
        public CompositeDependencyUserControl()
        {
            InitializeComponent();
        }

        #region Properties

        public static readonly DependencyProperty CompositeDependencyProperty = DependencyProperty.Register("CompositeDependency", typeof(CompositeDependency), typeof(CompositeDependencyUserControl), new FrameworkPropertyMetadata { BindsTwoWayByDefault = true, DefaultValue = null });

        public CompositeDependency CompositeDependency
        {
            get { return (CompositeDependency)GetValue(CompositeDependencyProperty); }
            set { SetValue(CompositeDependencyProperty, value); }
        }

        #endregion

        #region Methods

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

        #endregion

        #region Commands

        private ICommand _createDependencyCommand;

        public ICommand CreateDependencyCommand
        {
            get { return _createDependencyCommand ?? (_createDependencyCommand = new RelayCommand<CompositeDependency>(CreateDependency)); }
        }

        private ICommand _removeDependencyCommand;

        public ICommand RemoveDependencyCommand
        {
            get { return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<CompositeDependency>(RemoveDependency)); }
        }

        #endregion
    }
}