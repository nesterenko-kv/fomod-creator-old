using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System.Collections.ObjectModel;

namespace Module.Editor.ViewModel
{
    public class PluginViewModel : BaseViewModel
    {
        public PluginViewModel()
        {
            AddFlagDependencyCommand = new RelayCommand(AddFlagDependency);
            RemoveFlagDependencyCommand = new RelayCommand<SetConditionFlag>(RemoveFlagDependency);
            AddFileCommand = new RelayCommand<ObservableCollection<SystemItem>>(AddFile);
            AddFolderCommand = new RelayCommand<ObservableCollection<SystemItem>>(AddFolder);
            RemoveSystemItemCommand = new RelayCommand<SystemItem>(RemoveSystemItem);
        }
        
        #region Commands

        public RelayCommand AddFlagDependencyCommand { get; private set; }
        public RelayCommand<SetConditionFlag> RemoveFlagDependencyCommand { get; private set; }
        public RelayCommand<SystemItem> RemoveSystemItemCommand { get; private set; }
        public RelayCommand<ObservableCollection<SystemItem>> AddFolderCommand { get; private set; }
        public RelayCommand<ObservableCollection<SystemItem>> AddFileCommand { get; private set; }
        
        #endregion

        private void AddFlagDependency()
        {
            var plugin = Data as Plugin;
            if (plugin == null) return;
            if (plugin.ConditionFlags == null)
                plugin.ConditionFlags = new ConditionFlagList();
            if (plugin.ConditionFlags.Flag == null)
                plugin.ConditionFlags.Flag = new ObservableCollection<SetConditionFlag>();
            plugin.ConditionFlags.Flag.Add(SetConditionFlag.Create());
        }

        private void RemoveFlagDependency(SetConditionFlag conditionFlag)
        {
            var plugin = Data as Plugin;
            if (plugin == null) return;
            plugin.ConditionFlags.Flag.Remove(conditionFlag);
            if (plugin.ConditionFlags.Flag.Count == 0)
                plugin.ConditionFlags = null;
        }

        private void AddFile(ObservableCollection<SystemItem> systemItems)
        {
            systemItems?.Add(FileSystemItem.Create());
        }

        private void AddFolder(ObservableCollection<SystemItem> systemItems)
        {
            systemItems?.Add(FolderSystemItem.Create());
        }

        private void RemoveSystemItem(SystemItem systemItem)
        {
            systemItem?.Parent.Remove(systemItem);
        }
    }
}