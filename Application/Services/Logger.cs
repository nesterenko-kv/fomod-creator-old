using Prism.Logging;
using System;
using System.Diagnostics;

namespace MainApplication.Services
{
    public class Logger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            var messageToLog = $"Time: {DateTime.Now.ToShortTimeString()}. Category: {category.ToString().ToUpper()}. Priority: {priority}. Message: {message}.";
            Debug.WriteLine(messageToLog);
        }
    }
}