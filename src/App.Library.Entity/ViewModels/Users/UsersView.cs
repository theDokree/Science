using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace App.Library.Entity.ViewModels.News
{
    public class UsersView
    {
        [Display(Name = "ИД")]
        public string Id { get; set; }
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
        [Display(Name = "Имя пользоватееля")]
        public string UserName { get; set; }
        [Display(Name = "Роли")]
        public List<string> Roles { get; set; }

    }

    public class RoleView
    {
        public string Name { get; set; } 
    }
}
