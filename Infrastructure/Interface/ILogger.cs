using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FomodInfrastructure.Interface
{
    public interface ILogger
    {
        void Log(string Msg);
        void LogCreate(object obj);
        void LogDisposable(object obj);
      
    }
}
