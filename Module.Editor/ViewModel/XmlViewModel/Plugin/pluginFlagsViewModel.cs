using FomodInfrastructure.MvvmLibrary.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace Module.Editor.ViewModel
{
    public class pluginFlagsViewModel
    {
        XmlNode _xmlNode;
        public XmlNode XmlNode
        {
            get
            {
                return _xmlNode;
            }
            set
            {
                _xmlNode = value;
            }
        }



        ICommand _deleteFlag;
        public ICommand DeleteFlag
        {
            get
            {
                //параметр flag
                return _deleteFlag != null ? _deleteFlag : _deleteFlag = new RelayCommand(p =>
                {
                    var pa = p;
                });
            }
        }

        ICommand _addFlag;
        public ICommand AddFlag
        {
            get
            {
                //параметр conditionFlags
                return _addFlag != null ? _addFlag : _addFlag = new RelayCommand(p =>
                {

                });
            }
        }

        ICommand _removeConditionFlags;
        public ICommand RemoveConditionFlags
        {
            get
            {
                //параметр conditionFlags
                return _removeConditionFlags != null ? _removeConditionFlags : _removeConditionFlags = new RelayCommand(p =>
                {

                });
            }
        }
    }
}
