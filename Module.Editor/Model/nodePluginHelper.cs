using System;
using System.Xml;
using Module.Editor.Resource;

namespace Module.Editor.Model
{
    public class NodePluginHelper: NodeBase
    {
        private readonly XmlElement _pluginNode;

        public NodePluginHelper(XmlElement pluginNode) : base(pluginNode)
        {
            if (pluginNode.Name != Names.PluginName) throw new ArgumentException();
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
            _pluginNode.InsertAfter(CreateNode("image", new XAttribute { Name = "path", Value = path }), lastImage ?? GetDescription());
        }
        public void RemoveImage(XmlNode node)
        {
             _pluginNode.RemoveChild(node);
        }
    }
}
