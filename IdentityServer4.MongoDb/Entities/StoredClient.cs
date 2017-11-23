using System.Collections.Generic;
using IdentityServer4.Models;
using MongoDB.Bson;

namespace IdentityServer4.MongoDb.Entities
{
    /*
     * CLIENT:
     * The Client class models an OpenID Connect or OAuth 2.0 client - 
     * e.g. a native application, a web application or a JS-based application.
    */
    public class StoredClient //: Client
    {
        public ObjectId Id { get; set; }

        // Basics
        public bool Enabled { get; set; }
        public string ClientId { get; set; }
        public ICollection<StoredSecret> ClientSecrets { get; set; }
        public bool RequireClientSecret { get; set; }
        public string ProtocolType { get; set; }
        public IEnumerable<string> AllowedGrantTypes { get; set; }
        public bool RequirePkce { get; set; }
        public bool AllowPlainTextPkce { get; set; }
        public ICollection<string> RedirectUris { get; set; }
        public ICollection<string> AllowedScopes { get; set; }
        public bool AllowOfflineAccess { get; set; }
        public bool AllowAccessTokensViaBrowser { get; set; }

        // Authentication / Logout: 
        public ICollection<string> PostLogoutRedirectUris { get; set; }
        public string LogoutUri { get; set; }
        public bool LogoutSessionRequired { get; set; }
        public bool EnableLocalLogin { get; set; }
        public ICollection<string> IdentityProviderRestrictions { get; set; }

        // Token: 
        public int IdentityTokenLifetime { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifetime { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        public TokenUsage RefreshTokenUsage { get; set; }
        public TokenExpiration RefreshTokenExpiration { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public AccessTokenType AccessTokenType { get; set; }
        public bool IncludeJwtId { get; set; }
        public ICollection<string> AllowedCorsOrigins { get; set; }
        public ICollection<StoredClaim> Claims { get; set; }
        public bool AlwaysSendClientClaims { get; set; }
        public bool PrefixClientClaims { get; set; }
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        // Consent Screen: 
        public bool RequireConsent { get; set; }
        public bool AllowRememberConsent { get; set; }
        public string ClientName { get; set; }
        public string ClientUri { get; set; }
        public string LogoUri { get; set; }


        public StoredClient()
        {
            Enabled = true;
            RequireClientSecret = true;
            ProtocolType = IdentityServerConstants.ProtocolTypes.OpenIdConnect;
            RequirePkce = false;
            AllowPlainTextPkce = false;
            AllowOfflineAccess = false;
            AllowAccessTokensViaBrowser = false;
            LogoutSessionRequired = true;
            EnableLocalLogin = true;

            IdentityTokenLifetime = 300;
            AccessTokenLifetime = 3600;
            AuthorizationCodeLifetime = 300;
            AbsoluteRefreshTokenLifetime = 2592000;
            SlidingRefreshTokenLifetime = 1296000;
            RefreshTokenUsage = TokenUsage.OneTimeOnly;
            RefreshTokenExpiration = TokenExpiration.Absolute;
            UpdateAccessTokenClaimsOnRefresh = false;
            AccessTokenType = AccessTokenType.Jwt;
            IncludeJwtId = false;
            AlwaysSendClientClaims = false;
            PrefixClientClaims = true;
            AlwaysIncludeUserClaimsInIdToken = false;

            RequireConsent = true;
            AllowRememberConsent = true;
        }
    }
}
