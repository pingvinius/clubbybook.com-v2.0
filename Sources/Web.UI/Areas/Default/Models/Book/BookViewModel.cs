namespace ClubbyBook.Web.UI.Areas.Default.Models.Book
{
    using System.ComponentModel.DataAnnotations;
    using ClubbyBook.Web.UI.Areas.Default.Models.Base;
    using ClubbyBook.Web.UI.Models;

    public sealed class BookViewModel : BaseEntityViewModel<ClubbyBook.Data.Models.Book>
    {
        public string Title { get; set; }

        public string OriginalTitle { get; set; }

        [DataType("BookAuthorList")]
        public BookAuthorListViewModel AuthorList { get; set; }

        [DataType("BookGenreList")]
        public BookGenreListViewModel GenreList { get; set; }

        public string Description { get; set; }

        public bool Collection { get; set; }

        public bool ContainsInOtherUserLibraryFromSameCity { get; set; }

        public bool ContainsInUserLibrary { get; set; }

        [DataType("Image")]
        public ImageViewModel Image { get; set; }

        public string UrlRewrite { get; set; }
    }
}