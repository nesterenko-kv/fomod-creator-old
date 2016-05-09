using System;
using System.Xml;

namespace Module.Editor.Model
{
    public class NodePluginFilesFoldersFlagsHelper : NodeBase
    {
        private readonly XmlNode _typeDescriptorNode;

        public NodePluginFilesFoldersFlagsHelper(XmlNode node) : base(node)
        {
            if (node.Name != "Plugin") throw new ArgumentException();
            _typeDescriptorNode = node;
        }
    }
}
