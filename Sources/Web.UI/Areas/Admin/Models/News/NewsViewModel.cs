namespace ClubbyBook.Web.UI.Areas.Admin.Models.News
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ClubbyBook.Web.UI.Areas.Admin.Models.Base;

    public sealed class NewsViewModel : BaseEntityViewModel<ClubbyBook.Data.Models.News>
    {
        [Required(ErrorMessage = "Заголовок является обьязательным полем.")]
        [StringLength(128, ErrorMessage = "Длинна заголовка новости не может превышать 128 символов.")]
        [DisplayName("Заголовок")]
        [DataType(DataType.Text)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Тело новости является обязательным полем.")]
        [DisplayName("Тело")]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        [DisplayName("Изменена")]
        [DataType(DataType.DateTime)]
        public DateTime LastModifiedDate { get; set; }

        [DisplayName("Создана")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Дружественный URL")]
        [DataType(DataType.Text)]
        public string UrlRewrite { get; set; }
    }
}