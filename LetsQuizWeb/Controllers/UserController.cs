using System.Text;
using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LetsQuizWeb.Controllers;

public class UserController : Controller
{
    
    IUserService _userService;

    public UserController(IUserService userService)
    {
            _userService = userService;
    }
    // GET
    public async Task<IActionResult> Index()
    {
        return View();
    }
    
    
    [HttpGet]
    public async Task<IActionResult> GetALl()
    {
       var users = await _userService.GetAllAsync();
       
       return Ok(users);
    }
}