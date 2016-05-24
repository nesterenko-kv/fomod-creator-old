using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Module.Editor.Resources.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CompositeDependencyUserControl.xaml
    /// </summary>
    public partial class CompositeDependencyUserControl : UserControl
    {
        public CompositeDependencyUserControl()
        {
            InitializeComponent();
        }



        public CompositeDependency CompositeDependency  
        {
            get { return (CompositeDependency)GetValue(CompositeDependencyProperty); }
            set { SetValue(CompositeDependencyProperty, value); }
        }

        public static readonly DependencyProperty CompositeDependencyProperty =
            DependencyProperty.Register("CompositeDependency", typeof(CompositeDependency), typeof(CompositeDependencyUserControl), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
                DefaultValue = null
            });


        ICommand _createDependencyCommand; public ICommand CreateDependencyCommand
        {
            get
            {
                return _createDependencyCommand ?? (_createDependencyCommand = new RelayCommand<CompositeDependency>(param =>
                {
                    if (param == null && CompositeDependency == null)
                        CompositeDependency = CompositeDependency.Create();
                    else if (param != null)
                        param.Dependencies = CompositeDependency.Create();
                    else
                        throw new ArgumentException();
                }));
            }
        }

        ICommand _removeDependencyCommand; public ICommand RemoveDependencyCommand
        {
            get
            {
                return _removeDependencyCommand ?? (_removeDependencyCommand = new RelayCommand<CompositeDependency>(param =>
                {
                    if (param.Parent == null && CompositeDependency != null)
                        CompositeDependency = null;
                    else if (param.Parent != null)
                        param.Parent.Dependencies = null;
                    else
                        throw new ArgumentException();
                }));
            }
        }

    }
}
