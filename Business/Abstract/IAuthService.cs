using Azure.Core;
using Core.Utilities.Results;
using LetsQuizCore.Entities;
using LetsQuizCore.Entities.DTOs;

namespace Business.Abstract;

public interface IAuthService
{
    IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password);
    IDataResult<User> Login(UserForLoginDto userForLoginDto);
    IResult UserExists(string email);
    IDataResult<AccessToken> CreateAccessToken(User user);
    IResult RegisterEmailSendCode();
    IResult RegisterControlEmailCode(int code);
}