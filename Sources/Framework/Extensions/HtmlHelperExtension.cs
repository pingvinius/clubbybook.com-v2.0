namespace Pingvinius.Framework.Extensions
{
    using System;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web.Mvc;

    /// <summary>
    /// The HtmlHelper extension.
    /// </summary>
    public static class HtmlHelperExtension
    {
        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <returns>The controller name.</returns>
        public static string GetControllerName(this HtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            return htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
        }

        /// <summary>
        /// Gets the name of the action.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <returns>The action name.</returns>
        public static string GetActionName(this HtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            return htmlHelper.ViewContext.RouteData.Values["action"].ToString();
        }

        /// <summary>
        /// Gets the URL helper.
        /// </summary>
        /// <param name="htmlHelper">The HTML helper.</param>
        /// <returns>The request URL helper instance.</returns>
        public static UrlHelper GetUrlHelper(this HtmlHelper htmlHelper)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            return new UrlHelper(htmlHelper.ViewContext.RequestContext);
        }

        /// <summary>
        /// Returns display HTML for multiline text.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="html">The HTML.</param>
        /// <param name="expression">The expression.</param>
        /// <returns>The HTML string in MvcHtmlString instance.</returns>
        public static MvcHtmlString MultilineDisplayFor<TModel, TValue>(this HtmlHelper<TModel> html, Expression<Func<TModel, TValue>> expression)
        {
            if (html == null)
            {
                throw new ArgumentNullException("html");
            }

            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            var model = html.Encode(metadata.Model);

            StringBuilder sb = new StringBuilder();

            sb.Append("<p>");

            bool lastCharIsNewLine = false;
            for (int i = 0; i < model.Length; i++)
            {
                var curr = model[i];

                if (curr == '\r')
                {
                    continue;
                }
                else if (curr == '\n')
                {
                    if (lastCharIsNewLine)
                    {
                        sb.Append("<br />");
                    }

                    sb.Append("</p>");

                    lastCharIsNewLine = true;
                }
                else
                {
                    if (lastCharIsNewLine)
                    {
                        sb.Append("<p>");
                    }

                    lastCharIsNewLine = false;
                    sb.Append(curr);
                }
            }

            sb.Append("</p>");

            model = sb.ToString();

            if (string.IsNullOrEmpty(model))
            {
                return MvcHtmlString.Empty;
            }

            return MvcHtmlString.Create(model);
        }
    }
}