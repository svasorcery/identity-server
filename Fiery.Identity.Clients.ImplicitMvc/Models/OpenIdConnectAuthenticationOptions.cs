namespace Fiery.Identity.Clients.ImplicitMvc.Models
{
    public class OpenIdConnectAuthenticationOptions
    {
        public string Authority { get; set; }

        public string Api { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string Scope { get; set; }
    }
}
