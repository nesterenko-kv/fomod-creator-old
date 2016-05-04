using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.MvvmLibrary.Commands;
using Module.Editor.Events;
using Module.Editor.Model;
using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;

namespace Module.Editor.ViewModel
{
    [Aspect(typeof(AspectINotifyPropertyChanged))]
    public class pluginFileViewMode: BindableBase
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

        //[Aspect(typeof(AspectINotifyPropertyChanged))]
        public bool IsHightVariant { get; set; }


        IEventAggregator _eventAggregator;

        public pluginFileViewMode(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;


            _eventAggregator.GetEvent<FilesFoldersFlagsChanged>().Subscribe(FilesFoldersFlags_Changed);

        }




        XmlDocumentFragment _xmltemp;

        private void FilesFoldersFlags_Changed(bool obj)
        {
            //TODO обработать ошибки
            var xdoc = _xmlNode.OwnerDocument;
            var cFlags = xdoc.SelectNodes("conditionFlags");
            var files = xdoc.SelectNodes("files");

            if (_xmltemp == null)
                _xmltemp = xdoc.CreateDocumentFragment();


            if (obj)
            {
                if (!chkFrament_var2(_xmlNode)) throw new FormatException(); //проверяем документ
                if (!chkFrament_var1(_xmltemp.OwnerDocument))  //проверяем темп на наличие преведущей версии
                {
                    //надо поместить в память содержимое
                }
                else
                {
                    //создаем шаблон
                }

                //очищаем темп + документ и заносим текущее содержимое
                _xmltemp.RemoveAll();
                _xmltemp.AppendChild(cFlags[0]);
                xdoc.RemoveChild(cFlags[0]);

                foreach (XmlNode file in files)
                {
                    _xmltemp.AppendChild(file);
                    _xmltemp.RemoveChild(file);
                }

                //добавляем в документ содержимое из временной переменной темп

            }
            else
            {
                if (!chkFrament_var1(_xmlNode)) throw new FormatException(); //проверяем документ

                _xmltemp.RemoveAll();
                _xmltemp.AppendChild(files[0]);
                foreach (XmlNode flag in cFlags)
                {
                    _xmltemp.AppendChild(flag);
                }
            }

            var f = (_xmlNode as XmlElement);
        }



        private bool chkFrament_var2(XmlNode xdoc)
        {
            var cFlags = xdoc.SelectNodes("conditionFlags");
            var files = xdoc.SelectNodes("files");

            if (cFlags.Count != 1)
                return false;
            if (files.Count > 0 && cFlags[0].NextSibling.Name != "files")
                return false;

            return true;
        }
        private bool chkFrament_var1(XmlNode xdoc)
        {
            var cFlags = xdoc.SelectNodes("conditionFlags");
            var files = xdoc.SelectNodes("files");

            if (files.Count != 1)
                return false;
            if (cFlags.Count > 0 && files[0].NextSibling.Name != "conditionFlags")
                return false;

            return true;
        }
    }
}
