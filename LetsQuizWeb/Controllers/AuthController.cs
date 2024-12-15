using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LetsQuizWeb.Controllers;

[ApiController]
 // -- localhost:6060/Auth /login
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")] // --> localhost:6060/api/Auth/login 
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = _authService.Authenticate(request.Username, request.Password);
        if (user == null)
        {
            return Unauthorized();
        }

        var token = _authService.GenerateJwtToken(user);
        return Ok(new { Token = token });
    }
    
    [HttpPost("register")] // --> localhost:6060/api/Auth/login 
    public IActionResult Register([FromBody] LoginRequest request)
    {
        var user = _authService.Authenticate(request.Username, request.Password);
        if (user == null)
        {
            return Unauthorized();
        }

        var token = _authService.GenerateJwtToken(user);
        return Ok(new { Token = token });
    }
    
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}