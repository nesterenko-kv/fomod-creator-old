using System;
using Prism.Mvvm;
using Prism.Regions;
using System.Xml;
using System.Collections.Generic;

namespace Module.Editor.ViewModel
{
    public class baseViewModel : BindableBase, INavigationAware
    {
        private string _curentParamName;


        protected XmlElement _xmlNode;
        public XmlElement XmlNode
        {
            get
            {
                return _xmlNode;
            }
            protected set
            {
                _xmlNode = value;
                foreach (var action in actionList)
                {
                    action.Invoke(value);
                }
            }
        }


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


        private List<Action<XmlElement>> actionList = new List<Action<XmlElement>>();
        protected void thenSetXmlNode(Action<XmlElement> action) { actionList.Add(action); }
    }
}
