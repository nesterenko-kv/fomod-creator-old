using System;
using System.IO;
using System.Windows.Data;
using System.Xml;
using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.Interface;
using FomodInfrastructure.MvvmLibrary.Commands;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Practices.ServiceLocation;
using Module.Editor.Model;
using Module.Editor.Resource;
using Prism.Events;
using System.Windows.Media.Imaging;

namespace Module.Editor.ViewModel
{
    /// <summary>
    /// Ctor plugin
    /// </summary>
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    public partial class PluginViewModel : BaseViewModel
    {
        #region Services

        private readonly IServiceLocator _serviceLocator;
        private readonly IEventAggregator _eventAggregator;
        private readonly IDialogCoordinator _dialogCoordinator;
        private readonly IRepository<XmlDataProvider> _repositoryXml;
        private readonly IFileBrowserDialog _fileBrowserDialog;

        #endregion

        protected XmlNode SetOrCreateNodeAtribute(string name, XmlNode node, XAttribute attribute)
        {
            if (XmlNode.OwnerDocument == null) return null;
            var typeNode = node ?? XmlNode.OwnerDocument.CreateNode(XmlNodeType.Element, name, string.Empty);
            if (typeNode.Attributes == null) return typeNode;
            var attr = typeNode.Attributes[attribute.Name];
            if (attr == null)
                attr = XmlNode.OwnerDocument.CreateAttribute(attribute.Name);
            attr.Value = attribute.Value;
            typeNode.Attributes.Append(attr);
            return typeNode;
        }

        public PluginViewModel(IServiceLocator serviceLocator, IEventAggregator eventAggregator, IDialogCoordinator dialogCoordinator, IRepository<XmlDataProvider> repositoryXml, IFileBrowserDialog fileBrowserDialog)
        {
            CurentParamName = Names.PluginName;
            _serviceLocator = serviceLocator;
            _eventAggregator = eventAggregator;
            _dialogCoordinator = dialogCoordinator;
            _repositoryXml = repositoryXml;
            _fileBrowserDialog = fileBrowserDialog;


            InfoCtor();
            FilesCtor();
            FlagsCtor();
        }
    }


    /// <summary>
    /// Base plugin info
    /// </summary>
    public partial class PluginViewModel
    {
        #region Commands

        public RelayCommand AddImage { get; private set; }
        public RelayCommand RemoveImage { get; private set; }

        #endregion

        private void InfoCtor()
        {
            AddImage = new RelayCommand(() =>
            {
                var newPath = _fileBrowserDialog.ShowDialog() ? _fileBrowserDialog.SelectedPath : null;
                if (newPath != null)
                {
                    var imageNode = SetOrCreateNodeAtribute("image", XmlNode.SelectSingleNode("image"), new XAttribute { Name = "path", Value = newPath });
                    var descriptionNode = XmlNode.SelectSingleNode("description"); //TODO подумать над отсутствием узла description
                    XmlNode.InsertAfter(imageNode, descriptionNode);

                    var plagins = XmlNode.ParentNode;
                    var group = plagins.ParentNode;
                    var optionalFileGroups = group.ParentNode;
                    var installStep = optionalFileGroups.ParentNode;
                    var installSteps = installStep.ParentNode;
                    var config = installSteps.ParentNode;
                    Bitmap = getBitmap();
                }

            });

            RemoveImage = new RelayCommand(() =>
            {
                var imageNode = XmlNode.SelectSingleNode("image");
                if (imageNode != null)
                {
                    XmlNode.RemoveChild(imageNode);
                    Bitmap = null;
                }
            });
        }


        public string ImagePath
        {
            get
            {
                var imagePath = XmlNode.SelectSingleNode("image/@path")?.Value;
                if (!string.IsNullOrWhiteSpace(imagePath))
                    return /*_repositoryXml.CurrentPath +*/ imagePath;
                return @"D:\logo.jps"; // throw new FileNotFoundException();
            }
        }

