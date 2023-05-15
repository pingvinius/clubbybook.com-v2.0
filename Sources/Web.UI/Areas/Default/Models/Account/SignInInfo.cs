namespace ClubbyBook.Web.UI.Areas.Default.Models.Account
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using Pingvinius.Framework.Mvc.Models;

    public sealed class SignInInfo : ViewModel
    {
        [DisplayName("Почта")]
        [Required(ErrorMessage = "Почта является обязательным полем.")]
        [StringLength(50, ErrorMessage = "Почта читателя не может превышать 50 символов.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Почта должна быть в формате name@domain.domain.")]
        public string Email { get; set; }

        [DisplayName("Пароль")]
        [Required(ErrorMessage = "Пароль является обязательным полем.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("Запомнить меня")]
        public bool RememberMe { get; set; }

        public SignInInfo()
        {
            this.Email = string.Empty;
            this.Password = string.Empty;
            this.RememberMe = false;
        }
    }
}