using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Module.Editor.Model
{
    public abstract class nodeBase
    {
        protected XmlDocument _xmlDoc { get;  set; }


        public nodeBase(XmlNode pluginNode)
        {
            _xmlDoc = pluginNode.OwnerDocument;
        }

        #region class private

        protected XmlNode CreateNode(string name, params xAttribute[] attribute)
        {
            var typeNode = _xmlDoc.CreateNode(XmlNodeType.Element, name, "");
            foreach (var item in attribute)
            {
                SetAttributeVlaue(typeNode, item);
            }
            return typeNode;
        }

        protected void SetAttributeVlaue(XmlNode Node, xAttribute xAttribute)
        {
            var attr = Node.Attributes[xAttribute.Name]; //(xAttribute.Name);
            if (attr == null)
            {
                attr = _xmlDoc.CreateAttribute(xAttribute.Name);
            }
            attr.Value = xAttribute.Value;
            Node.Attributes.Append(attr);
        }

        #endregion
    }

    public class xAttribute
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
    public class xElement
    {
        public string Name { get; set; }
        public object Value { get; set; }
    }
}
