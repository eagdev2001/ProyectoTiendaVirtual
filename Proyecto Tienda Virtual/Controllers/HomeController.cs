using Microsoft.AspNetCore.Mvc;
using Proyecto_Tienda_Virtual.Models;
using System.Diagnostics;

namespace Proyecto_Tienda_Virtual.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Productoes");
        }

        public IActionResult Copyright()    
        {
            return View();
        }

        public IActionResult Integrantes()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
