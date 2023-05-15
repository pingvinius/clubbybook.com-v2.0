namespace ClubbyBook.Web.UI.Areas.Default.Controllers
{
    using System.Web.Mvc;
    using ClubbyBook.Web.UI.Areas.Default.Controllers.Base;

    public sealed class HomeController : DefaultController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}