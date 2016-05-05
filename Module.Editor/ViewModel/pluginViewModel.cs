using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.MvvmLibrary.Commands;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace Module.Editor.ViewModel
{
    /// <summary>
    /// Base plugin info
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    public partial class pluginViewModel : baseViewModel
    {
        IServiceLocator _serviceLocator;
        IEventAggregator _eventAggregator;

        public pluginViewModel(IServiceLocator serviceLocator, IEventAggregator eventAggregator)
        {
            _serviceLocator = serviceLocator;
            _eventAggregator = eventAggregator;

            filesCtor();
            FlagsCtor();
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }

        ICommand _addImage;
        public ICommand AddImage
        {
            get
            {
                //TODO сделать проверку передаваемого параметра
                return _addImage != null ? _addImage : _addImage = new RelayCommand(p =>
                    {
                    //_nodePluginHelper.AddImage(p.ToString());
                });
            }
        }

        ICommand _removeImage;
        public ICommand RemoveImage
        {
            get
            {
                //TODO сделать проверку передаваемого параметра или диалог выбора файлов (мнодественный) а также проверку форматов картинок (авось что то не то передадут)
                return _removeImage != null ? _removeImage : _removeImage = new RelayCommand(p =>
                {
                    //_nodePluginHelper.RemoveImage(p as XmlNode);
                });
            }
        }

    }

    /// <summary>
    /// Files and folders
    /// </summary>
    public partial class pluginViewModel : baseViewModel
    {
        public bool IsFilesFoldersFlags { get; set; }
        
        void filesCtor()
        {
            thenSetXmlNode((xmlNode) =>
            {
                if (chkFrament_var1(xmlNode))
                    IsFilesFoldersFlags = true;
                else if (chkFrament_var2(xmlNode))
                    IsFilesFoldersFlags = false;
                else
                    throw new NotImplementedException();
            });
        }

        private bool chkFrament_var2(XmlNode xdoc)
        {
            var cFlags = xdoc.SelectNodes("conditionFlags");
            var files = xdoc.SelectNodes("files");

            if (cFlags.Count != 1)
                return false;
            if (files.Count > 0 && cFlags[0].NextSibling.Name != "files")
                return false;
            return true;
        }
        private bool chkFrament_var1(XmlNode pluginNode)
        {
            var cFlags = pluginNode.SelectNodes("conditionFlags");
            var files = pluginNode.SelectNodes("files");

            if (files.Count != 1)
                return false;
            if (cFlags.Count > 0 && files[0].NextSibling.Name != "conditionFlags")
                return false;
            return true;
        }



        ICommand _addFileFolderGroup;
        public ICommand AddFileFolderGroup
        {
            get
            {
                return _addFileFolderGroup != null ? _addFileFolderGroup : _addFileFolderGroup = new RelayCommand(p =>
                {
                    System.Windows.MessageBox.Show("Wow! Added files group");
                });
            }
        }

        ICommand _removeFileFolderGroup;
        public ICommand RemoveFileFolderGroup
        {
            get
            {
                return _removeFileFolderGroup != null ? _removeFileFolderGroup : _removeFileFolderGroup = new RelayCommand(p =>
                {
                    System.Windows.MessageBox.Show("Wow! Remove files group");
                });
            }
        }

        ICommand _removeFile;
        public ICommand RemoveFile
        {
            get
            {
                return _removeFile != null ? _removeFile : _removeFile = new RelayCommand(p =>
                {
                    System.Windows.MessageBox.Show("Wow! Remove FILE/FOLDE");
                });
            }
        }
        ICommand _addFile;
        public ICommand AddFile
        {
            get
            {
                return _addFile != null ? _addFile : _addFile = new RelayCommand(p =>
                {
                    System.Windows.MessageBox.Show("Wow! added file");
                });
            }
        }

        ICommand _addFolder;
        public ICommand AddFolder
        {
            get
            {
                return _addFolder != null ? _addFolder : _addFolder = new RelayCommand(p =>
                {
                    System.Windows.MessageBox.Show("Wow! added folder");
                });
            }
        }

    }

    /// <summary>
    /// Flags
    /// </summary>
    public partial class pluginViewModel : baseViewModel
    {

        void FlagsCtor()
        {
            
        }

        ICommand _addFlagsGroup;
        public ICommand AddFlagsGroup
        {
            get
            {
                return _addFlagsGroup != null ? _addFlagsGroup : _addFlagsGroup = new RelayCommand(p =>
                {
                    System.Windows.MessageBox.Show("Wow! Added flags group");
                });
            }
        }

        ICommand _removeFlagsGroup;
        public ICommand RemoveFlagsGroup
        {
            get
            {
                return _removeFlagsGroup != null ? _removeFlagsGroup : _removeFlagsGroup = new RelayCommand(p =>
                {
                    System.Windows.MessageBox.Show("Wow! Remove flags group");
                });
            }
        }

        ICommand _removeFlag;
        public ICommand RemoveFlag
        {
            get
            {
                return _removeFlag != null ? _removeFlag : _removeFlag = new RelayCommand(p =>
                {
                    System.Windows.MessageBox.Show("Wow! Remove FLAG");
                });
            }
        }
        ICommand _addFlag;
        public ICommand AddFlag
        {
            get
            {
                return _addFlag != null ? _addFlag : _addFlag = new RelayCommand(p =>
                {
                    System.Windows.MessageBox.Show("Wow! added FLAG");
                });
            }
        }
    }


}
