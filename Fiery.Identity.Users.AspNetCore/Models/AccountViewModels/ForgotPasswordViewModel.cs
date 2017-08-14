using System.ComponentModel.DataAnnotations;

namespace Fiery.Identity.Users.AspNetCore.Models.AccountViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
