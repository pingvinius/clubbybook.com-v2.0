namespace ClubbyBook.Web.UI.Areas.Default.Models.Author
{
    using System.ComponentModel.DataAnnotations;
    using ClubbyBook.Web.UI.Areas.Default.Models.Base;
    using ClubbyBook.Web.UI.Models;

    public sealed class AuthorViewModel : BaseEntityViewModel<ClubbyBook.Data.Models.Author>
    {
        public string FullName { get; set; }

        public string LifeYears { get; set; }

        [DataType("AuthorType")]
        public AuthorTypeViewModel Type { get; set; }

        [DataType("Gender")]
        public GenderViewModel Gender { get; set; }

        public int TotalBookCount { get; set; }

        public string ShortDescription { get; set; }

        public string Biography { get; set; }

        [DataType("Image")]
        public ImageViewModel Image { get; set; }

        public string UrlRewrite { get; set; }
    }
}