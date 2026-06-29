using Microsoft.AspNetCore.Mvc;
using ToDoApp.Application.Interfaces;

namespace ToDoApp.Web.Controllers;

public class AuthController : Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    [HttpGet] public IActionResult Login() => View();

    [HttpPost]
    public async Task<IActionResult> Login(string username, string password)
    {
        var token = await _authService.LoginAsync(username, password);
        if (!string.IsNullOrEmpty(token))
        {
            Console.WriteLine($"\nУРА! ТОКЕН ПОЛУЧЕН: {token.Substring(0, 20)}...\n");
            HttpContext.Response.Cookies.Append("AuthToken", token, new CookieOptions 
            { 
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Lax
            });
            return RedirectToAction("Index", "Employee");
        }

        ModelState.AddModelError("", "Login failed");
        return View();
    }

    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Response.Cookies.Delete("AuthToken");
        return RedirectToAction("Login");
    }
}
