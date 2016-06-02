using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;

namespace Module.Editor.Resources.UserControls
{
    public partial class FileListUserControl
    {
        public FileListUserControl()
        {
            InitializeComponent();
        }

        #region Properties

        public static readonly DependencyProperty AddFileCommandProperty = DependencyProperty.Register("AddFileCommand", typeof(ICommand), typeof(FileListUserControl), new PropertyMetadata(null));

        public ICommand AddFileCommand
        {
            get { return (ICommand)GetValue(AddFileCommandProperty); }
            set { SetValue(AddFileCommandProperty, value); }
        }

        public static readonly DependencyProperty AddFolderCommandProperty = DependencyProperty.Register("AddFolderCommand", typeof(ICommand), typeof(FileListUserControl), new PropertyMetadata(null));

        public ICommand AddFolderCommand
        {
            get { return (ICommand)GetValue(AddFolderCommandProperty); }
            set { SetValue(AddFolderCommandProperty, value); }
        }

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

        private ICommand _addFileCommandLocal;

        public ICommand AddFileCommandLocal
        {
            get { return _addFileCommandLocal ?? (_addFileCommandLocal = new RelayCommand<List<string>>(AddFileLocal)); }
        }

        private ICommand _addFolderCommandLocal;

        public ICommand AddFolderCommandLocal
        {
            get { return _addFolderCommandLocal ?? (_addFolderCommandLocal = new RelayCommand<List<string>>(AddFolderLocal)); }
        }

        private ICommand _removeItemCommand;

        public ICommand RemoveItemCommand
        {
            get { return _removeItemCommand ?? (_removeItemCommand = new RelayCommand<SystemItem>(RemoveItem)); }
        }

        private ICommand _dropItemCommand;

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
            AddFileCommand?.Execute(path);
            if (FileList.Items.Count == 0)
                FileList = null;
        }

        private void AddFolderLocal(List<string> path)
        {
            if (FileList == null)
                FileList = FileList.Create();
            AddFolderCommand?.Execute(path);
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
            var data = param as IDataObject;
            if (data == null || !data.GetDataPresent(DataFormats.FileDrop))
                return;
            var filePath = (string[])data.GetData(DataFormats.FileDrop);
            var foldes = new List<string>();
            var files = new List<string>();
            foreach (var fp in filePath)
            {
                if (!Directory.Exists(fp))
                {
                    if (!File.Exists(fp))
                        continue;
                    files.Add(fp);
                }
                else
                    foldes.Add(fp);
            }
            if (foldes.Count > 0)
                AddFolderLocal(foldes);
            if (files.Count > 0)
                AddFileLocal(files);
        }

        #endregion
    }
}