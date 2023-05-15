namespace ClubbyBook.Web.UI.Areas.Admin.Controllers.Base
{
    using ClubbyBook.Business.Repositories.Interfaces;
    using ClubbyBook.Common;
    using ClubbyBook.Web.UI.Controllers.Base;
    using Pingvinius.Framework.Attributes;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Security;

    [AccessPermission(RoleNames.Editor, RoleNames.Admin)]
    public abstract class AdminController : ClubbyBookController
    {
        public string CurrentTabName { get; set; }

        protected AdminController()
        {
            this.CurrentTabName = string.Empty;
        }

        protected override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            if (AccessManager.IsAuthenticated && this.ViewBag.CurrentUser == null)
            {
                this.ViewBag.CurrentUser = RepositoryFactory.Get<IUserRepository>().Get(AccessManager.CurrentIdentity.Id);
            }

            this.ViewBag.CurrentTabName = this.CurrentTabName;

            base.OnActionExecuted(filterContext);
        }
    }
}