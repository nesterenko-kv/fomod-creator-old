using FomodInfrastructure.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainApplication.Services
{
    public class UserMsgService : IUserMsgService
    {
        public event UserMsgHandler ShowMsg;

        public void Send(string msg)
        {
            ShowMsg?.Invoke(new UserMsgArgs { Msg = msg });
        }

    }
}
