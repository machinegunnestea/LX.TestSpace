using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace LX.TestSpace.Client.Services.Authorization
{
    public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
    {
        public CustomAuthorizationMessageHandler(IAccessTokenProvider provider,
        NavigationManager navigation)
        : base(provider, navigation)
        {
            ConfigureHandler(
                authorizedUrls: new[] { "https://localhost:7063" },
                scopes: new[] { "LX.TestSpace.ServerAPI",});
        }
    }
}
