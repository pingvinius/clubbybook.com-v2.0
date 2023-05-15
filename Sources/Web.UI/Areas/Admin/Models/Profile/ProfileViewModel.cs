namespace ClubbyBook.Web.UI.Areas.Admin.Models.Profile
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using ClubbyBook.Web.UI.Areas.Admin.Models.Base;
    using ClubbyBook.Web.UI.Models;

    public sealed class ProfileViewModel : BaseEntityViewModel<ClubbyBook.Data.Models.Profile>
    {
        [StringLength(50, ErrorMessage = "Имя читателя не может превышать 50 символов.")]
        [DisplayName("Имя")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [StringLength(50, ErrorMessage = "Фамилия читателя не может превышать 50 символов.")]
        [DisplayName("Фамилия")]
        [DataType(DataType.Text)]
        public string Surname { get; set; }

        [StringLength(50, ErrorMessage = "Псевдоним читателя не может превышать 50 символов.")]
        [DisplayName("Псевдоним")]
        [DataType(DataType.Text)]
        public string Nickname { get; set; }

        [DisplayName("Полное имя")]
        [DataType(DataType.Text)]
        public string FullName { get; set; }

        [DisplayName("День рожденье")]
        [DataType(DataType.DateTime)]
        public DateTime? Birthday { get; set; }

        [Required(ErrorMessage = "Почта читателя является обязательным полем.")]
        [StringLength(50, ErrorMessage = "Почта читателя не может превышать 50 символов.")]
        [DisplayName("Почта")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Почта должна быть в формате name@domain.domain.")]
        public string Email { get; set; }

        [DisplayName("Пол")]
        [DataType("Gender")]
        public GenderViewModel Gender { get; set; }

        [DisplayName("Город")]
        [DataType("City")]
        public CityViewModel City { get; set; }

        [DisplayName("Роли")]
        [DataType("ProfileRoles")]
        public ProfileRolesViewModel Roles { get; set; }

        [DisplayName("Зарегистрирован")]
        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Последняя активность")]
        [DataType(DataType.DateTime)]
        public DateTime LastAccessDate { get; set; }

        [DisplayName("Дружественный URL")]
        [DataType(DataType.Text)]
        public string UrlRewrite { get; set; }

        [DisplayName("Аватарка")]
        [DataType("Image")]
        public ImageViewModel Image { get; set; }
    }
}