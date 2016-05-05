using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.MvvmLibrary.Commands;
using Microsoft.Practices.ServiceLocation;
using Prism.Events;
using System;
using System.Windows.Input;
using System.Xml;
using Module.Editor.Resource;

namespace Module.Editor.ViewModel
{
    /// <summary>
    /// Base plugin info
    /// </summary>
    /// 
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    public partial class PluginViewModel
    {
        #region Services

        private IServiceLocator _serviceLocator;
        private IEventAggregator _eventAggregator;

        #endregion
        
        #region Commands

        private ICommand _addImage;
        private ICommand _removeImage;

        public ICommand AddImage
        {
            get
            {
                //TODO сделать проверку передаваемого параметра
                return _addImage ?? (_addImage = new RelayCommand(p =>
                {
                    //_nodePluginHelper.AddImage(p.ToString());
                }));
            }
        }

        public ICommand RemoveImage
        {
            get
            {
                //TODO сделать проверку передаваемого параметра или диалог выбора файлов (мнодественный) а также проверку форматов картинок (авось что то не то передадут)
                return _removeImage ?? (_removeImage = new RelayCommand(p =>
                {
                    //_nodePluginHelper.RemoveImage(p as XmlNode);
                }));
            }
        }

        #endregion

        public PluginViewModel(IServiceLocator serviceLocator, IEventAggregator eventAggregator)
        {
            _serviceLocator = serviceLocator;
            _eventAggregator = eventAggregator;
            FilesCtor();
            FlagsCtor();
            CurentParamName = Names.PluginName;
        }
    }

    /// <summary>
    /// Files & Folders
    /// </summary>
    public partial class PluginViewModel
    {
        #region Commands

        private ICommand _addFileFolderGroup;
        private ICommand _removeFileFolderGroup;
        private ICommand _removeFile;
        private ICommand _addFile;
        private ICommand _addFolder;

        public ICommand AddFileFolderGroup
        {
            get
            {
                return _addFileFolderGroup ?? (_addFileFolderGroup = new RelayCommand(p => System.Windows.MessageBox.Show("Wow! Added files group")));
            }
        }
        public ICommand RemoveFileFolderGroup
        {
            get
            {
                return _removeFileFolderGroup ?? (_removeFileFolderGroup = new RelayCommand(p => System.Windows.MessageBox.Show("Wow! Remove files group")));
            }
        }
        public ICommand RemoveFile
        {
            get
            {
                return _removeFile ?? (_removeFile = new RelayCommand(p => System.Windows.MessageBox.Show("Wow! Remove FILE/FOLDE")));
            }
        }
        public ICommand AddFile
        {
            get
            {
                return _addFile ?? (_addFile = new RelayCommand(p => System.Windows.MessageBox.Show("Wow! added file")));
            }
        }
        public ICommand AddFolder
        {
            get
            {
                return _addFolder ?? (_addFolder = new RelayCommand(p => System.Windows.MessageBox.Show("Wow! added folder")));
            }
        }
        
        #endregion

        public bool IsFilesFoldersFlags { get; set; }

        private void FilesCtor()
        {
            ThenSetXmlNode(xmlNode =>
            {
                if (chkFrament_var1(xmlNode))
                    IsFilesFoldersFlags = true;
                else if (chkFrament_var2(xmlNode))
                    IsFilesFoldersFlags = false;
                else
                    throw new NotImplementedException();
            });
        }

        private static bool chkFrament_var2(XmlNode xdoc)
        {
            var cFlags = xdoc.SelectNodes("conditionFlags");
            var files = xdoc.SelectNodes("files");

            if (cFlags.Count != 1)
                return false;
            if (files.Count > 0 && cFlags[0].NextSibling.Name != "files")
                return false;
            return true;
        }
        private static bool chkFrament_var1(XmlNode pluginNode)
        {
            var cFlags = pluginNode.SelectNodes("conditionFlags");
            var files = pluginNode.SelectNodes("files");

            if (files.Count != 1)
                return false;
            if (cFlags.Count > 0 && files[0].NextSibling.Name != "conditionFlags")
                return false;
            return true;
        }
    }

    /// <summary>
    /// Flags
    /// </summary>
    public partial class PluginViewModel : BaseViewModel
    {
        #region Commands

        private ICommand _addFlagsGroup;
        private ICommand _removeFlagsGroup;
        private ICommand _removeFlag;
        private ICommand _addFlag;

        public ICommand AddFlagsGroup
        {
            get
            {
                return _addFlagsGroup ?? (_addFlagsGroup = new RelayCommand(p => System.Windows.MessageBox.Show("Wow! Added flags group")));
            }
        }

        public ICommand RemoveFlagsGroup
        {
            get
            {
                return _removeFlagsGroup ?? (_removeFlagsGroup = new RelayCommand(p => System.Windows.MessageBox.Show("Wow! Remove flags group")));
            }
        }

        public ICommand RemoveFlag
        {
            get
            {
                return _removeFlag ?? (_removeFlag = new RelayCommand(p => System.Windows.MessageBox.Show("Wow! Remove FLAG")));
            }
        }

        public ICommand AddFlag
        {
            get
            {
                return _addFlag ?? (_addFlag = new RelayCommand(p => System.Windows.MessageBox.Show("Wow! added FLAG")));
            }
        }
        
        #endregion

        private void FlagsCtor()
        {
            
        }
        
    }
    
}