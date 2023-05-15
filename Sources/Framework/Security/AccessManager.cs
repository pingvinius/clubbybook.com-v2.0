namespace Pingvinius.Framework.Security
{
    using System.Linq;
    using NLog;

    public static class AccessManager
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private static IAccessManagerProvider provider;

        public static bool IsAuthenticated
        {
            get
            {
                return AccessManager.CurrentIdentity != null;
            }
        }

        public static IUserIdentity CurrentIdentity
        {
            get
            {
                return AccessManager.Provider.GetCurrentIdentity();
            }
        }

        private static IAccessManagerProvider Provider
        {
            get
            {
                if (AccessManager.provider == null)
                {
                    throw new AccessManagerIsNotInitializedException();
                }

                return AccessManager.provider;
            }
        }

        public static void Initialize(IAccessManagerProvider provider)
        {
            AccessManager.provider = provider;

            AccessManager.logger.Info("The access manager has been successfully initialized.");
        }

        #region Main Methods Routine

        public static void FillAuthenticateRequest()
        {
            AccessManager.Provider.FillAuthenticateRequest();
        }

        public static bool SignIn(string email, string password, bool rememberMe)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            return AccessManager.Provider.SignIn(email, password, rememberMe);
        }

        public static void SignOut()
        {
            if (AccessManager.IsAuthenticated)
            {
                AccessManager.Provider.SignOut();
            }
        }

        public static bool HasRole(string roleIdentity)
        {
            if (AccessManager.IsAuthenticated)
            {
                return AccessManager.CurrentIdentity.Roles.Contains(roleIdentity);
            }
            return false;
        }

        #endregion Main Methods Routine
    }
}