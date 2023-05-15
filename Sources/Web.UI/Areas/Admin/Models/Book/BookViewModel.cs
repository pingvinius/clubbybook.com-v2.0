namespace ClubbyBook.Web.UI.Areas.Admin.Models.Book
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ClubbyBook.Web.UI.Areas.Admin.Models.Base;
    using ClubbyBook.Web.UI.Models;

    public sealed class BookViewModel : BaseEntityViewModel<ClubbyBook.Data.Models.Book>
    {
        [Required(ErrorMessage = "Заголовок является обьязательным полем.")]
        [StringLength(512, ErrorMessage = "Длинна заголовка книги не может превышать 512 символов.")]
        [DisplayName("Заголовок")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [StringLength(512, ErrorMessage = "Длинна оригинального заголовка книги не может превышать 512 символов.")]
        [DisplayName("Название оригинала")]
        [DataType(DataType.Text)]
        public string OriginalTitle { get; set; }

        [Required(ErrorMessage = "Описание является обьязательным полем.")]
        [DisplayName("Описание")]
        [DataType(DataType.Text)]
        public string Description { get; set; }

        [DisplayName("Авторы")]
        [DataType("BookAuthorList")]
        public BookAuthorListViewModel AuthorList { get; set; }

        [DisplayName("Жанр")]
        [DataType("BookGenre")]
        public BookGenreViewModel Genre { get; set; }

        [DisplayName("Обложка")]
        [DataType("Image")]
        public ImageViewModel Image { get; set; }

        [DisplayName("Изменена")]
        [DataType(DataType.DateTime)]
        public DateTime LastModifiedDate { get; set; }

        [DisplayName("Добавлена")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Книга проверена")]
        public bool Confirmed { get; set; }

        [DisplayName("Сборник")]
        public bool Collection { get; set; }

        [DisplayName("Дружественный URL")]
        [DataType(DataType.Text)]
        public string UrlRewrite { get; set; }
    }
}