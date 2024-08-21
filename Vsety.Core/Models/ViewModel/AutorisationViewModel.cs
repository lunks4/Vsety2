using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vsety.Core.Models.ViewModel
{
    public class AutorisationViewModel
    {
        [Required(ErrorMessage = "Введите почту")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Неверный e-mail")]
        public string Login { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
