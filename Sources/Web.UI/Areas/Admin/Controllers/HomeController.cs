namespace ClubbyBook.Web.UI.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using ClubbyBook.Web.UI.Areas.Admin.Controllers.Base;

    public sealed class HomeController : AdminController
    {
        public HomeController()
        {
            this.CurrentTabName = "tab-home";
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}