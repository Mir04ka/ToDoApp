using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Web.Controllers;

public class HomeController : Controller
{
    public IActionResult Error(string message = "An error occured")
    {
        ViewBag.ErrorMessage = message;
        return View();
    }
}
