using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.MvvmLibrary.Commands;
using Microsoft.Practices.ServiceLocation;
using Module.Editor.Events;
using Module.Editor.Model;
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
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    public class pluginViewModel : baseViewModel
    {
        IServiceLocator _serviceLocator;
        IEventAggregator _eventAggregator;

        public pluginViewModel(IServiceLocator serviceLocator, IEventAggregator eventAggregator)
        {
            _serviceLocator = serviceLocator;
            _eventAggregator = eventAggregator;
        }


        private nodePluginHelper _nodePluginHelper;

        public pluginTypeViewModel pluginTypeViewModel { get; set; }
        public pluginFileViewMode pluginFileViewMode { get; set; }
        public pluginFlagsViewModel pluginFlagsViewModel { get; set; }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            _nodePluginHelper = new nodePluginHelper(XmlNode);


            var files = XmlNode.SelectSingleNode("files");
            var conditionFlags = XmlNode.SelectSingleNode("conditionFlags");

            if ((conditionFlags != null & files != null) && conditionFlags.PreviousSibling.Name == "files")
            {
                //1 var
                IsFilesFoldersFlags = true;
            }
            else if (conditionFlags != null && files == null)
            {
                //2 var
                IsFilesFoldersFlags = false;
            }
            else if (conditionFlags == null && files != null)
            {
                //1 var
                IsFilesFoldersFlags = true;
            }


            factory(nameof(pluginTypeViewModel), "typeDescriptor");
            factory(nameof(pluginFileViewMode), "plugin");
            factory(nameof(pluginFlagsViewModel), "plugin");

        }

        private void factory(string propertyName, string nodeName) 
        {
            XmlNode node = null;
            if (nodeName == XmlNode.Name)
            {
                node = (XmlNode)XmlNode;
            }
            else
            {
                node = XmlNode.SelectSingleNode(nodeName);
            }
            var property = this.GetType().GetProperty(propertyName);

            var vm = _serviceLocator.GetInstance(property.PropertyType);
            (vm as dynamic).XmlNode = node;
            property.SetValue(this, vm);

        }



        ICommand _addImage;
        public ICommand AddImage
        {
            get
            {
                //TODO сделать проверку передаваемого параметра
                return _addImage!=null? _addImage: _addImage = new RelayCommand(p =>
                {
                    _nodePluginHelper.AddImage(p.ToString());
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
                    _nodePluginHelper.RemoveImage(p as XmlNode);
                });
            }
        }

        bool _isFilesFoldersFlags;
        public bool IsFilesFoldersFlags
        {
            get
            {
                return _isFilesFoldersFlags;
            }
            set
            {
                _isFilesFoldersFlags = value;
                _eventAggregator.GetEvent<FilesFoldersFlagsChanged>().Publish(value);
            }
        }



    }
}
