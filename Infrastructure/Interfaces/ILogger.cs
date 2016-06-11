using System;

namespace FomodInfrastructure.Interfaces
{
    public interface ILogger
    {
        void Log(string msg);

        void Log(Exception exception);

        void LogCreate(object obj);

        void LogDisposable(object obj);
    }
}