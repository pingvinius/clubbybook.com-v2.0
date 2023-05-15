namespace ClubbyBook.Web.UI.Areas.Default.Controllers
{
    using System.Web.Mvc;
    using ClubbyBook.Web.UI.Areas.Default.Controllers.Base;

    public sealed class StaticPagesController : DefaultController
    {
        public ActionResult UserAgreement()
        {
            return this.View();
        }

        public ActionResult About()
        {
            return this.View();
        }

        public ActionResult Contacts()
        {
            return this.View();
        }

        public ActionResult SiteMap()
        {
            return this.View();
        }
    }
}