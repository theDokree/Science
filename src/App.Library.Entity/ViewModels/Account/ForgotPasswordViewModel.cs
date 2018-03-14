using System.ComponentModel.DataAnnotations;

namespace App.Library.Entity.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
