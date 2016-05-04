using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Module.Editor.Model
{
    public class nodePluginFilesFoldersFlagsHelper : nodeBase
    {
        private readonly XmlNode _typeDescriptorNode;

        public nodePluginFilesFoldersFlagsHelper(XmlNode node) : base(node)
        {
            if (node.Name != "plugin") throw new ArgumentException();
            _typeDescriptorNode = node;
        }



    }


   
}
