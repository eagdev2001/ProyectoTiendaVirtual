using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proyecto_Tienda_Virtual.ModelsFromDb;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace Proyecto_Tienda_Virtual.Controllers
{
    public class ProductoesController : Controller
    {
        private readonly TienDaDbContext _context;

        public ProductoesController(TienDaDbContext context)
        {
            _context = context;
        }

        // GET: Productoes
        public async Task<IActionResult> Index()
        {
            var items = await _context.Productos.ToListAsync();
            ViewBag.ProductCount = items.Count;
            ViewBag.TypeNames = string.Join(", ", items.Select(i => i.GetType().FullName));
            return View(items);
        }

        // GET: Productoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            ViewBag.DetailModelType = producto?.GetType().FullName;
            return View(producto);
        }

        // GET: Productoes/Create
        public IActionResult AgregarAlCarrito(int id)
        {
            var producto = _context.Productos.Find(id);
            List<CarritoItem> carrito;
            var session = HttpContext.Session.GetString("Carrito");
            if (session != null)
            {
                carrito = JsonConvert.DeserializeObject<List<CarritoItem>>(session);
            } else
            {
                carrito = new List<CarritoItem>();
            }
            carrito.Add(new CarritoItem
            {
                ProductoId = producto.ProductoId,
                Nombre = producto.DescripcionArt,
                Precio = producto.ValorMonetario,
                Cantidad = 1
            });
            HttpContext.Session.SetString("Carrito", JsonConvert.SerializeObject(carrito));

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Productoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([Bind("ProductoId,DescripcionArt,ValorMonetario,ExistenciasAct")] Proyecto_Tienda_Virtual.ModelsFromDb.Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                producto.ValorMonetario = producto.ValorMonetario * 1.15; //Aqui aplicamos un aumento del 15% al valor monetario del producto
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.EditGetModelType = producto?.GetType().FullName;
            return View(producto);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id); 
            if (producto == null)
            {
                return NotFound();
            }
            return View(producto);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, [Bind("ProductoId,DescripcionArt,ValorMonetario,ExistenciasAct")] Proyecto_Tienda_Virtual.ModelsFromDb.Producto producto)
        {
            if (id != producto.ProductoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.ProductoId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Productoes/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.ProductoId == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Productoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(int id)
        {
            return _context.Productos.Any(e => e.ProductoId == id);
        }
    }
}
