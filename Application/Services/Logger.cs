using System;
using System.Collections.Generic;
using FomodInfrastructure.Interface;
using Prism.Events;

namespace MainApplication.Services
{
    public class Logger : ILogger
    {
        private static readonly Dictionary<Type, int> _counts = new Dictionary<Type, int>();

        private readonly IEventAggregator _eventAggregator;

        public Logger(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
            LogCreate(this);
        }

        public void Log(string msg)
        {
            _eventAggregator.GetEvent<PubSubEvent<string>>().Publish($"[{DateTime.Now.ToLongTimeString()}] {msg}");
        }

        public void LogCreate(object obj)
        {
            _eventAggregator.GetEvent<PubSubEvent<string>>().Publish($"[{DateTime.Now.ToLongTimeString()}] [{obj.GetHashCode()}] [Create] [{Increment(obj)}] {obj.GetType().Name}");
        }

        public void LogDisposable(object obj)
        {
            _eventAggregator.GetEvent<PubSubEvent<string>>().Publish($"[{DateTime.Now.ToLongTimeString()}] [{obj.GetHashCode()}] [Disposable] [{Decrement(obj)}] {obj.GetType().Name}");
        }

        ~Logger()
        {
            LogDisposable(this);
        }

        public void Log(string msg, object forName)
        {
            _eventAggregator.GetEvent<PubSubEvent<string>>().Publish($"[{DateTime.Now.ToLongTimeString()}] {msg} ({forName.GetType().Name})");
        }

        private int Increment(object obj)
        {
            int count;
            if (_counts.TryGetValue(obj.GetType(), out count))
                _counts[obj.GetType()] = ++count;
            else
                _counts.Add(obj.GetType(), ++count);
            return count;
        }

        private int Decrement(object obj)
        {
            int count;
            if (_counts.TryGetValue(obj.GetType(), out count))
                _counts[obj.GetType()] = --count;
            return count;
        }
    }
}