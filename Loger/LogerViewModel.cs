using System.Diagnostics;
using Prism.Events;
using Prism.Mvvm;

namespace Loger
{
    public class LogerViewModel : BindableBase
    {
        private string _log;

        public LogerViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<PubSubEvent<string>>().Subscribe(PublishMsg);
        }

        public string Log
        {
            get { return _log; }
            set { SetProperty(ref _log, value); }
        }

        private void PublishMsg(string msg)
        {
            Log = msg + "\r\n" + Log;
            Debug.Print(msg);
        }
    }
}