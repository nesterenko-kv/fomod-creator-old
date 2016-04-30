using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Module.Editor.Model
{
    public class nodePlugin
    {

        public void ChangeTypeToSimpleType(XmlElement pluginNode)
        {
            if (!ChkNode(pluginNode)) throw new ArgumentException(); 

            //typeDescriptor всегда последний элемент
            var typeDescriptor = pluginNode.LastChild;
            if (typeDescriptor.Name != "typeDescriptor") throw new ArgumentException();

            //<type name="Optional"/>

            typeDescriptor.RemoveAll();
            

            var typeAttribute = typeDescriptor.OwnerDocument.CreateAttribute("name");
            typeAttribute.Value = "NotUsable";
            var typeNode = typeDescriptor.OwnerDocument.CreateNode(XmlNodeType.Element, "type", "");
            typeNode.Attributes.Append(typeAttribute);

            typeDescriptor.AppendChild(typeNode);


            var rrr = "";
        }



        public void ChangeTypeToCompositeType(XmlElement pluginNode)
        {
            if (ChkNode(pluginNode)) return;

        }




        private bool ChkNode(XmlElement Node)
        {
            if (Node.Name == "plugin")
            {
                return true;
            }
            return false;
        }
    }
}
