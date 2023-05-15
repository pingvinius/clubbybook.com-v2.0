namespace ClubbyBook.Web.UI.Extensions
{
    using System;
    using System.Web.Mvc;
    using Pingvinius.Framework.Attributes;
    using Pingvinius.Framework.Utilities;

    public enum CommonUrlTemplates
    {
        [UrlRewrite("/")]
        DefaultSite,

        [UrlRewrite("/admin")]
        AdminSite,

        [UrlRewrite("/registration")]
        Registration,

        [UrlRewrite("/login")]
        SignIn,

        [UrlRewrite("/logout")]
        SignOut,

        [UrlRewrite("/resetpassword")]
        ResetPassword,
    }

    public enum AdminUrlTemplates
    {
        [UrlRewrite("/admin/news/index")]
        News,

        [UrlRewrite("/admin/news/details/{0}")]
        ViewNews,

        [UrlRewrite("/admin/news/edit/{0}")]
        EditNews,

        [UrlRewrite("/admin/news/edit")]
        CreateNews,

        [UrlRewrite("/admin/news/delete")]
        DeleteNews,

        [UrlRewrite("/admin/book/index")]
        Books,

        [UrlRewrite("/admin/book/details/{0}")]
        ViewBook,

        [UrlRewrite("/admin/book/edit/{0}")]
        EditBook,

        [UrlRewrite("/admin/book/edit")]
        CreateBook,

        [UrlRewrite("/admin/book/delete")]
        DeleteBook,

        [UrlRewrite("/admin/author/index")]
        Authors,

        [UrlRewrite("/admin/author/details/{0}")]
        ViewAuthor,

        [UrlRewrite("/admin/author/edit/{0}")]
        EditAuthor,

        [UrlRewrite("/admin/author/edit")]
        CreateAuthor,

        [UrlRewrite("/admin/author/delete")]
        DeleteAuthor,

        [UrlRewrite("/admin/profile/index")]
        Readers,

        [UrlRewrite("/admin/profile/details/{0}")]
        ViewReader,

        [UrlRewrite("/admin/profile/edit/{0}")]
        EditReader,

        [UrlRewrite("/admin/profile/delete")]
        DeleteReader,

        [UrlRewrite("/admin/{0}/index")]
        ListTemplate,

        [UrlRewrite("/admin/{0}/details/{1}")]
        ViewTemplate,

        [UrlRewrite("/admin/{0}/edit/{1}")]
        EditTemplate,

        [UrlRewrite("/admin/{0}/edit")]
        CreateTemplate,

        [UrlRewrite("/admin/{0}/delete/{1}")]
        DeleteTemplate,

        [UrlRewrite("/admin/feedbacknotification/index")]
        FeedbackNotifications,

        [UrlRewrite("/admin/feedbacknotification/delete")]
        DeleteFeedbackNotification,

        [UrlRewrite("/admin/tools/index")]
        Tools,
    }

    public enum DefaultUrlTemplates
    {
        [UrlRewrite("/news")]
        News,

        [UrlRewrite("/viewnews/{0}")]
        ViewNews,

        [UrlRewrite("/books")]
        Books,

        [UrlRewrite("/books/{0}")]
        BooksByUser,

        [UrlRewrite("/books/{0}")]
        BooksByGenre,

        [UrlRewrite("/books/{0}")]
        BooksByAuthor,

        [UrlRewrite("/viewbook/{0}")]
        ViewBook,

        [UrlRewrite("/authors")]
        Authors,

        [UrlRewrite("/viewauthor/{0}")]
        ViewAuthor,

        [UrlRewrite("/readers")]
        Readers,

        [UrlRewrite("/view-reader/{0}")]
        ViewReader,

        [UrlRewrite("/edit-reader/{0}")]
        EditReader,

        [UrlRewrite("/edit-reader-account/{0}")]
        EditReaderAccount,

        [UrlRewrite("/useragreement")]
        UserAgreement,

        [UrlRewrite("/about")]
        About,

        [UrlRewrite("/contacts")]
        Contacts,

        [UrlRewrite("/sitemap")]
        SiteMap,

        [UrlRewrite("/messages")]
        Messages,
    }

    public static class UrlHelperExtension
    {
        #region GoTo Routine

        public static string GoTo(this UrlHelper urlHelper, CommonUrlTemplates commonUrlTemplate, params object[] values)
        {
            if (urlHelper == null)
            {
                throw new ArgumentNullException("urlHelper");
            }

            return UrlHelperExtension.GoTo<CommonUrlTemplates>(commonUrlTemplate, values);
        }

        public static string GoTo(this UrlHelper urlHelper, AdminUrlTemplates adminUrlTemplate, params object[] values)
        {
            if (urlHelper == null)
            {
                throw new ArgumentNullException("urlHelper");
            }

            return UrlHelperExtension.GoTo<AdminUrlTemplates>(adminUrlTemplate, values);
        }

        public static string GoTo(this UrlHelper urlHelper, DefaultUrlTemplates defaultUrlTemplate, params object[] values)
        {
            if (urlHelper == null)
            {
                throw new ArgumentNullException("urlHelper");
            }

            return UrlHelperExtension.GoTo<DefaultUrlTemplates>(defaultUrlTemplate, values);
        }

        private static string GoTo<TEnum>(TEnum urlTemplateValue, params object[] values)
        {
            if (Enum.IsDefined(typeof(TEnum), urlTemplateValue))
            {
                string urlTemplate = AttributeHelper.GetUrlRewriteValue(urlTemplateValue);
                string url = string.Format(urlTemplate, values);
                return url;
            }

            throw new NotSupportedException(string.Format("The following URL template '{0}' is not supported.", urlTemplateValue));
        }

        #endregion GoTo Routine
    }
}