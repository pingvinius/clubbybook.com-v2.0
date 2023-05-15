namespace ClubbyBook.Web.UI.Areas.Admin
{
    using System;
    using System.Web.Mvc;

    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            context.MapRoute(
                name: "Admin_default",
                url: "admin/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "ClubbyBook.Web.UI.Areas.Admin.Controllers" }
            );
        }
    }
}