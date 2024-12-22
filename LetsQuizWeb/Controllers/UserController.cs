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
    public IActionResult Index()
    {
        return View();
    }
    
    
    [HttpGet]
    public IActionResult GetALl()
    {
       var users = _userService.GetAll();
       
       return Ok(users);
    }
}