namespace ClubbyBook.Web.UI
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using ClubbyBook.Business.Repositories.Resolving;
    using ClubbyBook.Business.Security;
    using ClubbyBook.Common;
    using ClubbyBook.Data.DbContext;
    using ClubbyBook.Web.UI.Areas.Admin;
    using ClubbyBook.Web.UI.Areas.Default;
    using ClubbyBook.Web.UI.Mapping;
    using NLog;
    using Pingvinius.Framework.DbContext;
    using Pingvinius.Framework.Mapping;
    using Pingvinius.Framework.Mvc.Overrides;
    using Pingvinius.Framework.Repositories;
    using Pingvinius.Framework.Security;

    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();

        #region Application Routine

        protected void Application_Start()
        {
            MvcApplication.RegisterFilters();
            MvcApplication.RegisterRoutes();
            MvcApplication.RegisterBundles();

            // Fix JsonValueProvider to allow big JSON data
            ValueProviderFactories.Factories.Remove(ValueProviderFactories.Factories.OfType<JsonValueProviderFactory>().FirstOrDefault());
            ValueProviderFactories.Factories.Add(new FixedJsonValueProviderFactory() { MaxJsonLength = 4194304 }); // 4Mb to be sure

            this.InitializeApplication();

            this.logger.Info("ClubbyBook has been started.");
        }

        protected void Application_End()
        {
            DbContextManager.Destroy();

            this.logger.Info("ClubbyBook has been stopped.");
        }

        #endregion Application Routine

        #region Request Routine

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            DbContextManager.InitCurrent();
        }

        protected void Application_EndRequest(object sender, EventArgs e)
        {
            DbContextManager.DestroyCurrent();
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            AccessManager.FillAuthenticateRequest();
        }

        #endregion Request Routine

        #region Error Routine

        protected void Application_Error(object sender, EventArgs e)
        {
            this.logger.Error("The {0} error(s) have occurred while performing current operation.", Context.AllErrors.Length);

            foreach (var exception in base.Context.AllErrors)
            {
                this.logger.Error(exception.ToString());
            }

            this.Server.ClearError();
        }

        protected void Global_Error(Object sender, EventArgs e)
        {
            this.logger.Error("A global error has occurred: {0}.", this.Server.GetLastError());
        }

        #endregion Error Routine

        #region Session Routine

        protected void Session_Start(object sender, EventArgs e)
        {
        }

        protected void Session_End(object sender, EventArgs e)
        {
        }

        #endregion Session Routine

        #region Routes Routine

        private static void RegisterRoutes()
        {
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            RouteTable.Routes.IgnoreRoute("{*robotstxt}", new { robotstxt = @"(.*/)?robots.txt(/.*)?" });
            RouteTable.Routes.IgnoreRoute("{*favicon}", new { favicon = @"(.*/)?favicon.ico(/.*)?" });
            RouteTable.Routes.IgnoreRoute("{*staticfiles}", new { staticfiles = @".*\.(css|js|gif|png|jpg)(/.*)?" });

            // Shared
            RouteTable.Routes.MapRoute(
                name: "Shared",
                url: "Shared/{controller}/{action}",
                defaults: new { },
                namespaces: new[] { "ClubbyBook.Web.UI.Controllers" }
            );

            var adminArea = new AdminAreaRegistration();
            var adminAreaContext = new AreaRegistrationContext(adminArea.AreaName, RouteTable.Routes);
            adminArea.RegisterArea(adminAreaContext);

            var defaultArea = new DefaultAreaRegistration();
            var defaultAreaContext = new AreaRegistrationContext(defaultArea.AreaName, RouteTable.Routes);
            defaultArea.RegisterArea(defaultAreaContext);
        }

        #endregion Routes Routine

        #region Filters Routine

        private static void RegisterFilters()
        {
            GlobalFilters.Filters.Add(new HandleErrorAttribute());
        }

        #endregion Filters Routine

        #region Bundles Routine

        /// <summary>
        /// Registers the bundles.
        /// </summary>
        private static void RegisterBundles()
        {
            // Scripts
            BundleTable.Bundles.Add(new ScriptBundle("~/Bundles/js/admin").Include(
                "~/Scripts/jquery-2.1.3.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootstrap-confirm.js",
                "~/Scripts/app/common/cb-common-constants.js",
                "~/Scripts/app/common/cb-common-ui-constants.js",
                "~/Scripts/app/common/cb-common-basic.js",
                "~/Scripts/app/common/cb-common-lighttooltip.js",
                "~/Scripts/app/common/cb-common-api-proxy.js",
                "~/Scripts/app/common/cb-common-helper.js",
                "~/Scripts/app/admin/cb-admin-ui-constants.js",
                "~/Scripts/app/admin/cb-admin-api-proxy.js",
                "~/Scripts/app/admin/cb-admin-helper.js"));

            BundleTable.Bundles.Add(new ScriptBundle("~/Bundles/js/default").Include(
                "~/Scripts/jquery-2.1.3.js",
                "~/Scripts/bootstrap-modal.js",
                "~/Scripts/bootstrap-confirm.js",
                "~/Scripts/app/common/cb-common-constants.js",
                "~/Scripts/app/common/cb-common-ui-constants.js",
                "~/Scripts/app/common/cb-common-basic.js",
                "~/Scripts/app/common/cb-common-lighttooltip.js",
                "~/Scripts/app/common/cb-common-api-proxy.js",
                "~/Scripts/app/common/cb-common-helper.js",
                "~/Scripts/app/default/cb-default-feedback.js"));

            BundleTable.Bundles.Add(new ScriptBundle("~/Bundles/js/admin-edit-profile").Include(
                "~/Scripts/jquery.color.js",
                "~/Scripts/jquery.jcrop.js",
                "~/Scripts/app/common/cb-common-image-chooser.js",
                "~/Scripts/app/common/cb-common-city-chooser.js",
                "~/Scripts/app/admin/cb-admin-role-chooser.js"));

            BundleTable.Bundles.Add(new ScriptBundle("~/Bundles/js/admin-edit-book").Include(
                "~/Scripts/jquery.color.js",
                "~/Scripts/jquery.jcrop.js",
                "~/Scripts/jquery.tokeninput.js",
                "~/Scripts/app/common/cb-common-image-chooser.js",
                "~/Scripts/app/admin/cb-admin-author-list-chooser.js"));

            BundleTable.Bundles.Add(new ScriptBundle("~/Bundles/js/admin-edit-author").Include(
                "~/Scripts/jquery.color.js",
                "~/Scripts/jquery.jcrop.js",
                "~/Scripts/app/common/cb-common-image-chooser.js"));

            BundleTable.Bundles.Add(new ScriptBundle("~/Bundles/js/default-edit-profile").Include(
                "~/Scripts/jquery.color.js",
                "~/Scripts/jquery.jcrop.js",
                "~/Scripts/app/common/cb-common-image-chooser.js",
                "~/Scripts/app/common/cb-common-city-chooser.js"));

            // Styles
            BundleTable.Bundles.Add(new StyleBundle("~/Bundles/css/admin").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/cb-common.css",
                "~/Content/cb-common-lighttooltip.css",
                "~/Content/cb-admin.css"));

            BundleTable.Bundles.Add(new StyleBundle("~/Bundles/css/default").Include(
                "~/Content/bootstrap-modal.css",
                "~/Content/cb-common.css",
                "~/Content/cb-common-lighttooltip.css",
                "~/Content/cb-default.css"));

            BundleTable.Bundles.Add(new StyleBundle("~/Bundles/css/admin-edit-profile").Include(
                "~/Content/jcrop.css"));

            BundleTable.Bundles.Add(new StyleBundle("~/Bundles/css/admin-edit-book").Include(
                "~/Content/jcrop.css",
                "~/Content/token-input.css"));

            BundleTable.Bundles.Add(new StyleBundle("~/Bundles/css/admin-edit-author").Include(
                "~/Content/jcrop.css"));

            BundleTable.Bundles.Add(new StyleBundle("~/Bundles/css/default-edit-profile").Include(
                "~/Content/jcrop.css"));

            // Disable minification depends on web.config setting 'EnableMinification'
            if (!Settings.EnableMinification)
            {
                foreach (var bundle in BundleTable.Bundles)
                {
                    bundle.Transforms.Clear();
                }
            }

            BundleTable.EnableOptimizations = true;
        }

        #endregion Bundles Routine

        #region Application Initialization Routine

        private void InitializeApplication()
        {
            try
            {
                AccessManager.Initialize(new AccessManagerProvider());

                DbContextManager.Initialize(new WebDbContextStore(), new DbContextFactory());

                RepositoryFactory.Initialize(new DefaultRepositoryResolver());

                Mapper.Initialize(new ClubbyBookMapper());
            }
            catch (Exception ex)
            {
                this.logger.Error("While starting ClubbyBook application unknown exception has occurred: {0}.", ex);
                throw;
            }
        }

        #endregion Application Initialization Routine
    }
}