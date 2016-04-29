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

namespace Module.Editor.View
{
    /// <summary>
    /// Логика взаимодействия для MainEditor.xaml
    /// </summary>
    public partial class MainEditorView : UserControl
    {
        public MainEditorView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var data = (this.tw.DataContext as XmlDataProvider);
            var node = data.Document.ChildNodes[0].ChildNodes[0].ChildNodes[0];
            node.InnerText += "AAAAA";


            System.Windows.MessageBox.Show(node.InnerText);
        }
    }
}
