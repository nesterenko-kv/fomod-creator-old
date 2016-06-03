using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System;

namespace Module.Editor.Resources.UserControls
{
    public partial class FileListUserControl
    {
        public FileListUserControl()
        {
            InitializeComponent();
        }

        #region Properties




        public Func<List<string>, List<SystemItem>> AddFileSystemItemsMethmod
        {
            get { return (Func<List<string>, List<SystemItem>>)GetValue(AddFileSystemItemsMethmodProperty); }
            set { SetValue(AddFileSystemItemsMethmodProperty, value); }
        }

        public static readonly DependencyProperty AddFileSystemItemsMethmodProperty =
            DependencyProperty.Register("AddFileSystemItemsMethmod", typeof(Func<List<string>, List<SystemItem>>), typeof(FileListUserControl), new PropertyMetadata(null));

        public Func<List<string>> AddFileMethmod
        {
            get { return (Func<List<string>>)GetValue(AddFileMethmodProperty); }
            set { SetValue(AddFileMethmodProperty, value); }
        }

        public static readonly DependencyProperty AddFileMethmodProperty =
            DependencyProperty.Register("AddFileMethmod", typeof(Func<List<string>>), typeof(FileListUserControl), new PropertyMetadata(null));

        public Func<List<string>> AddFolderMethmod
        {
            get { return (Func<List<string>>)GetValue(AddFolderMethmodProperty); }
            set { SetValue(AddFolderMethmodProperty, value); }
        }

        public static readonly DependencyProperty AddFolderMethmodProperty =
            DependencyProperty.Register("AddFolderMethmod", typeof(Func<List<string>>), typeof(FileListUserControl), new PropertyMetadata(null));





        public static readonly DependencyProperty FileListProperty = DependencyProperty.Register("FileList", typeof(FileList), typeof(FileListUserControl), new FrameworkPropertyMetadata { BindsTwoWayByDefault = true, DefaultValue = null });

        public FileList FileList
        {
            get { return (FileList)GetValue(FileListProperty); }
            set { SetValue(FileListProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(FileListUserControl), new PropertyMetadata(null));

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        #endregion

        #region Commands

        private ICommand _addFileCommandLocal, _addFolderCommandLocal, _removeItemCommand, _dropItemCommand;

        public ICommand AddFileCommandLocal
        {
            get { return _addFileCommandLocal ?? (_addFileCommandLocal = new RelayCommand<List<string>>(AddFileLocal)); }
        }

        public ICommand AddFolderCommandLocal
        {
            get { return _addFolderCommandLocal ?? (_addFolderCommandLocal = new RelayCommand<List<string>>(AddFolderLocal)); }
        }


        public ICommand RemoveItemCommand
        {
            get { return _removeItemCommand ?? (_removeItemCommand = new RelayCommand<SystemItem>(RemoveItem)); }
        }


        public ICommand DropItemCommand
        {
            get { return _dropItemCommand ?? (_dropItemCommand = new RelayCommand<object>(OnDropItem)); }
        }

        #endregion

        #region Methods

        private void AddFileLocal(List<string> path)
        {
            if (FileList == null)
                FileList = FileList.Create();
            var fileList = AddFileMethmod?.Invoke();
            if (fileList != null && AddFileSystemItemsMethmod != null)
                foreach (var item in AddFileSystemItemsMethmod.Invoke(fileList))
                    FileList.Items.Add(item);
            //AddFileCommand?.Execute(path);
            if (FileList.Items.Count == 0)
                FileList = null;
        }

        private void AddFolderLocal(List<string> path)
        {
            if (FileList == null)
                FileList = FileList.Create();
            var folderList = AddFolderMethmod?.Invoke();
            if (folderList != null && AddFileSystemItemsMethmod != null)
                foreach (var item in AddFileSystemItemsMethmod?.Invoke(folderList))
                    FileList.Items.Add(item);
            //AddFolderCommand?.Execute(path);
            if (FileList.Items.Count == 0)
                FileList = null;
        }

        private void RemoveItem(SystemItem item)
        {
            if (FileList?.Items == null)
                return;
            FileList.Items.Remove(item);
            if (FileList.Items.Count == 0)
                FileList = null;
        }

        private void OnDropItem(object param)
        {
            if (FileList == null)
                FileList = FileList.Create();

            var data = param as IDataObject;
            if (data == null || !data.GetDataPresent(DataFormats.FileDrop))
                return;
            var filePath = (string[])data.GetData(DataFormats.FileDrop);
            var fileList = new List<string>(filePath);
            var fileSystemItems = AddFileSystemItemsMethmod?.Invoke(fileList);

            if (fileSystemItems != null && AddFileSystemItemsMethmod != null)
                foreach (var item in fileSystemItems)
                    FileList.Items.Add(item);

            if (FileList.Items.Count == 0)
                FileList = null;
        }

        #endregion
    }
}