using System.ComponentModel.DataAnnotations;

namespace Vsety.Core.Models.ViewModel
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage ="Введите почту")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage ="Неверный e-mail")]
        public string Login { get; set; }
        [Required(ErrorMessage ="Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Повторите пароль")]
        [DataType(DataType.Password)]
        [Display(Name = "Потдвердите пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordCheck { get; set; }
    }
}
