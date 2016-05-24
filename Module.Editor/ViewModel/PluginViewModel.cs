using FomodInfrastructure.MvvmLibrary.Commands;
using FomodModel.Base.ModuleCofiguration;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Module.Editor.ViewModel
{
    public class PluginViewModel : BaseViewModel
    {
        #region Commands

        private ICommand _addFlagDependencyCommand;
        public ICommand AddFlagDependencyCommand
        {
            get
            {
                return _addFlagDependencyCommand ?? (_addFlagDependencyCommand = new RelayCommand(() =>
                {
                    var plugin = Data as Plugin;
                    if (plugin == null) return;
                    if (plugin.ConditionFlags == null)
                        plugin.ConditionFlags = new ConditionFlagList();
                    if (plugin.ConditionFlags.Flag == null)
                        plugin.ConditionFlags.Flag = new ObservableCollection<SetConditionFlag>();
                    plugin.ConditionFlags.Flag.Add(SetConditionFlag.Create());
                }));
            }
        }

        private ICommand _removeFlagDependencyCommand;
        public ICommand RemoveFlagDependencyCommand {
            get
            {
                return _removeFlagDependencyCommand ?? (_removeFlagDependencyCommand = new RelayCommand<SetConditionFlag>(conditionFlag=>
                {
                    var plugin = Data as Plugin;
                    if (plugin == null) return;
                    plugin.ConditionFlags.Flag.Remove(conditionFlag);
                    if (plugin.ConditionFlags.Flag.Count == 0)
                        plugin.ConditionFlags = null;
                }));
            }
        }

        #endregion
    }
}