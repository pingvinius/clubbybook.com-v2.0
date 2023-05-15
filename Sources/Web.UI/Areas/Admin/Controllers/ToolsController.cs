namespace ClubbyBook.Web.UI.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using ClubbyBook.Web.UI.Areas.Admin.Controllers.Base;

    public sealed class ToolsController : AdminController
    {
        public ToolsController()
        {
            this.CurrentTabName = "tab-tools";
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}