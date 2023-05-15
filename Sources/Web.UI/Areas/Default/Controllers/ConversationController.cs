namespace ClubbyBook.Web.UI.Areas.Default.Controllers
{
    using System.Web.Mvc;
    using ClubbyBook.Common;
    using ClubbyBook.Web.UI.Areas.Default.Controllers.Base;
    using Pingvinius.Framework.Attributes;

    [AccessPermission(RoleNames.Account, RoleNames.Editor, RoleNames.Admin)]
    public sealed class ConversationController : DefaultController
    {
        public ActionResult Index()
        {
            return this.View();
        }
    }
}