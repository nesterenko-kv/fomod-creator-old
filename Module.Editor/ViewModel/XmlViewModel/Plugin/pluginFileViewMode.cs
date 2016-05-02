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
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    public class pluginFileViewMode
    {
        XmlNode _xmlNode;
        public XmlNode XmlNode
        {
            get
            {
                return _xmlNode;
            }
            set
            {
                _xmlNode = value;

                var files = _xmlNode.SelectSingleNode("files");
                var conditionFlags = _xmlNode.SelectSingleNode("conditionFlags");

                if ((conditionFlags != null & files != null) && conditionFlags.PreviousSibling.Name == "files")
                {
                    //1 var
                    IsHightVariant = true;
                }
                else if (conditionFlags != null && files == null)
                {
                    //2 var
                    IsHightVariant = false;
                }
                else if (conditionFlags == null && files != null)
                {
                    //1 var
                    IsHightVariant = true;
                }
            }
        }

        public bool IsHightVariant { get; set; }



    }
}
