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
    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    public async Task<IActionResult> Verification()
    {
        return View();
    }


    public async Task<IActionResult> Login([FromBody]UserForLoginDto userForLoginDto)
    {
        var userToLogin = await _authService.LoginAsync(userForLoginDto);
        if (!userToLogin.Success)
        {
            return BadRequest(userToLogin.Message);
        }

        var result = await _authService.CreateAccessTokenAsync(userToLogin.Data);
        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result.Message);
    }

    public async Task<IActionResult> Register()
    {
        return View();
    }

    public async Task<IActionResult> UserRegister([FromBody] UserForRegisterDto userForRegisterDto)
    {
        var userExists = await _authService.UserExistsAsync(userForRegisterDto.Email);
        if (userExists.Success)
        {
            return BadRequest(userExists.Message);
        }
            
        var registerResult = await _authService.RegisterAsync(userForRegisterDto, userForRegisterDto.Password);
        var result = await _authService.CreateAccessTokenAsync(registerResult.Data);
        if (result.Success)
        {
            //_authService.RegisterEmailSendCode();
            return Ok(result);
        }

        return BadRequest(result.Message);
    }
    
    public async Task<IActionResult> ControlCode([FromBody] string code)
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
            var result =  await _authService.RegisterControlEmailCodeAsync(codeValue);
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
    
    public async Task<IActionResult> SendVerificationCode()
    {
        var result = await _authService.RegisterEmailSendCodeAsync();

        if (result.Success)
        {
            return Ok(result);
        }

        return BadRequest(result.Message);
    }
}