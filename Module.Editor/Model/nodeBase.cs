using System.Xml;

namespace Module.Editor.Model
{
    public abstract class NodeBase
    {
        protected XmlDocument XmlDoc { get;  set; }

        protected NodeBase(XmlNode pluginNode)
        {
            XmlDoc = pluginNode.OwnerDocument;
        }

        protected XmlNode CreateNode(string name, params XAttribute[] attribute)
        {
            var typeNode = XmlDoc.CreateNode(XmlNodeType.Element, name, "");
            foreach (var item in attribute)
                SetAttributeVlaue(typeNode, item);
            return typeNode;
        }

        protected void SetAttributeVlaue(XmlNode node, XAttribute xAttribute)
        {
            var attr = node.Attributes?[xAttribute.Name];
            if (attr == null)
                attr = XmlDoc.CreateAttribute(xAttribute.Name);
            attr.Value = xAttribute.Value;
            node.Attributes?.Append(attr);
        }
    }
}
