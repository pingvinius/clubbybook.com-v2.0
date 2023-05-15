namespace Pingvinius.Framework.Mvc.Controllers
{
    using System;
    using System.Web;
    using System.Web.Mvc;
    using NLog;
    using Pingvinius.Framework.Utilities;

    public abstract class AbstractController : Controller
    {
        #region Constants

        protected const string DefaultJsonContentType = "application/json; charset=utf-8";

        #endregion Constants

        #region Fields

        private Logger logger;

        #endregion Fields

        #region Properties

        public Logger Logger
        {
            get
            {
                if (this.logger == null)
                {
                    this.logger = LogManager.GetLogger(this.GetType().Name);
                }
                return this.logger;
            }
        }

        #endregion Properties

        #region Parsing Methods Routine

        protected string GetStringParameter(string key)
        {
            return HttpUtility.UrlDecode(this.Request.Params[key]);
        }

        protected int? GetIntParameter(string key)
        {
            return ParsingHelper.ToNullableInt(this.Request.Params[key]);
        }

        #endregion Parsing Methods Routine

        protected override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }
            base.OnException(filterContext);

            if (filterContext.Exception != null)
            {
                this.Logger.ErrorException(filterContext.Exception.Message, filterContext.Exception);
            }
        }
    }
}