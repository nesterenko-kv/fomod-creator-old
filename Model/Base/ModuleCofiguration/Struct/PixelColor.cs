using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FomodModel.Base.ModuleCofiguration.Struct
{
    [Serializable]
    public struct PixelColor
    {
        [XmlAttribute]
        public byte Blue;
        [XmlAttribute]
        public byte Green;
        [XmlAttribute]
        public byte Red;
        [XmlAttribute]
        public byte Alpha;
    }
}
