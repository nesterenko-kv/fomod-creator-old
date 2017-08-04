namespace FOMOD.Creator.ViewModels
{
    using FOMOD.Creator.Interfaces;
    using FOMOD.Creator.PrismEvent;
    using MahApps.Metro.Controls.Dialogs;
    using Prism.Events;
    using Prism.Regions;
    using PropertyChanged;
    using StructureMap;
    using System.Globalization;
    using WPFLocalizeExtension.Engine;

    [ImplementPropertyChanged]
    public class WelcomeViewModel : ProjectWorkerBaseViewModel, IHaveDisplayName
    {
        public System.Collections.ObjectModel.ObservableCollection<System.Globalization.CultureInfo> Cultures { get; set; }
        public CultureInfo SelectedCulture { get; set; }

        void OnSelectedCultureChanged()
        {
            LocalizeDictionary.Instance.SetCurrentThreadCulture = true;
            LocalizeDictionary.Instance.Culture = SelectedCulture;
            SaveSettings();
        }
        private void SaveSettings()
        {
            Properties.Settings.Default.Culture = SelectedCulture?.Name;
            Properties.Settings.Default.Save();
        }

        public WelcomeViewModel(IEventAggregator eventAggregator, IDialogCoordinator dialogCoordinator, IContainer container, IRegionManager regionManager)
            : base(dialogCoordinator, container, eventAggregator, regionManager)
        {
            eventAggregator.GetEvent<OpenLink>().Subscribe(OpenProject);

            Cultures = new System.Collections.ObjectModel.ObservableCollection<System.Globalization.CultureInfo>(Localize.JsonLocalizeProvider.Default.GetCultures());
            var curent = Properties.Settings.Default.Culture;
            if (string.IsNullOrWhiteSpace(curent))
            {
                SelectedCulture = CultureInfo.InvariantCulture;
            }
            else
            {
                SelectedCulture = CultureInfo.GetCultureInfo(curent);
            }

        }

        public string DisplayName
        {
            get
            {
                return Localize.JsonLocalizeProvider.Default
                    .GetLocalizedObject("app-main-tab-name", null, WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture)
                    .ToString();
            }
        }
    }
}
