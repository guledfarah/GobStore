using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace Gob.Services.Identity.Configs;

public static class StaticDetail
{
    // Application user roles
    public const string Admin = "Admin";
    public const string Customer = "Customer";

    // Resources: Resources that need to be protect by IdentityServer; user data, apis, etc...
    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile()
        };
    
    // Resource Scope: Scope of the protected resources that can be accessed by the clients who request
    // access from IdentityServer
    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("gobAdmin", "Gob Server"),
            new ApiScope("productApi", "Gob Product Api Access"),
            new ApiScope("read", "Read data scope."),
            new ApiScope("write", "Write data scope."),
            new ApiScope("delete", "Delete data scope."),
        };
    
    // Clients: Clients that will use IdentityServer to get Identity and Access tokens
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("1600PenAve".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = { "read", "profile" }
            },
            
            new Client
            {
                ClientId = "gobWeb",
                ClientSecrets = { new Secret("1600PenAve".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = {"https://localhost:7144/signin-oidc"},
                PostLogoutRedirectUris = {"https://localhost:7144/signout-callback-oidc"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "gobAdmin"
                }
            },
            
            new Client
            {
                ClientId = "gobUI",
                ClientSecrets = { new Secret("1700PenAve".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = {"http://localhost:4200/login"},
                PostLogoutRedirectUris = {"http://localhost:4200/signout-callback-oidc"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "productApi"
                }
            },
        };
}
