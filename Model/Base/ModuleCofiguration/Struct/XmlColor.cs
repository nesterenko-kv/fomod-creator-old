using System;
using System.Drawing;
using System.Globalization;
using System.Xml.Serialization;

namespace FomodModel.Base.ModuleCofiguration.Struct
{
    [Serializable]
    public class XmlColor
    {
        private Color _color = Color.Black;

        public XmlColor() { }
        public XmlColor(Color c) { _color = c; }

        public static implicit operator Color(XmlColor x)
        {
            return x._color;
        }

        public static implicit operator XmlColor(Color c)
        {
            return new XmlColor(c);
        }

        public static string FromColor(Color color)
        {
            return $"{color.ToArgb() & 0x00ffffff:X6}";
        }

        public static Color ToColor(string value)
        {
            try
            {
                return Color.FromArgb((int)(uint.Parse(value, NumberStyles.HexNumber, null) | 0xff000000));
            }
            catch (Exception)
            {
                return Color.Black;
            }
        }

        [XmlText]
        public string Default
        {
            get { return FromColor(_color); }
            set { _color = ToColor(value); }
        }
    }
}