using System.ComponentModel.DataAnnotations;

namespace App.Library.Entity.ViewModels.Account
{
    public class ExternalLoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
