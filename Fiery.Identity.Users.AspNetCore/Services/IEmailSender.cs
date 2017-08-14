using System.Threading.Tasks;

namespace Fiery.Identity.Users.AspNetCore.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}
