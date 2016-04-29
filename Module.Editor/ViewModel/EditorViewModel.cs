using AspectInjector.Broker;
using FomodInfrastructure.Aspect;
using FomodInfrastructure.Interface;
using System.Windows.Data;
using Prism.Mvvm;

namespace Module.Editor.ViewModel
{
    public class EditorViewModel: BindableBase
    {
        #region Properties

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public string Header { get; set; } = "Редактор";

        #endregion

        #region Services

        private readonly IRepository<XmlDataProvider> _repository;

        #endregion

        public EditorViewModel(IRepository<XmlDataProvider> repository)
        {
            _repository = repository; 
        }

        private XmlDataProvider _xmlData;
        public XmlDataProvider XmlData
        {
            get
            {
                if (_xmlData == null)
                    _xmlData = _repository.GetData();
                return _xmlData;
            }
        }
    }
}
