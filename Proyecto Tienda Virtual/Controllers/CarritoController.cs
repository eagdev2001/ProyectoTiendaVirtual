using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Proyecto_Tienda_Virtual.ModelsFromDb;
using Proyecto_Tienda_Virtual.Data;

public class CarritoController : Controller
{
    private readonly TienDaDbContext _context;

    public CarritoController(TienDaDbContext context)
    {
        _context = context;
    }
    public IActionResult Historial()
    {
        var ventas = _context.Ventas
            .Include(v => v.Producto)
            .OrderByDescending(v => v.FechaHora)
            .ToList();

        return View(ventas);
    }
    public IActionResult Index()
    {
        var session = HttpContext.Session.GetString("Carrito");

        if (session == null)
            return View(new List<CarritoItem>());

        var carrito = JsonConvert.DeserializeObject<List<CarritoItem>>(session);

        return View(carrito);
    }
    public IActionResult FinalizarCompra()
    {
        var session = HttpContext.Session.GetString("Carrito");

        if (session == null)
            return RedirectToAction("Index");

        var carrito = JsonConvert.DeserializeObject<List<CarritoItem>>(session);

        foreach (var item in carrito)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.ProductoId == item.ProductoId);

            if (producto != null)
            {
                // Validar stock
                if (producto.ExistenciasAct < item.Cantidad)
                {
                    return Content("No hay suficiente stock para: " + producto.DescripcionArt);
                }

                // Crear venta
                var venta = new Venta
                {
                    ProductoId = producto.ProductoId,
                    CantidadAdq = item.Cantidad,
                    FechaHora = DateTime.Now
                };

                _context.Ventas.Add(venta);

                // Restar stock
                producto.ExistenciasAct -= item.Cantidad;
            }
        }

        _context.SaveChanges();

        // Limpiar carrito
        HttpContext.Session.Remove("Carrito");

        return RedirectToAction("CompraExitosa");
    }
    public IActionResult CompraExitosa()
    {
        return View();
    }
}