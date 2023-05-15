namespace Pingvinius.Framework.Attributes
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using Pingvinius.Framework.Security;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AccessPermissionAttribute : AuthorizeAttribute
    {
        public new string[] Roles { get; set; }

        public AccessPermissionAttribute(params string[] roles)
        {
            if (roles == null || roles.Length == 0)
            {
                throw new ArgumentNullException("roles");
            }

            this.Roles = roles;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("httpContext");
            }

            if (!httpContext.User.Identity.IsAuthenticated || AccessManager.CurrentIdentity == null)
            {
                return false;
            }

            return this.Roles.Any(r => AccessManager.HasRole(r));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}