        BitmapImage _bitmap;
        public BitmapImage Bitmap
        {
            get
            {
                if (_bitmap == null)
                    _bitmap = getBitmap();
                return _bitmap;
            }
            set
            {
                _bitmap = value;
            }
        }

        private BitmapImage getBitmap()
        {
            var _bitmap = new BitmapImage();
            var imagePath = XmlNode.SelectSingleNode("image/@path")?.Value;
            if (string.IsNullOrWhiteSpace(imagePath)) return null;
                //imagePath = @"D:\logo.jps";
            using (var stream = File.OpenRead(imagePath))
            {
                _bitmap.BeginInit();
                _bitmap.CacheOption = BitmapCacheOption.OnLoad;
                _bitmap.StreamSource = stream;
                _bitmap.EndInit();
            }
            return _bitmap;
        }

    }


    /// <summary>
    /// Files & Folders
    /// </summary>
    public partial class PluginViewModel
    {
        #region Commands

        public RelayCommand AddFileFolderGroup { get; private set; } 
        public RelayCommand RemoveFileFolderGroup { get; private set; }
        public RelayCommand RemoveFile { get; private set; }
        public RelayCommand AddFile { get; private set; }
        public RelayCommand AddFolder { get; private set; } 

        #endregion

        public bool IsFilesFoldersFlags { get; set; }

        private void FilesCtor()
        {
            AddFileFolderGroup = new RelayCommand(() => _dialogCoordinator.ShowMessageAsync(this, "Wow!", "Added files group"));
            RemoveFileFolderGroup = new RelayCommand(() => _dialogCoordinator.ShowMessageAsync(this, "Wow!", "Removed files group"));
            RemoveFile = new RelayCommand(() => _dialogCoordinator.ShowMessageAsync(this, "Wow!", "Removed FILE/FOLDER"));
            AddFile = new RelayCommand(() => _dialogCoordinator.ShowMessageAsync(this, "Wow!", "Added file"));
            AddFolder = new RelayCommand(() => _dialogCoordinator.ShowMessageAsync(this, "Wow!", "Added folder"));

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

        // ReSharper disable PossibleNullReferenceException
        private static bool chkFrament_var2(XmlNode xdoc)
        {
            var cFlags = xdoc.SelectNodes("conditionFlags");
            var files = xdoc.SelectNodes("files");
            
            return cFlags.Count == 1 && (files.Count <= 0 || cFlags[0].NextSibling.Name == "files");
        }
        private static bool chkFrament_var1(XmlNode pluginNode)
        {
            var cFlags = pluginNode.SelectNodes("conditionFlags");
            var files = pluginNode.SelectNodes("files");

            return files.Count == 1 && (cFlags.Count <= 0 || files[0].NextSibling.Name == "conditionFlags");
        }
        // ReSharper restore PossibleNullReferenceException

    }

    /// <summary>
    /// Flags
    /// </summary>
    public partial class PluginViewModel 
    {
        #region Commands

        public RelayCommand AddFlagsGroup { get; private set; }
        public RelayCommand RemoveFlagsGroup { get; private set; }
        public RelayCommand RemoveFlag { get; private set; }
        public RelayCommand AddFlag { get; private set; }

        #endregion

        private void FlagsCtor()
        {
            AddFlagsGroup = new RelayCommand(() => _dialogCoordinator.ShowMessageAsync(this, "Wow!", "Added flags group"));
            RemoveFlagsGroup = new RelayCommand(() => _dialogCoordinator.ShowMessageAsync(this, "Wow!", "Removed flags group"));
            RemoveFlag = new RelayCommand(() => _dialogCoordinator.ShowMessageAsync(this, "Wow!", "Removed FLAG"));
            AddFlag = new RelayCommand(() => _dialogCoordinator.ShowMessageAsync(this, "Wow!", "Added FLAG"));
        }
    }
}