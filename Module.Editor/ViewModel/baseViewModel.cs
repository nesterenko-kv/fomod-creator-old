using System;
using Prism.Mvvm;
using Prism.Regions;
using System.Xml;
using System.Collections.Generic;

namespace Module.Editor.ViewModel
{
    public class BaseViewModel : BindableBase, INavigationAware
    {
        protected string CurentParamName { get; set; }
        private XmlElement _xmlNode;
        public XmlElement XmlNode
        {
            get
            {
                return _xmlNode;
            }
            private set
            {
                _xmlNode = value;
                foreach (var action in _actionList)
                    action.Invoke(value);
            }
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            var xml = navigationContext.Parameters[CurentParamName] as XmlElement;
            return xml == null || xml == XmlNode;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            XmlNode = navigationContext.Parameters[CurentParamName] as XmlElement;
            if (XmlNode == null) throw new ArgumentNullException(nameof(navigationContext), "При навигации обязательныо нужно передавать параметры");
            if (XmlNode.Name != CurentParamName) throw new ArgumentException("Передан не верный параметр");
        }

        private readonly List<Action<XmlElement>> _actionList = new List<Action<XmlElement>>();

        public void ThenSetXmlNode(Action<XmlElement> action)
        {
            _actionList.Add(action);
        }
    }
}
