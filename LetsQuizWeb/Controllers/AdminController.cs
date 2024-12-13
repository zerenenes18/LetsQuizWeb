using Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace LetsQuizWeb.Controllers;

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
    
    
}