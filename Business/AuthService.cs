using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Business.Abstract;
using DataAccess.Abstract;
using LetsQuizCore.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Business;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
     IUserDal _userDal;

    public AuthService(IConfiguration configuration,IUserDal userDal)
    {
        _configuration = configuration;
        _userDal = userDal;
    }

    public User Authenticate(string username, string password)
    {
        List<User> userList = _userDal.GetAllAsync().GetAwaiter().GetResult();
        return userList.SingleOrDefault(x => x.UserName == username && x.PasswordHash == password);
    }

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}