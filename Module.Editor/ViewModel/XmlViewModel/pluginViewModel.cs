using FomodInfrastructure.MvvmLibrary.Commands;
using Module.Editor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace Module.Editor.ViewModel
{
    public class pluginViewModel : baseViewModel
    {
        private readonly xmlHelper _xmlHelper;

        public pluginViewModel(xmlHelper xmlHelper)
        {
            _xmlHelper = xmlHelper;
        }

        ICommand _changeTypeCommand;

        public ICommand ChangeTypeCommand
        {
            get
            {
                if (_changeTypeCommand == null)
                    _changeTypeCommand = new RelayCommand(p =>
                    {
                        _xmlHelper.nodePlugin.ChangeTypeToSimpleType(p as XmlElement);
                    });
                return _changeTypeCommand;
            }
        }
    }
}
