using Prism.Events;
using Prism.Mvvm;
using System.Diagnostics;

namespace Loger
{

    public class Viewmodel: BindableBase
    {
        private string _log;

        #region Services

        private readonly IEventAggregator _eventAggregator;

        #endregion


        public string Log
        {
            get { return _log; }
            set { SetProperty(ref _log, value); }
        }


        public Viewmodel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<PubSubEvent<string>>().Subscribe(PublishMsg);
        }

        private void PublishMsg(string msg)
        {
            Log = msg + "\r\n" + Log;
            Debug.Print(msg);
        }
    }
}
