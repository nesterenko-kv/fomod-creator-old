using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Module.Editor.Resources.UserControls
{
    public partial class FileListUserControl
    {
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

        public FileListUserControl()
        {
            InitializeComponent();
        }

        private ICommand _addFileCommand;
        public ICommand AddFileCommand
        {
            get
            {
                return _addFileCommand ?? (_addFileCommand = new RelayCommand(() => 
                {
                    if (FileList == null)
                        FileList = new FileList
                        {
                            Items = new ObservableCollection<SystemItem>()
                        };
                    FileList.Items.Add(FileSystemItem.Create()); 
                }));
            }
        }

        private ICommand _addFolderCommand;
        public ICommand AddFolderCommand
        {
            get
            {
                return _addFolderCommand ?? (_addFolderCommand = new RelayCommand(() =>
                {
                    if (FileList == null)
                        FileList = new FileList
                        {
                            Items = new ObservableCollection<SystemItem>()
                        };
                    FileList.Items.Add(FolderSystemItem.Create());
                }));
            }
        }

        private ICommand _removeItemCommand;
        public ICommand RemoveItemCommand
        {
            get
            {
                return _removeItemCommand ?? (_removeItemCommand = new RelayCommand<SystemItem>(item =>
                {
                    if (FileList?.Items == null) return;
                    FileList.Items.Remove(item);
                    if (FileList.Items.Count == 0)
                        FileList = null;
                }));
            }
        }
    }
}
