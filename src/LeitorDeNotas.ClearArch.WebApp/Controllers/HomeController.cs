using Microsoft.AspNetCore.Mvc;

namespace LeitorDeNotas.ClearArch.WebApp.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
