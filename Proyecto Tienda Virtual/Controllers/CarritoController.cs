using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

public class CarritoController : Controller
{
    public IActionResult Index()
    {
        var session = HttpContext.Session.GetString("Carrito");
        if (session == null)
            return View(new List<CarritoItem>());
        var carrito = JsonConvert.DeserializeObject<List<CarritoItem>>(session);
        return View(carrito);
    }
}