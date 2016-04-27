using MainApplication.Properties;
using Prism.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Services
{
    public class Logger : ILoggerFacade
    {
        public void Log(string message, Category category, Priority priority)
        {
            string messageToLog = String.Format(DateTime.Now.ToShortTimeString(), category.ToString().ToUpper(), message, priority);

            Debug.WriteLine(messageToLog);
        }
    }
}
