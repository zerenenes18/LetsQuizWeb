using Business.Abstract;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LetsQuizWeb.Controllers;


public class AdminController : Controller
{
    
    IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }
    
    
    public async Task<IActionResult> Index()
    {
        // return View();
        var result = await _adminService.GetAllAsync();
        return View(result.Data);
    }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Admin erişim sağladı!");
        }
    
}