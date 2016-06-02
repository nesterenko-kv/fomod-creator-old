using System;
using System.Diagnostics;
using Prism.Logging;
using Prism.Events;
using FomodInfrastructure.Interface;
using System.Collections;
using System.Collections.Generic;

namespace MainApplication.Services
{
    public class Logger : ILogger//ILoggerFacade
    {
        static Dictionary<Type, int> Counts = new Dictionary<Type, int>();

        private readonly IEventAggregator _eventAggregator;

        public Logger(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            LogCreate(this);
        }
        ~Logger()
        {
            LogDisposable(this);
        }

        public void Log(string message)
        {
            _eventAggregator.GetEvent<PubSubEvent<string>>().Publish($"[{DateTime.Now.ToLongTimeString()}] {message}");
        }
        public void Log(string message, object forName)
        {
            _eventAggregator.GetEvent<PubSubEvent<string>>().Publish($"[{DateTime.Now.ToLongTimeString()}] {message} ({forName.GetType().Name})");
        }

        public void LogCreate(object obj)
        {
            _eventAggregator.GetEvent<PubSubEvent<string>>().Publish($"[{DateTime.Now.ToLongTimeString()}] [{obj.GetHashCode()}] [Create] [{Increment(obj)}] {obj.GetType().Name}");
        }
        public void LogDisposable(object obj)
        {

            _eventAggregator.GetEvent<PubSubEvent<string>>().Publish($"[{DateTime.Now.ToLongTimeString()}] [{obj.GetHashCode()}] [Disposable] [{Decrement(obj)}] {obj.GetType().Name}");
        }



        private int Increment(object obj)
        {
            int count = 0;
            if (Counts.TryGetValue(obj.GetType(), out count))
                Counts[obj.GetType()] = ++count;
            else
                Counts.Add(obj.GetType(), ++count);
            return count;
        }
        private int Decrement(object obj)
        {
            int count = 0;
            if (Counts.TryGetValue(obj.GetType(), out count))
                Counts[obj.GetType()] = --count;
            return count;
        }
    }
}