namespace ClubbyBook.Web.UI.Areas.Admin.Extensions
{
    using System;
    using System.Web.Mvc;
    using ClubbyBook.Web.UI.Areas.Admin.Models.Common;
    using ClubbyBook.Web.UI.Extensions;
    using Pingvinius.Framework.Extensions;

    public static class HtmlHelperExtension
    {
        #region Header Builders Routine

        public static Header GetHeaderBlockForDetails(this HtmlHelper htmlHelper, int entityId, string title)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            UrlHelper urlHelper = htmlHelper.GetUrlHelper();

            var header = new Header()
            {
                Title = title,
                ShowSmallPart = false,
            };

            header.Buttons.Add(new HeaderButton()
            {
                Text = "Редактировать",
                Url = urlHelper.GoTo(AdminUrlTemplates.EditTemplate, htmlHelper.GetControllerName().ToLower(), entityId),
                IsPrimary = true
            });

            header.Buttons.Add(new HeaderButton()
            {
                Text = "К списку",
                Url = urlHelper.GoTo(AdminUrlTemplates.ListTemplate, htmlHelper.GetControllerName().ToLower())
            });

            return header;
        }

        public static Header GetHeaderBlockForEdit(this HtmlHelper htmlHelper, string title)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            return new Header()
            {
                Title = title,
                ShowSmallPart = false
            };
        }

        public static Header GetHeaderBlockForIndex(this HtmlHelper htmlHelper, string title, string smallPartTitle = "", bool showAddButton = true)
        {
            if (htmlHelper == null)
            {
                throw new ArgumentNullException("htmlHelper");
            }

            UrlHelper urlHelper = htmlHelper.GetUrlHelper();

            var header = new Header()
            {
                Title = title,
                ShowSmallPart = !string.IsNullOrWhiteSpace(smallPartTitle),
                SmallPartTitle = smallPartTitle,
            };

            if (showAddButton)
            {
                header.Buttons.Add(new HeaderButton()
                {
                    Text = "Добавить",
                    Url = urlHelper.GoTo(AdminUrlTemplates.CreateTemplate, htmlHelper.GetControllerName().ToLower())
                });
            }

            return header;
        }

        #endregion Header Builders Routine
    }
}