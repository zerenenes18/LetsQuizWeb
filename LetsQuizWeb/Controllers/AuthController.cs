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
    
    

    public ActionResult Login(UserForLoginDto userForLoginDto)
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

   
    public ActionResult Register(UserForRegisterDto userForRegisterDto)
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
    
    
    
  
}