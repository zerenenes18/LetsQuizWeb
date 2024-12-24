using System.Security.Claims;

namespace Core.Extensions;

public static class ClaimsPrincipalExtensions
{
    
    public static List<string> Claims(this ClaimsPrincipal claimsPrincipal, string claimType)
    {
        var result = claimsPrincipal?.FindAll(claimType)?.Select(x => x.Value).ToList();
        return result;
    }

    public static string ClaimEmail(this ClaimsPrincipal principal)
    {
        var email = principal.Claims(ClaimTypes.Email)[0];
        return email;
    }
    
    public static string ClaimUserName(this ClaimsPrincipal principal)
    {
        var email = principal.Claims(ClaimTypes.Name)[0];
        return email;
    }


    public static List<string> ClaimRoles(this ClaimsPrincipal claimsPrincipal)
    {
        return claimsPrincipal?.Claims(ClaimTypes.Role);
    }
}