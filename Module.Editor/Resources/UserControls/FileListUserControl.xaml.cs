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
                        CreateCommand.Execute(CreateCommandParameter);
                    if (FileList != null)
                        FileList.Items.Add(new FileSystemItem { Source = "new source file" });
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
                        CreateCommand?.Execute(CreateCommandParameter);
                    if (FileList != null)
                        FileList.Items.Add(new FolderSystemItem { Source = "new source folder" });
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
                            RemoveCommand?.Execute(RemoveCommandParameter);
                    }
                }));
            }
        }

        #region DependencyProperty

        public ICommand CreateCommand
        {
            get { return (ICommand)GetValue(CreateCommandProperty); }
            set { SetValue(CreateCommandProperty, value); }
        }

        public static readonly DependencyProperty CreateCommandProperty =
            DependencyProperty.Register("CreateCommand", typeof(ICommand), typeof(FileListUserControl), new PropertyMetadata(null));



        public ICommand RemoveCommand
        {
            get { return (ICommand)GetValue(RemoveCommandProperty); }
            set { SetValue(RemoveCommandProperty, value); }
        }

        public static readonly DependencyProperty RemoveCommandProperty =
            DependencyProperty.Register("RemoveCommand", typeof(ICommand), typeof(FileListUserControl), new PropertyMetadata(null));

        public object CreateCommandParameter
        {
            get { return (object)GetValue(CreateCommandParametrProperty); }
            set { SetValue(CreateCommandParametrProperty, value); }
        }

        public static readonly DependencyProperty CreateCommandParametrProperty =
            DependencyProperty.Register("CreateCommandParameter", typeof(object), typeof(FileListUserControl), new PropertyMetadata(null));



        public object RemoveCommandParameter
        {
            get { return (object)GetValue(RemoveCommandParameterProperty); }
            set { SetValue(RemoveCommandParameterProperty, value); }
        }

        public static readonly DependencyProperty RemoveCommandParameterProperty =
            DependencyProperty.Register("RemoveCommandParameter", typeof(object), typeof(FileListUserControl), new PropertyMetadata(null));



        public FileList FileList
        {
            get { return (FileList)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        public static readonly DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("FileList", typeof(FileList), typeof(FileListUserControl), new PropertyMetadata(null));




        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Header.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(string), typeof(FileListUserControl), new PropertyMetadata(null));



        #endregion




        //public static readonly DependencyProperty AddFileCommandProperty =
        //    DependencyProperty.Register("AddFileCommand", typeof(ICommand), typeof(FileListUserControl), new PropertyMetadata(null));

        //public static readonly DependencyProperty AddFileCommandParameterProperty =
        //    DependencyProperty.Register("AddFileCommandParameter", typeof(object), typeof(FileListUserControl), new PropertyMetadata(null));

        //public static readonly DependencyProperty AddFolderCommandProperty =
        //    DependencyProperty.Register("AddFolderCommand", typeof(ICommand), typeof(FileListUserControl), new PropertyMetadata(null));

        //public static readonly DependencyProperty AddFolderCommandParameterProperty =
        //    DependencyProperty.Register("AddFolderCommandParameter", typeof(object), typeof(FileListUserControl), new PropertyMetadata(null));

        //public static readonly DependencyProperty DeleteFileSystemItemCommaandProperty =
        //    DependencyProperty.Register("DeleteFileSystemItemCommaand", typeof(ICommand), typeof(FileListUserControl), new PropertyMetadata(null));

        //public static readonly DependencyProperty DeleteFileSystemItemCommaandParameterProperty =
        //    DependencyProperty.Register("DeleteFileSystemItemCommaandParameter", typeof(object), typeof(FileListUserControl), new PropertyMetadata(null));





        //public ICommand AddFileCommand
        //{
        //    get { return (ICommand)GetValue(AddFileCommandProperty); }
        //    set { SetValue(AddFileCommandProperty, value); }
        //}

        //public object AddFileCommandParameter
        //{
        //    get { return (object)GetValue(AddFileCommandParameterProperty); }
        //    set { SetValue(AddFileCommandParameterProperty, value); }
        //}
        //public ICommand AddFolderCommand
        //{
        //    get { return (ICommand)GetValue(AddFolderCommandProperty); }
        //    set { SetValue(AddFolderCommandProperty, value); }
        //}
        //public object AddFolderCommandParameter
        //{
        //    get { return (object)GetValue(AddFolderCommandParameterProperty); }
        //    set { SetValue(AddFolderCommandParameterProperty, value); }
        //}

        //public ICommand DeleteFileSystemItemCommaand
        //{
        //    get { return (ICommand)GetValue(DeleteFileSystemItemCommaandProperty); }
        //    set { SetValue(DeleteFileSystemItemCommaandProperty, value); }
        //}

        //public object DeleteFileSystemItemCommaandParameter
        //{
        //    get { return (object)GetValue(DeleteFileSystemItemCommaandParameterProperty); }
        //    set { SetValue(DeleteFileSystemItemCommaandParameterProperty, value); }
        //}
    }
}
