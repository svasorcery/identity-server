using System.Threading.Tasks;

namespace Fiery.Identity.Users.AspNetCore.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}
