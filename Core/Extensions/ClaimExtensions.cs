using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Core.Extensions;

public static class ClaimExtensions
{
  
    public static void AddEmail(this ICollection<Claim> claims, string email)
    {
        if (!string.IsNullOrWhiteSpace(email))
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, email));
        }
    }
    
    public static void AddName(this ICollection<Claim> claims, string name)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            claims.Add(new Claim(ClaimTypes.Name, name));
        }
    }
    
    public static void AddUserName(this ICollection<Claim> claims, string userName)
    {
        if (!string.IsNullOrWhiteSpace(userName))
        {
            claims.Add(new Claim(ClaimTypes.Name, userName));
        }
    }

    public static void AddNameIdentifier(this ICollection<Claim> claims, string nameIdentifier)
    {
        if (!string.IsNullOrWhiteSpace(nameIdentifier))
        {
            claims.Add(new Claim(ClaimTypes.NameIdentifier, nameIdentifier));
        }
    }
    
    public static void AddRoles(this ICollection<Claim> claims, IEnumerable<string> roles)
    {
        if (roles != null)
        {
            foreach (var role in roles.Where(role => !string.IsNullOrWhiteSpace(role)))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }
    }
}