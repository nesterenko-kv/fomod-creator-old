using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using System.Windows.Markup;

namespace FomodInfrastructure.Converters
{
    public sealed class EnumValues : MarkupExtension
    {
        private readonly Type _enumType;

        public EnumValues(Type enumType)
        {
            _enumType = enumType;
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(_enumType);
        }
    }

    public sealed class EnumValueToDecriptionConverter : IValueConverter
    {
        #region IValueConverter

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return null;
            }
            var type = value.GetType();
            if (!type.IsEnum)
            {
                return null;
            }
            var field = type.GetField(value.ToString());
            var attr = field.GetCustomAttributes(typeof (DescriptionAttribute), true)
                .Cast<DescriptionAttribute>()
                .FirstOrDefault();
            if (attr != null)
            {
                return attr.Description;
            }
            else
            {
                return field.Name;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion


    }
}