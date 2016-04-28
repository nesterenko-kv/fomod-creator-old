using FomodInfrastructure.Interface;

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
