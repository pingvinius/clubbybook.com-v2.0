namespace Pingvinius.Framework.Extensions
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    /// <summary>
    /// The UrlHelper extensions.
    /// </summary>
    public static class UrlHelperExtension
    {
        /// <summary>
        /// Replaces the parameter in current URL.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="parameterName">Name of the parameter.</param>
        /// <param name="newValue">The new value.</param>
        /// <returns>New replaced URL string.</returns>
        /// <exception cref="System.ArgumentNullException">
        /// parameterName
        /// or
        /// newValue
        /// </exception>
        public static string ReplaceParameter(this UrlHelper urlHelper, string parameterName, string newValue)
        {
            if (urlHelper == null)
            {
                throw new ArgumentNullException("urlHelper");
            }

            if (string.IsNullOrWhiteSpace(parameterName))
            {
                throw new ArgumentNullException("parameterName");
            }

            if (string.IsNullOrWhiteSpace(newValue))
            {
                throw new ArgumentNullException("newValue");
            }

            HttpRequestBase request = urlHelper.RequestContext.HttpContext.Request;
            var absoluteUrl = request.Url.AbsoluteUri.Split('?').FirstOrDefault() ?? string.Empty;
            var qs = HttpUtility.ParseQueryString(request.Url.Query);
            qs[parameterName] = newValue;
            return new Uri(string.Format("{0}?{1}", absoluteUrl, qs)).ToString();
        }

        /// <summary>
        /// Gets the previous URL.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <returns>The URL string.</returns>
        public static string GetPreviousUrl(this UrlHelper urlHelper)
        {
            if (urlHelper == null)
            {
                throw new ArgumentNullException("urlHelper");
            }

            return urlHelper.RequestContext.HttpContext.Request.UrlReferrer.ToString();
        }
    }
}