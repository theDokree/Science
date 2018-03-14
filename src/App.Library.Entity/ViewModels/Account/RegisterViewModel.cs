using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace App.Library.Entity.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required (ErrorMessage = "Поле '{0}' должно быть заполнено.")]
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Поле '{0}' должно быть заполнено.")]
        [StringLength(100, ErrorMessage = "{0} должно быть не менее {2} и не более {1} символов.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Подтверждения пароля")]
        [Compare("Password", ErrorMessage = "Пароль и подтверждение пароля не совпадают.")]
        public string ConfirmPassword { get; set; }
        [Display(Name = "Подразделение")]
        public string Company { get; set; }
        [Display(Name = "Провайдер")]
        public string ExternalProvider { get; set; }
        [Display(Name = "ФИО")]
        public string DisplayName { get; set; }
        [Display(Name = "Имя")]
        public string FirstName { get; set; }
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }
        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }
        [Display(Name = "Роли")]
        public IList<string> Roles { get; set; }

        public RegisterViewModel()
        {
            this.Roles = new List<string>();
        }
    }
}
