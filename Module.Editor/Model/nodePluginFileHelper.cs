using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Module.Editor.Model
{
    public class nodePluginFileHelper:nodeBase
    {
        private readonly XmlNode _typeDescriptorNode;

        public nodePluginFileHelper(XmlNode node):base(node)
        {
            if (node.Name != "typeDescriptor") throw new ArgumentException();
            _typeDescriptorNode = node;
        }



    }


   
}
