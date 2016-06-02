using Prism.Events;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loger
{

    public class viewmodel:BindableBase
    {
        string _log;

        #region Services

        private readonly IEventAggregator _eventAggregator;

        #endregion


        public string Log
        {
            get { return _log; }
            set { _log = value; OnPropertyChanged(nameof(Log)); }
        }


        public viewmodel(IEventAggregator eventAggregator)
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
