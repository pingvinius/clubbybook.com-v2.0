namespace ClubbyBook.Web.UI.Areas.Default.Controllers.Base
{
    using System.Web.Mvc;
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Web.UI.Controllers.Base;
    using ClubbyBook.Web.UI.Exceptions;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Security;

    public abstract class DefaultController : ClubbyBookController
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (AccessManager.IsAuthenticated && this.ViewBag.CurrentUser == null)
            {
                this.ViewBag.CurrentUser = RepositoryFactory.Get<IUserRepository>().Get(AccessManager.CurrentIdentity.Id);
            }

            base.OnActionExecuted(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);

            if (filterContext.Exception is EntityIsNotFoundException)
            {
                filterContext.Result = this.ShowPageNotFoundError();
                filterContext.ExceptionHandled = true;
            }
            else if (filterContext.Exception is AccessDenyException)
            {
                filterContext.Result = this.ShowAccessDenyError();
                filterContext.ExceptionHandled = true;
            }
        }

        protected ActionResult ShowAccessDenyError()
        {
            return this.View("ErrorAccessDeny");
        }

        protected ActionResult ShowPageNotFoundError()
        {
            return this.View("ErrorPageNotFound");
        }
    }
}