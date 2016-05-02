using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using Module.Editor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Module.Editor.ViewModel
{
    public class pluginTypeViewModel
    {
        private nodePluginTypeHelper _nodePluginType;
        XmlNode _xmlNode;

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public XmlNode XmlNode
        {
            get
            {
                return _xmlNode;
            }
            set
            {
                _xmlNode = value;
                _nodePluginType = new nodePluginTypeHelper(value);
                _isSimpleTypeDescriptor = _nodePluginType.IsSimpleTypeDescriptor;
            }
        }

        bool _isSimpleTypeDescriptor;


        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public bool IsSimpleTypeDescriptor
        {
            get
            {

                return _isSimpleTypeDescriptor;
            }
            set
            {

                _isSimpleTypeDescriptor = value;

                if (value)
                {
                    _nodePluginType.ChangeTypeToSimpleType();
                }
                else
                {
                    _nodePluginType.ChangeTypeToCompositeType();
                }


            }
        }


    }
}
