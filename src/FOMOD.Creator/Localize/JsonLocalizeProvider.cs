

namespace FOMOD.Creator.Localize
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Windows;
    using WPFLocalizeExtension.Providers;
    using System.IO;
    using System.Reflection;
    using System.Linq;
    using WPFLocalizeExtension.Engine;

    class JsonLocalizeProvider : ILocalizationProvider
    {
        public static JsonLocalizeProvider Default => new JsonLocalizeProvider();

        ParentNotifiers _parentNotifiers = new ParentNotifiers();
        string _localizeFileName = "Localize";
        ApplicationLocalize _applicationLocalize;

        public ObservableCollection<CultureInfo> AvailableCultures => new ObservableCollection<CultureInfo>();

        public event ProviderChangedEventHandler ProviderChanged;
        public event ProviderErrorEventHandler ProviderError;
        public event ValueChangedEventHandler ValueChanged;

        public FullyQualifiedResourceKeyBase GetFullyQualifiedResourceKey(string inkey, DependencyObject target)
        {
            var key = string.Empty;
            var dictionary = string.Empty;
            var assembly = string.Empty;

            if (!string.IsNullOrWhiteSpace(inkey))
            {
                var split = inkey.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries)
                    .Reverse()
                    .Concat(new[] { (string)null, null })
                    .ToList();

                key = split[0];
                //dictionary = split[1] ?? _localizeFileName;
                //assembly = split[2] ?? target.GetValueOrRegisterParentNotifier(
                //    dp => Assembly.GetCallingAssembly().GetName().Name,
                //    t => ProviderChanged(this, new ProviderChangedEventArgs(t)), 
                //    _parentNotifiers);
            }
            return new FQAssemblyDictionaryKey(key);
        }
        public object GetLocalizedObject(string key, DependencyObject target, CultureInfo culture)
        {
            var localize = GetApplicationLocalize(key, target, culture);
            return _applicationLocalize.Read(key);
        }
        public object GetLocalizedObject(string key)
        {
            var localize = GetApplicationLocalize(key, null, WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture);
            return _applicationLocalize.Read(key);
        }
        public CultureInfo[] GetCultures()
        {
            var list = new System.Collections.Generic.List<CultureInfo>() { CultureInfo .InvariantCulture };
            var applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Localize\";
            foreach (var file in new DirectoryInfo(applicationPath).GetFiles())
            {
                if (file.Extension == ".json" && file.Name.Contains(_localizeFileName))
                {
                    var parts = file.Name.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 3)
                    {
                        try
                        {
                            list.Add(CultureInfo.GetCultureInfo(parts[1]));
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return list.ToArray();
        }
        ApplicationLocalize GetApplicationLocalize(string key, DependencyObject target, CultureInfo culture)
        {
            if(target is EnumComboBox)
            {
                var a = 1;
            }
            if (_applicationLocalize == null || _applicationLocalize.Culture != LocalizeDictionary.Instance.Culture)
            {
                var applicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Localize\";
                var filePath = string.Empty;

                while (culture != CultureInfo.InvariantCulture )
                {
                    filePath = Path.Combine(applicationPath, _localizeFileName + (String.IsNullOrWhiteSpace(culture.Name) ? "" : "." + culture.Name) + ".json");

                    if (File.Exists(filePath))
                        break;
                    culture = culture.Parent;
                }


                if (File.Exists(filePath) == false)
                {
                    filePath = Path.Combine(applicationPath, _localizeFileName + ".json");

                    if (!File.Exists(filePath))
                    {
                        _applicationLocalize = new ApplicationLocalize();
                        _applicationLocalize.Culture = LocalizeDictionary.Instance.Culture;
                    }
                    else
                    {
                        Load(filePath, key, target);
                    }
                }
                else
                {
                    Load(filePath, key, target);
                }
            }
            return _applicationLocalize;
        }

        void Load(string filePath, string key, DependencyObject target)
        {
            try
            {
                var json = File.ReadAllText(filePath);
                _applicationLocalize = Newtonsoft.Json.JsonConvert.DeserializeObject<ApplicationLocalize>(json);
                _applicationLocalize.Culture = LocalizeDictionary.Instance.Culture;
            }
            catch (Exception ex)
            {
                var message = $"Provider IO exception: {ex.InnerException?.Message ?? ex.Message ?? "Unknown error"}";
                ProviderError?.Invoke(this, new ProviderErrorEventArgs(target, key, message));
            }
        }
    }
}
