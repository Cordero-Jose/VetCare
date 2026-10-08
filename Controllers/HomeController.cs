using Microsoft.AspNetCore.Mvc;

namespace VetCare.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return RedirectToAction("Index", "Mascotas");
    }
}
