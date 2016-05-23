using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System.Collections;
using System.Collections.ObjectModel;

namespace Module.Editor.ViewModel
{
    public class PluginViewModel : BaseViewModel
    {
        public PluginViewModel()
        {
            AddFlagDependencyCommand = new RelayCommand(AddFlagDependency);
            RemoveFlagDependencyCommand = new RelayCommand<SetConditionFlag>(RemoveFlagDependency);

            AddFileCommand = new RelayCommand<object>(AddFile);
            AddFolderCommand = new RelayCommand<object>(AddFolder);
            RemoveSystemItemCommand = new RelayCommand<SystemItem>(RemoveSystemItem);

        }

        public RelayCommand AddFlagDependencyCommand { get; private set; }
        public RelayCommand<SetConditionFlag> RemoveFlagDependencyCommand { get; private set; }
        public RelayCommand<SystemItem> RemoveSystemItemCommand { get; private set; }
        public RelayCommand<object> AddFolderCommand { get; private set; }
        public RelayCommand<object> AddFileCommand { get; private set; }

        private void AddFlagDependency()
        {
            if ((Data as Plugin).ConditionFlags == null) (Data as Plugin).ConditionFlags = new ConditionFlagList { Flag = new ObservableCollection<SetConditionFlag>() };
            (Data as Plugin).ConditionFlags.Flag.Add(new SetConditionFlag { Name = "New Flag", Value = "null" });
        }
     
        private void RemoveFlagDependency(SetConditionFlag conditionFlag)
        {
            (Data as Plugin).ConditionFlags.Flag.Remove(conditionFlag);
            if ((Data as Plugin).ConditionFlags.Flag.Count == 0) (Data as Plugin).ConditionFlags = null;
        }


        private void AddFile(object obj)
        {
            if (obj is IList)
            {
                (obj as IList).Add(new FileSystemItem
                {
                    Source = @"\ffga\kfdd.exe",
                    Destination = @"\kfdd.exe",
                    AlwaysInstall = false,
                    InstallIfUsable = false,
                    Priority = "0"
                });
                return;
            }
        }
        private void AddFolder(object obj)
        {
            if (obj is IList)
            {
                (obj as IList).Add(new FolderSystemItem
                {
                    Source = @"\ffga\folder\1\folderNew",
                    Destination = @"\folderNew",
                    AlwaysInstall = false,
                    InstallIfUsable = false,
                    Priority = "0"
                });
                return;
            }
        }

        private void RemoveSystemItem(SystemItem systemItem)
        {
            systemItem.Parent.Remove(systemItem);
        }

    }



}