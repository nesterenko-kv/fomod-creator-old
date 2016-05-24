using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /*

    */



    public partial class FileListUserControl : UserControl
    {
        public FileListUserControl()
        {
            InitializeComponent();
        }

        ICommand _addFileCommand; public ICommand AddFileCommand
        {
            get
            {
                return _addFileCommand ?? (_addFileCommand = new RelayCommand(() => 
                {
                    if (FileList == null)
                        FileList = new FileList { Items = new ObservableCollection<SystemItem>()};
                    if (FileList != null)
                        FileList.Items.Add(FileSystemItem.Create()); 
                }));
            }
        }

        ICommand _addFolderCommand; public ICommand AddFolderCommand
        {
            get
            {
                return _addFolderCommand ?? (_addFolderCommand = new RelayCommand(() =>
                {
                    if (FileList == null)
                        FileList = new FileList { Items = new ObservableCollection<SystemItem>() };
                    if (FileList != null)
                        FileList.Items.Add(FolderSystemItem.Create());
                }));
            }
        }

        ICommand _removeItemCommand; public ICommand RemoveItemCommand
        {
            get
            {
                return _removeItemCommand ?? (_removeItemCommand = new RelayCommand<SystemItem>((item) =>
                {
                    if (FileList != null && FileList.Items != null)
                    {
                        FileList.Items.Remove(item);
                        if (FileList.Items.Count == 0)
                            FileList = null;
                    }
                }));
            }
        }

        #region DependencyProperty

        public FileList FileList
        {
            get { return (FileList)GetValue(FileListProperty); }
            set { SetValue(FileListProperty, value); }
        }

        public static readonly DependencyProperty FileListProperty =
            DependencyProperty.Register("FileList", typeof(FileList), typeof(FileListUserControl), new FrameworkPropertyMetadata
            {
                BindsTwoWayByDefault = true,
                DefaultValue = null
            });


        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(FileListUserControl), new PropertyMetadata(null));

        #endregion

    }
}
