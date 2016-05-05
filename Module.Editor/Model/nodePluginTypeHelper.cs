using System;
using System.Xml;

namespace Module.Editor.Model
{
    public class NodePluginTypeHelper: NodeBase
    {
        private readonly XmlNode _typeDescriptorNode;

        private XmlNode _previewSimpleTypeDescriptor;
        private XmlNode _previewStrongeTypeDescriptor;

        public NodePluginTypeHelper(XmlNode pluginNode): base(pluginNode)
        {
            if (pluginNode.Name != "typeDescriptor") throw new ArgumentException();
            _typeDescriptorNode = pluginNode;
        }

        #region TypeDescriptor

        #region private
        private XmlNode GetTypeDescriptor()
        {
            return _typeDescriptorNode;
        }

        private XmlNode GetStrongType()
        {
            var typeDescriptor = GetTypeDescriptor();
            return typeDescriptor.SelectSingleNode("dependencyType");

        }
        private XmlNode GetSimpleType()
        {
            var typeDescriptor = GetTypeDescriptor();
            return typeDescriptor.SelectSingleNode("type");
        }


        #endregion

        public bool IsSimpleTypeDescriptor
        {
            get
            {
                var typeDescriptor = GetTypeDescriptor();
                var lastNode = typeDescriptor.ChildNodes[0];
                if (lastNode.Name != "type" & lastNode.Name != "dependencyType") throw new ArgumentException();
                return lastNode.Name == "type";
            }
        }
        public void ChangeTypeToSimpleType()
        {
            var typeDescriptor = GetTypeDescriptor();
            _previewStrongeTypeDescriptor = GetStrongType();
            var newValue = _previewSimpleTypeDescriptor ?? CreateNode("type", new XAttribute { Name = "name", Value = "NotUsable" });
            var oldValue = typeDescriptor.SelectSingleNode("dependencyType");
            if (oldValue != null) _typeDescriptorNode.ReplaceChild(newValue, oldValue);
        }
        public void ChangeTypeToCompositeType()
        {
            var typeDescriptor = GetTypeDescriptor();
            _previewSimpleTypeDescriptor = GetSimpleType();

            var newValue = _previewStrongeTypeDescriptor;
            var oldValue = typeDescriptor.SelectSingleNode("type");

            if (newValue == null)
            {
                var dependencyType = CreateNode("dependencyType");
                var defaultType = CreateNode("defaultType", new XAttribute { Name = "name", Value = "Recommended" });
                dependencyType.AppendChild(defaultType);
                newValue = dependencyType;
            }

            if (oldValue != null) _typeDescriptorNode.ReplaceChild(newValue, oldValue);
        }

        #endregion

    } 
}
