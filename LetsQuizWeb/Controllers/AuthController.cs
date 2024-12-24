using System.Text.Json;
using Business.Abstract;
using LetsQuizCore.Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace LetsQuizWeb.Controllers;

public class AuthController : Controller
{
    
    private IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    public IActionResult Verification()
    {
        return View();
    }


    public ActionResult Login([FromBody]UserForLoginDto userForLoginDto)
    {
        var userToLogin = _authService.Login(userForLoginDto);
        if (!userToLogin.Success)
        {
            return BadRequest(userToLogin.Message);
        }

        var result = _authService.CreateAccessToken(userToLogin.Data);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result.Message);
    }

    public IActionResult Register()
    {
        return View();
    }

    public ActionResult UserRegister([FromBody] UserForRegisterDto userForRegisterDto)
    {
        var userExists = _authService.UserExists(userForRegisterDto.Email);
        if (userExists.Success)
        {
            return BadRequest(userExists.Message);
        }
            
        var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
        var result = _authService.CreateAccessToken(registerResult.Data);
        if (result.Success)
        {
            //_authService.RegisterEmailSendCode();
            return Ok(result);
        }

        return BadRequest(result.Message);
    }
    
    public ActionResult ControlCode([FromBody] string code)
    {
        try
        {
            if (string.IsNullOrEmpty(code))
            {
                return BadRequest("Verification code is missing or empty.");
            }

            // Doğrulama kodunu int'e dönüştürme
            if (!int.TryParse(code, out int codeValue))
            {
                return BadRequest("Verification code is invalid.");
            }

            // Servis çağrısı
            var result = _authService.RegisterControlEmailCode(codeValue);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred: {ex.Message}");
        }
    }
    
    public ActionResult SendVerificationCode()
    {
        var result = _authService.RegisterEmailSendCode();

        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result.Message);
    }
}