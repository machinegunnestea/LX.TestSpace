using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication.Internal;

namespace LX.TestSpace.Client.Services.Authorization;

public class CustomProfileFactory : AccountClaimsPrincipalFactory<RemoteUserAccount>
{
    public CustomProfileFactory(IAccessTokenProviderAccessor accessor) : base(accessor)
    {
    }

    public override async ValueTask<ClaimsPrincipal> CreateUserAsync(RemoteUserAccount account, RemoteAuthenticationUserOptions options)
    {
        var user = await base.CreateUserAsync(account, options);
        //fix weired behaviour when here is multiple roles
        if (user.Identity is not { IsAuthenticated: true })
            return user;
        
        if (!account.AdditionalProperties.TryGetValue("role", out var property)) 
            return user;
        
        if (property is not JsonElement { ValueKind: JsonValueKind.Array or JsonValueKind.String } element) 
            return user;
        
        var userIdentity = (ClaimsIdentity)user.Identity;
        if (element.ValueKind == JsonValueKind.Array)
        {
            FillRolesFromArray(element, userIdentity);
        }
        else
        {
            userIdentity.AddClaim(new Claim(ClaimTypes.Role, element.GetString()?? ""));
        }

        return user;
    }

    private static void FillRolesFromArray(JsonElement element, ClaimsIdentity userIdentity)
    {
        var enumerator = element.EnumerateArray();
        while (enumerator.MoveNext())
        {
            userIdentity.AddClaim(new Claim(ClaimTypes.Role, enumerator.Current.GetString() ?? ""));
        }
    }
}
