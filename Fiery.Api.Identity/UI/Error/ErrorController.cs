using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Fiery.Api.Identity.UI
{
    [Filters.SecurityHeaders]
    public class ErrorController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;

        public ErrorController(IIdentityServerInteractionService interaction)
        {
            _interaction = interaction;
        }

        [Route("Error")]
        public async Task<IActionResult> Error(string id)
        {
            var vm = new ErrorViewModel();

            // retrieve error details from identityserver
            var message = await _interaction.GetErrorContextAsync(id);
            if (message != null)
            {
                vm.Error = message;
            }

            return View(vm);
        }
    }
}
