using Microsoft.AspNetCore.Mvc;

namespace LetsQuizWeb.Controllers;

public class LoginController : Controller
{
    // GET: Login
    public ActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public ActionResult Index(string username, string password)
    {
        // Basit bir kullanıcı doğrulama örneği
        if (username == "admin" && password == "12345")
        {
            // Doğruysa ana sayfaya yönlendir
            return RedirectToAction("Dashboard", "Home");
        }
            
        // Hata mesajı göstermek için ViewBag kullanımı
        ViewBag.ErrorMessage = "Kullanıcı adı veya şifre yanlış!";
        return View();
    }
    
    
    
    public ActionResult ForgotPassword()
    {
        return View();
    }


    
    
}