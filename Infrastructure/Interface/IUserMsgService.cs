using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FomodInfrastructure.Interface
{
    public interface IUserMsgService
    {
        void Send(string Msg);


        /// <summary>
        /// Событие  выполняется при отправке сервисом сообщений
        /// </summary>
        event UserMsgHandler ShowMsg;
    }


    public  delegate void UserMsgHandler(UserMsgArgs Args);

    public class UserMsgArgs
    {
        public string Msg { get; set; }
    }
}
