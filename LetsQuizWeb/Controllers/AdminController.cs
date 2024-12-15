using Business.Abstract;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetsQuizWeb.Controllers;

[Authorize(Roles = UserRoles.Admin)]
[ApiController]
public class AdminController : Controller
{
    
    IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    
    
    public IActionResult Index()
    {
        // return View();
        var result = _adminService.GetAll();
        return View(result.Data);
    }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Admin erişim sağladı!");
        }
    
}