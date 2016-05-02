using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Module.Editor.Model
{
    public class nodePluginHelper: nodeBase
    {
        private readonly XmlElement _pluginNode;

        public nodePluginHelper(XmlElement pluginNode) : base(pluginNode)
        {
            if (pluginNode.Name != "plugin") throw new ArgumentException();
            _pluginNode = pluginNode;
        }



        private XmlNode GetDescription()
        {
            var descriptor = _pluginNode.SelectSingleNode("description");
            if (descriptor == null) throw new ArgumentException();

            return descriptor;
        }

        public void AddImage(string path)
        {
            var lastImage = _pluginNode.SelectSingleNode("image[last()]");
            _pluginNode.InsertAfter(CreateNode("image", new xAttribute { Name = "path", Value = path }), lastImage ?? GetDescription());
        }
        public void RemoveImage(XmlNode Node)
        {
             _pluginNode.RemoveChild(Node);
        }



    }


  
}
