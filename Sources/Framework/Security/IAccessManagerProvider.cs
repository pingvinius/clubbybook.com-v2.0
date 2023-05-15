namespace Pingvinius.Framework.Security
{
    public interface IAccessManagerProvider
    {
        IUserIdentity GetCurrentIdentity();

        void FillAuthenticateRequest();

        bool SignIn(string email, string password, bool rememberMe);

        void SignOut();
    }
}