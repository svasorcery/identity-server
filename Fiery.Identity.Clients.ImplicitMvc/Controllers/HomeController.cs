using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Fiery.Identity.Clients.ImplicitMvc.Models;

namespace Fiery.Identity.Clients.ImplicitMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly OpenIdConnectAuthenticationOptions _oidcOptions;

        public HomeController(IOptions<OpenIdConnectAuthenticationOptions> optionsAccessor)
        {
            _oidcOptions = optionsAccessor.Value;
        }


        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Secured()
        {
            ViewData["Message"] = "Secure page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);

            return Redirect("~/");
        }

        public async Task<IActionResult> CallApiUsingClientCredentials()
        {
            var tokenClient = new TokenClient($"{_oidcOptions.Authority}/connect/token", _oidcOptions.ClientId, _oidcOptions.ClientSecret);
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync(_oidcOptions.Scope);

            var client = new HttpClient();
            client.SetBearerToken(tokenResponse.AccessToken);
            var content = await client.GetStringAsync($"{_oidcOptions.Api}/identity");

            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }

        public async Task<IActionResult> CallApiUsingUserAccessToken()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.SetBearerToken(accessToken);
            var content = await client.GetStringAsync($"{_oidcOptions.Api}/identity");

            ViewBag.Json = JArray.Parse(content).ToString();
            return View("json");
        }
    }
}
