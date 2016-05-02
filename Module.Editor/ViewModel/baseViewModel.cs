using System;
using Prism.Mvvm;
using Prism.Regions;
using System.Xml;

namespace Module.Editor.ViewModel
{
    public class baseViewModel : BindableBase, INavigationAware
    {
        private string _curentParamName;
        public XmlElement XmlNode { get; protected set; }


        public baseViewModel()
        {
            _curentParamName = this.GetType().Name.Replace("ViewModel", "");
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var xml = navigationContext.Parameters[_curentParamName] as XmlElement;
            return xml == null ? true : xml == XmlNode;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            XmlNode = navigationContext.Parameters[_curentParamName] as XmlElement;
            if (XmlNode == null) throw new ArgumentNullException("При навигации обязательныо нужно передавать параметры");
            if (XmlNode.Name != _curentParamName) throw new ArgumentException("Передан не верный параметр");
        }
    }
}
