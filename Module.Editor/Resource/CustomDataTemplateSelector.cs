using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using FomodModel.Base;
using FomodModel.Base.ModuleCofiguration;

namespace Module.Editor.Resource
{
    public class CustomDataTemplateSelector : DataTemplateSelector
    {
        private static readonly Dictionary<Type, object> TypeToKey = new Dictionary<Type, object>
        {
            /*{typeof (Plugin), "Plugin"},
            {typeof (Group), "Group"},
            {typeof (InstallStep), "InstallStep"},*/
            {typeof (ModuleConfiguration), "Config"},
            {typeof (ModuleInformation), "Info"}
        };
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var element = container as FrameworkElement;
            if (element == null || item == null) return base.SelectTemplate(item, container);
            var itemtype = item.GetType();
            object keyObject;
            if (!TypeToKey.TryGetValue(itemtype, out keyObject)) return base.SelectTemplate(item, container);
            var template = element.TryFindResource(keyObject) as DataTemplate;
            return template ?? base.SelectTemplate(item, container);
        }
    }
}