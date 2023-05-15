namespace ClubbyBook.Business.Security
{
    using System;
    using System.Linq;
    using System.Security.Principal;
    using System.Web;
    using System.Web.Security;
    using ClubbyBook.Business.Exceptions;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Business.Repositories.SelectCriteria;
    using ClubbyBook.Data.Models;
    using NLog;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Security;
    using Pingvinius.Framework.Utilities;

    public sealed class AccessManagerProvider : IAccessManagerProvider
    {
        #region Constants

        private const string CurrentUserKey = "current_user_key";

        #endregion Constants

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region IAccessManagerProvider Members

        public IUserIdentity GetCurrentIdentity()
        {
            if (HttpContext.Current != null && HttpContext.Current.User != null && HttpContext.Current.User.Identity != null && HttpContext.Current.User.Identity.IsAuthenticated)
            {
                if (HttpContext.Current.Items.Contains(AccessManagerProvider.CurrentUserKey))
                {
                    return (UserIdentity)HttpContext.Current.Items[AccessManagerProvider.CurrentUserKey];
                }

                AccessManagerProvider.logger.Debug("Fetching current identity...");

                int userId;
                if (int.TryParse(HttpContext.Current.User.Identity.Name, out userId))
                {
                    var user = RepositoryFactory.Get<IUserRepository>().Get(userId);
                    if (user != null)
                    {
                        if (user.IsDeleted)
                        {
                            throw new UserIsDeletedException(user.EMail);
                        }

                        var userRoles = RepositoryFactory.Get<IRoleRepository>().Select(new RoleSelectCriteria.UserRoles(user.Id).ToList()).Select(r => r.Name).ToArray();
                        HttpContext.Current.Items.Add(AccessManagerProvider.CurrentUserKey, new UserIdentity(user.Id, user.EMail, userRoles));

                        return (UserIdentity)HttpContext.Current.Items[AccessManagerProvider.CurrentUserKey];
                    }
                }
            }

            return null;
        }

        public void FillAuthenticateRequest()
        {
            if (HttpContext.Current.User != null)
            {
                if (HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    if (HttpContext.Current.User.Identity.GetType() == typeof(FormsIdentity))
                    {
                        FormsIdentity fi = (FormsIdentity)HttpContext.Current.User.Identity;
                        FormsAuthenticationTicket fat = fi.Ticket;

                        string[] roles = fat.UserData.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
                        HttpContext.Current.User = new GenericPrincipal(fi, roles);

                        AccessManagerProvider.logger.Trace("The authorized request is started.");
                    }
                }
            }
        }

        public bool SignIn(string email, string password, bool rememberMe)
        {
            AccessManagerProvider.logger.Trace(string.Format("The user {0} is trying to sign in.", email));

            User user = RepositoryFactory.Get<IUserRepository>().Select(new UserSelectCriteria.ByEmail(email).ToList()).FirstOrDefault();
            if (user == null)
            {
                return false;
            }

            if (user.IsDeleted)
            {
                throw new UserIsDeletedException(email);
            }

            if (user.Password != Md5Helper.Calculate(password))
            {
                return false;
            }

            var userRoles = RepositoryFactory.Get<IRoleRepository>().Select(new RoleSelectCriteria.UserRoles(user.Id).ToList()).Select(r => r.Name).ToArray();

            FormsAuthentication.Initialize();

            DateTime dtNow = DateTimeHelper.Now;

            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(
                1,
                user.Id.ToString(),
                dtNow,
                rememberMe ? dtNow.AddMonths(2) : dtNow.AddMinutes(FormsAuthentication.Timeout.TotalMinutes),
                rememberMe,
                string.Join(",", userRoles),
                FormsAuthentication.FormsCookiePath);

            string encTicket = FormsAuthentication.Encrypt(ticket);

            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            cookie.HttpOnly = !FormsAuthentication.RequireSSL;
            cookie.Secure = FormsAuthentication.RequireSSL;
            cookie.Path = FormsAuthentication.FormsCookiePath;
            if (rememberMe)
            {
                cookie.Expires = ticket.Expiration;
            }

            if (!string.IsNullOrEmpty(FormsAuthentication.CookieDomain))
            {
                cookie.Domain = FormsAuthentication.CookieDomain;
            }

            HttpContext.Current.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
            HttpContext.Current.Response.Cookies.Add(cookie);

            RepositoryFactory.Get<IUserRepository>().UpdateActivity(user);

            AccessManagerProvider.logger.Trace(string.Format("The user {0} has successfully signed in.", email));

            return true;
        }

        public void SignOut()
        {
            if (AccessManager.IsAuthenticated)
            {
                var currentUser = RepositoryFactory.Get<IUserRepository>().Get(AccessManager.CurrentIdentity.Id);

                RepositoryFactory.Get<IUserRepository>().UpdateActivity(currentUser);

                FormsAuthentication.SignOut();

                AccessManagerProvider.logger.Trace(string.Format("The user {0} has successfully signed out.", currentUser.EMail));
            }
        }

        #endregion IAccessManagerProvider Members
    }
}