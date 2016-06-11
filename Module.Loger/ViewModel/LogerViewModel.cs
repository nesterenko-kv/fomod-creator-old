using System;
using System.Diagnostics;
using AspectInjector.Broker;
using FomodInfrastructure.Aspects;
using Module.Loger.PrismEvent;
using Prism.Events;

namespace Module.Loger.ViewModel
{
    public class LogerViewModel
    {
        public LogerViewModel(IEventAggregator eventAggregator)
        {
            eventAggregator.GetEvent<SendLog>().Subscribe(PublishMsg);
        }

        [Aspect(typeof(AspectINotifyPropertyChanged))]
        public string Log { get; set; }

        private void PublishMsg(string msg)
        {
            Log = msg + Environment.NewLine + Log;
            Debug.Print(msg);
        }
    }
}