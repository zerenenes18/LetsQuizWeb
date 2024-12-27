
using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using LetsQuizCore.Entities;
using LetsQuizCore.Entities.DTOs;

namespace Business.Abstract;

public interface IAuthService
{
    Task<IDataResult<User>> RegisterAsync(UserForRegisterDto userForRegisterDto, string password);
    Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto);
    Task<IResult> UserExistsAsync(string email);
    Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user);
    Task<IResult> RegisterEmailSendCodeAsync();
    Task<IResult> RegisterControlEmailCodeAsync(int code);
}


