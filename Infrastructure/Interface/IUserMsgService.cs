namespace FomodInfrastructure.Interface
{
    public interface IUserMsgService
    {
        void Send(string msg);

        /// <summary>
        /// Событие  выполняется при отправке сервисом сообщений
        /// </summary>
        event UserMsgHandler ShowMsg;
    }


    public delegate void UserMsgHandler(UserMsgArgs args);

    public class UserMsgArgs
    {
        public string Msg { get; set; }
    }
}
