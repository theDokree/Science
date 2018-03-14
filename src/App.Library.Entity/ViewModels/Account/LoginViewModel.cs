using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Library.Entity.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Поле адрес электронной почты является обязательным.")]
        [EmailAddress(ErrorMessage = "Поле адрес электронной почты не является корректным.")]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Поле пароль является обязательным.")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Display(Name = "Не выходить из системы")]
        public bool RememberMe { get; set; }
    }
}
