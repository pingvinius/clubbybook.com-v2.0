namespace ClubbyBook.Web.UI.Areas.Default.Models.News
{
    using System;
    using ClubbyBook.Web.UI.Areas.Default.Models.Base;

    public sealed class NewsViewModel : BaseEntityViewModel<ClubbyBook.Data.Models.News>
    {
        public string Title { get; set; }

        public string Message { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UrlRewrite { get; set; }
    }
}