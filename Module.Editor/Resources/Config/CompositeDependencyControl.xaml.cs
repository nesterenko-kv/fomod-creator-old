using System.Windows;
using System.Windows.Input;

namespace Module.Editor.Resources.Config
{
    /// <summary>
    /// Логика взаимодействия для CompositeDependencyControl.xaml
    /// </summary>
    public partial class CompositeDependencyControl
    {
        public CompositeDependencyControl()
        {
            InitializeComponent();
        }

        public ICommand AddDependencyCommand
        {
            get { return (ICommand)GetValue(AddDependencyCommandProperty); }
            set { SetValue(AddDependencyCommandProperty, value); }
        }

        public static readonly DependencyProperty AddDependencyCommandProperty =
            DependencyProperty.Register("AddDependencyCommand", typeof(ICommand), typeof(CompositeDependencyControl), new PropertyMetadata(null));

       
        public ICommand RemoveDependencyCommand
        {
            get { return (ICommand)GetValue(RemoveDependencyCommandProperty); }
            set { SetValue(RemoveDependencyCommandProperty, value); }
        }

        public static readonly DependencyProperty RemoveDependencyCommandProperty =
            DependencyProperty.Register("RemoveDependencyCommand", typeof(ICommand), typeof(CompositeDependencyControl), new PropertyMetadata(null));

        public object AddCommandParameter
        {
            get { return GetValue(AddCommandParametrProperty); }
            set { SetValue(AddCommandParametrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddCommandParametr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddCommandParametrProperty =
            DependencyProperty.Register("AddCommandParameter", typeof(object), typeof(CompositeDependencyControl), new PropertyMetadata(null));


        public object RemoveCommandParameter
        {
            get { return GetValue(RemoveCommandParameterProperty); }
            set { SetValue(RemoveCommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RemoveCommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RemoveCommandParameterProperty =
            DependencyProperty.Register("RemoveCommandParameter", typeof(object), typeof(CompositeDependencyControl), new PropertyMetadata(null));



        public object CompositeDependency
        {
            get { return GetValue(CompositeDependencyProperty); }
            set { SetValue(CompositeDependencyProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CompositeDependency.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CompositeDependencyProperty =
            DependencyProperty.Register("CompositeDependency", typeof(object), typeof(CompositeDependencyControl), new PropertyMetadata(null));
    }
}
