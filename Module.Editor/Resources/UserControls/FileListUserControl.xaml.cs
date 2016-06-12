using System.Collections.Generic;
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
        
        public Func<List<string>, List<SystemItem>> AddFileSystemItemsMethod
        {
            get { return (Func<List<string>, List<SystemItem>>)GetValue(AddFileSystemItemsMethodProperty); }
            set { SetValue(AddFileSystemItemsMethodProperty, value); }
        }

        public static readonly DependencyProperty AddFileSystemItemsMethodProperty =
            DependencyProperty.Register(@"AddFileSystemItemsMethod", typeof(Func<List<string>, List<SystemItem>>), typeof(FileListUserControl), new PropertyMetadata(null));

        public static readonly DependencyProperty AddFileMethodProperty =
            DependencyProperty.Register(@"AddFileMethod", typeof(Func<List<string>>), typeof(FileListUserControl), new PropertyMetadata(null));

        public Func<List<string>> AddFileMethod
        {
            get { return (Func<List<string>>)GetValue(AddFileMethodProperty); }
            set { SetValue(AddFileMethodProperty, value); }
        }

        public static readonly DependencyProperty AddFolderMethodProperty =
           DependencyProperty.Register(@"AddFolderMethod", typeof(Func<List<string>>), typeof(FileListUserControl), new PropertyMetadata(null));

        public Func<List<string>> AddFolderMethod
        {
            get { return (Func<List<string>>)GetValue(AddFolderMethodProperty); }
            set { SetValue(AddFolderMethodProperty, value); }
        }
        
        public static readonly DependencyProperty FileListProperty = DependencyProperty.Register(@"FileList", typeof(FileList), typeof(FileListUserControl), new FrameworkPropertyMetadata { BindsTwoWayByDefault = true, DefaultValue = null });

        public FileList FileList
        {
            get { return (FileList)GetValue(FileListProperty); }
            set { SetValue(FileListProperty, value); }
        }

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register(@"Header", typeof(string), typeof(FileListUserControl), new PropertyMetadata(null));

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
            get { return _dropItemCommand ?? (_dropItemCommand = new RelayCommand<IDataObject>(OnDropItem, AcceptDrop)); }
        }
        
        #endregion

        #region Methods

        private void AddFileLocal(List<string> path)
        {
            if (FileList == null)
                FileList = FileList.Create();
            var fileList = AddFileMethod?.Invoke();
            if (fileList != null && AddFileSystemItemsMethod != null)
                foreach (var item in AddFileSystemItemsMethod?.Invoke(fileList))
                    FileList.Items.Add(item);
            if (FileList.Items.Count == 0)
                FileList = null;
        }

        private void AddFolderLocal(List<string> path)
        {
            if (FileList == null)
                FileList = FileList.Create();
            var invoke = AddFolderMethod?.Invoke();
            if (invoke != null && AddFileSystemItemsMethod != null)
                foreach (var item in AddFileSystemItemsMethod?.Invoke(invoke))
                    FileList.Items.Add(item);
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

        private static bool AcceptDrop(IDataObject data)
        {
            return data != null && data.GetDataPresent(DataFormats.FileDrop);
        }

        private void OnDropItem(IDataObject data)
        {
            var filePath = (string[])data.GetData(DataFormats.FileDrop);
            var fileList = new List<string>(filePath);
            var fileSystemItems = AddFileSystemItemsMethod?.Invoke(fileList);
            if (fileSystemItems != null)
            {
                if (FileList == null)
                    FileList = FileList.Create();
                foreach (var item in fileSystemItems)
                    FileList.Items.Add(item);
            }
            if (FileList.Items.Count == 0)
                FileList = null;
        }

        #endregion
    }
}