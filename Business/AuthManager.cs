using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess;
using LetsQuizCore.Entities;
using LetsQuizCore.Entities.DTOs;
using Microsoft.AspNetCore.Http;

using IResult = Core.Utilities.Results.IResult;

namespace Business;

public class AuthManager : IAuthService
{
    
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly EfVerificationCodeDal _validationCodeDal;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor, EfVerificationCodeDal validationCodeDal)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _tokenHelper = tokenHelper ?? throw new ArgumentNullException(nameof(tokenHelper));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _validationCodeDal = validationCodeDal ?? throw new ArgumentNullException(nameof(validationCodeDal));
    }


    public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
    {
        byte[] passwordHash;
        byte[] passwordSalt;
            
        HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        var user = new User
        {
            Email = userForRegisterDto.Email,
            FirstName = userForRegisterDto.FirstName,
            LastName = userForRegisterDto.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Role = userForRegisterDto.Role,
            Status = true,
            EmailControl = false
                
        };
        _userService.Add(user);
        return new SuccessDataResult<User>(user, Messages.UserRegistered);
    }

    public IDataResult<User> Login(UserForLoginDto userForLoginDto)
    {
        User userToCheck = _userService.GetByMail(userForLoginDto.Email).Data;
        if (userToCheck == null)
        {
            return new ErrorDataResult<User>(Messages.UserNotFound);
        }

        if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
        {
            return new ErrorDataResult<User>(Messages.PasswordError);
        }

        return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
    }

    public IResult UserExists(string email)
    {
        if (_userService.GetByMail(email).Success)
        {
            return new SuccessResult();
        }
        return new ErrorResult(Messages.UserNotFound);

    }

    public IDataResult<AccessToken> CreateAccessToken(User user)
    {
        var claims = _userService.GetClaims(user).Data;
        AccessToken accessToken = _tokenHelper.CreateToken(user, claims);
        return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
    }

    public IResult RegisterEmailSendCode()
    {
        throw new NotImplementedException();
    }

    public IResult RegisterControlEmailCode(int code)
    {
        throw new NotImplementedException();
    }
}