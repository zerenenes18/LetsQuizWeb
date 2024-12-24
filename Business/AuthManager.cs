using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using Business.Abstract;
using Business.Constants;
using Core.Extensions;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using DataAccess;
using LetsQuizCore.Entities;
using LetsQuizCore.Entities.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using IResult = Core.Utilities.Results.IResult;

namespace Business;

public class AuthManager : IAuthService
{
    
    private readonly IUserService _userService;
    private readonly ITokenHelper _tokenHelper;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly VerificationCodeDal _verificationCodeDal;
    private readonly MailSettings _mailSettings;


    public AuthManager(IUserService userService, ITokenHelper tokenHelper, IHttpContextAccessor httpContextAccessor, VerificationCodeDal validationCodeDal,IOptions<MailSettings> mailSettings)
    {
        _userService = userService ?? throw new ArgumentNullException(nameof(userService));
        _tokenHelper = tokenHelper ?? throw new ArgumentNullException(nameof(tokenHelper));
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _verificationCodeDal = validationCodeDal ?? throw new ArgumentNullException(nameof(validationCodeDal));
        _mailSettings = mailSettings.Value;
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
            UserName = userForRegisterDto.UserName,
            Role = userForRegisterDto.Role ?? "Student",
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
            userToCheck = _userService.GetByUserName(userForLoginDto.Email).Data;
            if (userToCheck == null)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }
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
        var userEmail = _httpContextAccessor.HttpContext.User.ClaimEmail();
        var userName = _httpContextAccessor.HttpContext.User.ClaimUserName();
        var code = new Random().Next(10000);
        _verificationCodeDal.AddAsync(new Core.Entities.VerificationCode()
        {
            UserEmail = userEmail,
            Code = code,
        });
        SendEMail(userName,userEmail,code).GetAwaiter().GetResult();

        return new SuccessResult();
    }

 
    public IResult RegisterControlEmailCode(int code)
    {
        var userEmail = _httpContextAccessor.HttpContext.User.ClaimEmail();

        var DbCode = _verificationCodeDal.GetAsync(c => c.UserEmail == userEmail).GetAwaiter().GetResult().Code;

        if (code == DbCode)
        {
            var userToUpdate =  _userService.GetByMail(userEmail).Data;
            userToUpdate.EmailControl = true;
            _userService.Update(userToUpdate);
            var deleteToCode = _verificationCodeDal.GetAsync(c => c.Code == code).GetAwaiter().GetResult();
            _verificationCodeDal.DeleteAsync(deleteToCode).GetAwaiter().GetResult();
            return new SuccessResult();
        }

        return new ErrorResult();

    }
    
    
    private async Task SendEMail(string userName,string email, int verificationCode)
    {
    string image = "../Business/Images/VerificationImage.png";
    string body = $@"
    <!DOCTYPE html>
    <html lang='en'>
    <head>
        <meta charset='UTF-8'>
        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
        <title>Account Verification - {_mailSettings.WebsiteName}</title>
        <style>
            body {{
                font-family: Arial, sans-serif;
                line-height: 1.6;
                color: #333;
                margin: 0;
                padding: 0;
                background-color: #f4f4f9;
            }}
            .email-container {{
                width: 100%;
                max-width: 600px;
                margin: 20px auto;
                background: #fff;
                padding: 20px;
                border-radius: 8px;
                box-shadow: 0px 4px 8px rgba(0, 0, 0, 0.1);
                overflow: hidden;
            }}
            .email-header {{
                text-align: center;
                margin-bottom: 20px;
            }}
            .email-header img {{
                max-width: 100%;
                height: auto;
            }}
            .email-body {{
                text-align: left;
            }}
            .verification-code {{
                font-size: 24px;
                font-weight: bold;
                color: #4caf50;
                text-align: center;
                margin: 20px 0;
            }}
            .email-footer {{
                text-align: center;
                margin-top: 30px;
                color: #888;
                font-size: 12px;
            }}
        </style>
    </head>
    <body>
        <div class='email-container'>
            <div class='email-header'>
                <img src='cid:VerificationImage' alt='Verification Image'>
            </div>
            <div class='email-body'>
                <h1>Hello {userName},</h1>
                <p>
                    We have sent you a unique verification code to confirm your account.
                    Please use the verification code below to activate your account:
                </p>
                <div class='verification-code'>
                    {verificationCode}
                </div>
                <p>
                    Enter this code into the required field to verify your account and complete the process.
                    If you did not create this account, please contact our customer service before sharing the verification code with anyone.
                </p>
                <p>
                    <strong>The {_mailSettings.WebsiteName} Team wishes you enjoyable quizzes!</strong>
                </p>
            </div>
            <div class='email-footer'>
                Best regards,<br>
                {_mailSettings.WebsiteName} Support Team
            </div>
        </div>
    </body>
    </html>";

    try
    {
        using (MailMessage mailMessage = new MailMessage(_mailSettings.SenderEmail, email, _mailSettings.Subject, body))
        {
            mailMessage.IsBodyHtml = true;

            LinkedResource linkedImage = new LinkedResource("../Business/Images/VerificationImage.png")
            {
                ContentId = "VerificationImage",
                ContentType = new ContentType("image/png")
            };

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
            htmlView.LinkedResources.Add(linkedImage);
            mailMessage.AlternateViews.Add(htmlView);
         

            using (SmtpClient smtpClient = new SmtpClient(_mailSettings.SmtpServer, _mailSettings.SmtpPort))
            {
                smtpClient.EnableSsl = true;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(_mailSettings.SenderEmail, _mailSettings.AppPassword);

                smtpClient.Send(mailMessage);
                Console.WriteLine("E-posta başarıyla gönderildi.");
            }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("E-posta gönderirken bir hata oluştu: " + ex.Message);
    }
}

}








