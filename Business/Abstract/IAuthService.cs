using LetsQuizCore.Entities;

namespace Business.Abstract;

public interface IAuthService
{
    string GenerateJwtToken(User user);
    User Authenticate(string username, string password);
}