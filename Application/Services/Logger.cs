using Prism.Logging;
using System;
using System.Diagnostics;

namespace MainApplication.Services
{
    public class Logger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            var messageToLog = string.Format(DateTime.Now.ToShortTimeString(), category.ToString().ToUpper(), message, priority);
            Debug.WriteLine(messageToLog);
        }
    }
}
