namespace ClubbyBook.Web.UI.Areas.Admin.Models.Author
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ClubbyBook.Web.UI.Areas.Admin.Models.Base;
    using ClubbyBook.Web.UI.Models;
    using Pingvinius.Framework.Attributes.DataAnnotations;

    public sealed class AuthorViewModel : BaseEntityViewModel<ClubbyBook.Data.Models.Author>
    {
        [Required(ErrorMessage = "Полное имя является обьязательным полем.")]
        [StringLength(255, ErrorMessage = "Длинна полного имени автора не может превышать 255 символов.")]
        [DisplayName("Полное имя")]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [DisplayName("Год рожденья")]
        [DataType(DataType.Text)]
        [Year(ErrorMessage = "Значение поля год рожденья должно быть валидным числом для года.")]
        public string BirthdayYear { get; set; }

        [DisplayName("Год смерти")]
        [DataType(DataType.Text)]
        [Year(ErrorMessage = "Значение поля год смерти должно быть валидным числом для года.")]
        public string DeathYear { get; set; }

        [DisplayName("Годы жизни")]
        [DataType(DataType.Text)]
        public string LifeYearsString { get; set; }

        [Required(ErrorMessage = "Краткое описание является обьязательным полем.")]
        [DisplayName("Краткое описание")]
        [DataType(DataType.MultilineText)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Биография является обьязательным полем.")]
        [DisplayName("Биография")]
        [DataType(DataType.Text)]
        public string Biography { get; set; }

        [DisplayName("Фото")]
        [DataType("Image")]
        public ImageViewModel Image { get; set; }

        [DisplayName("Тип")]
        [DataType("AuthorType")]
        public AuthorTypeViewModel Type { get; set; }

        [DisplayName("Пол")]
        [DataType("Gender")]
        public GenderViewModel Gender { get; set; }

        [DisplayName("Изменен")]
        [DataType(DataType.DateTime)]
        public DateTime LastModifiedDate { get; set; }

        [DisplayName("Добавлен")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Дружественный URL")]
        [DataType(DataType.Text)]
        public string UrlRewrite { get; set; }
    }
}