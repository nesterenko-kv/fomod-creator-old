using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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




        public ICommand AddFileCommand
        {
            get { return (ICommand)GetValue(AddFileCommandProperty); }
            set { SetValue(AddFileCommandProperty, value); }
        }

        public static readonly DependencyProperty AddFileCommandProperty =
            DependencyProperty.Register("AddFileCommand", typeof(ICommand), typeof(FileListUserControl), new PropertyMetadata(null));




        public ICommand AddFolderCommand
        {
            get { return (ICommand)GetValue(AddFolderCommandProperty); }
            set { SetValue(AddFolderCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for AddFolderCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AddFolderCommandProperty =
            DependencyProperty.Register("AddFolderCommand", typeof(ICommand), typeof(FileListUserControl), new PropertyMetadata(null));




        private ICommand _addFileCommandLocal;
        private ICommand _dropItemCommand = null;
        private ICommand _addFolderCommandLocal;
        private ICommand _removeItemCommand;


        public ICommand AddFileCommandLocal
        {
            get
            {
                return _addFileCommandLocal ?? (_addFileCommandLocal = new RelayCommand<string[]>(path =>
                {
                    
                    if (FileList == null)
                        FileList = new FileList
                        {
                            Items = new ObservableCollection<SystemItem>()
                        };
                    if (AddFileCommand != null)
                        AddFileCommand.Execute(path);

                    if (FileList.Items.Count == 0) FileList = null;
                }));
            }
        }

        public ICommand AddFolderCommandLocal
        {
            get
            {
                return _addFolderCommandLocal ?? (_addFolderCommandLocal = new RelayCommand<string[]>(path =>
                {
                    if (FileList == null)
                        FileList = new FileList
                        {
                            Items = new ObservableCollection<SystemItem>()
                        };

                    if (AddFolderCommand != null)
                        AddFolderCommand.Execute(path);

                    if (FileList.Items.Count == 0) FileList = null;
                }));
            }
        }

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
        
        public ICommand DropItemCommand
        {
            get
            {
                return _dropItemCommand ?? (_dropItemCommand = new RelayCommand<object>(
                (param) =>
                {
                    IDataObject ido = param as IDataObject;

                    if (ido.GetDataPresent(DataFormats.FileDrop))
                    {
                        var filePath = (string[])ido.GetData(DataFormats.FileDrop);

                        List<string> folderMassive = new List<string>();
                        List<string> fileMassive = new List<string>();
                        
                        foreach (var fp in filePath)
                        {
                            if(Directory.Exists(fp))
                                folderMassive.Add(fp);
                            else if (File.Exists(fp))
                                fileMassive.Add(fp);
                            else
                            {
                                //TODO придумать обработку
                            }
                        }

                        if (folderMassive.Count > 0) AddFolderCommandLocal.Execute(folderMassive.ToArray());
                        if (fileMassive.Count > 0) AddFileCommandLocal.Execute(fileMassive.ToArray());
                    }
                }));
            }
        }

    }
}
