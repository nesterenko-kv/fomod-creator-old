namespace FOMOD.Creator.Resources.UserControls
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Input;
    using FOMOD.Creator.Commands;
    using FOMOD.Creator.Domain.Models.ModuleCofiguration;
    using MahApps.Metro.Controls.Dialogs;
    using DataFormats = System.Windows.DataFormats;
    using IDataObject = System.Windows.IDataObject;

    public partial class FileListUserControl
    {
        public static readonly DependencyProperty FileListProperty = DependencyProperty.Register("FileList", typeof(FileList), typeof(FileListUserControl), new FrameworkPropertyMetadata
        {
            BindsTwoWayByDefault = true,
            DefaultValue = null
        });

        public static readonly DependencyProperty FolderPathProperty = DependencyProperty.Register("FolderPath", typeof(string), typeof(FileListUserControl));

        public static readonly DependencyProperty HeaderProperty = DependencyProperty.Register("Header", typeof(string), typeof(FileListUserControl));

        private ICommand _addFileCommandLocal;
        private ICommand _addFolderCommandLocal;
        private ICommand _dropItemCommand;
        private ICommand _removeItemCommand;

        public FileListUserControl()
        {
            InitializeComponent();
        }

        public ICommand AddFileCommandLocal
        {
            get
            {
                return _addFileCommandLocal ?? (_addFileCommandLocal = new RelayCommand<List<string>>(AddFileLocal));
            }
        }

        public ICommand AddFolderCommandLocal
        {
            get
            {
                return _addFolderCommandLocal ?? (_addFolderCommandLocal = new RelayCommand<List<string>>(AddFolderLocal));
            }
        }


        public ICommand DropItemCommand
        {
            get
            {
                return _dropItemCommand ?? (_dropItemCommand = new RelayCommand<IDataObject>(OnDropItem, AcceptDrop));
            }
        }

        public FileList FileList
        {
            get
            {
                return (FileList) GetValue(FileListProperty);
            }
            set
            {
                SetValue(FileListProperty, value);
            }
        }

        public string FolderPath
        {
            get
            {
                return (string) GetValue(FolderPathProperty);
            }
            set
            {
                SetValue(FolderPathProperty, value);
            }
        }

        public string Header
        {
            get
            {
                return (string) GetValue(HeaderProperty);
            }
            set
            {
                SetValue(HeaderProperty, value);
            }
        }


        public ICommand RemoveItemCommand
        {
            get
            {
                return _removeItemCommand ?? (_removeItemCommand = new RelayCommand<SystemItem>(RemoveItem));
            }
        }

        private static bool AcceptDrop(IDataObject data)
        {
            return data != null && data.GetDataPresent(DataFormats.FileDrop);
        }

        private static IEnumerable<string> AddFile()
        {
            var fileBrowserDialog = new OpenFileDialog
            {
                Multiselect = true,
                CheckFileExists = true
            };
            if (fileBrowserDialog.ShowDialog() == DialogResult.OK && fileBrowserDialog.FileNames.Any())
                return fileBrowserDialog.FileNames;
            return Enumerable.Empty<string>();
        }

        private static IEnumerable<string> AddFolder()
        {
            using (var folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK && Directory.Exists(folderBrowserDialog.SelectedPath))
                    yield return folderBrowserDialog.SelectedPath;
            }
        }

        private async void AddFileLocal(List<string> path)
        {
            var files = (await AddFileSystemItems(AddFile())).ToList();
            if (!files.Any())
                return;
            if (FileList == null)
                FileList = FileList.Create();
            FileList.Items.AddRange(files);
        }

        private async Task<IEnumerable<SystemItem>> AddFileSystemItems(IEnumerable<string> paths)
        {
            var enumerable = paths.ToList();
            var returnList = new List<SystemItem>();
            if (enumerable.All(path => path != FolderPath))
                if (enumerable.All(fileName => fileName.StartsWith(FolderPath)))
                    foreach (var path in enumerable)
                    {
                        var source = path.Substring(FolderPath.Length + 1);
                        var destination = path.Substring(FolderPath.Length + 1);
                        if (File.Exists(path))
                            returnList.Add(FileSystemItem.Create(source, destination));
                        else if (Directory.Exists(path))
                            returnList.Add(FolderSystemItem.Create(source, destination));
                    }
                else
                    await DialogCoordinator.Instance.ShowMessageAsync(this, "Error", "Allowed to add files and folders only from the project directory.");
            else
                await DialogCoordinator.Instance.ShowMessageAsync(this, "Error", "You can't add root project path.");
            return returnList;
        }

        private async void AddFolderLocal(List<string> path)
        {
            var folders = (await AddFileSystemItems(AddFolder())).ToList();
            if (!folders.Any())
                return;
            if (FileList == null)
                FileList = FileList.Create();
            FileList.Items.AddRange(folders);
        }

        private async void OnDropItem(IDataObject data)
        {
            var filePath = (string[]) data.GetData(DataFormats.FileDrop);
            if (filePath != null)
            {
                var fileList = new List<string>(filePath);
                var fileSystemItems = await AddFileSystemItems(fileList);
                if (fileSystemItems != null)
                {
                    if (FileList == null)
                        FileList = FileList.Create();
                    foreach (var item in fileSystemItems)
                        FileList.Items.Add(item);
                }
            }
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
    }
}
