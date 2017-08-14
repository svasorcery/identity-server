using System.ComponentModel.DataAnnotations;

namespace Fiery.Identity.Users.AspNetCore.Models.AccountViewModels
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
