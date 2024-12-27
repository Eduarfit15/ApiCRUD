using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class DetalleVentumsController : Controller
    {
        private readonly Prueba1Context _context;

        public DetalleVentumsController(Prueba1Context context)
        {
            _context = context;
        }

        // GET: DetalleVentums
        public async Task<IActionResult> Index()
        {
            var prueba1Context = _context.DetalleVenta.Include(d => d.IdFacturaNavigation).Include(d => d.IdProductoNavigation);
            return View(await prueba1Context.ToListAsync());
        }

        // GET: DetalleVentums/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVentum = await _context.DetalleVenta
                .Include(d => d.IdFacturaNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleVenta == id);
            if (detalleVentum == null)
            {
                return NotFound();
            }

            return View(detalleVentum);
        }

        // GET: DetalleVentums/Create
        public IActionResult Create()
        {
            ViewData["IdFactura"] = new SelectList(_context.Ventas, "IdFactura", "IdFactura");
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "NomProducto");
            
            return View();
        }

        // POST: DetalleVentums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleVenta,IdFactura,IdProducto,Cantidad,Subtotal")] DetalleVentum detalleVentum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(detalleVentum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdFactura"] = new SelectList(_context.Ventas, "IdFactura", "IdFactura", detalleVentum.IdFactura);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "NomProducto", detalleVentum.IdProducto);
            return View(detalleVentum);

        }

        [HttpGet]
        public JsonResult GetPrecioProducto(int idProducto)
        {
            var producto = _context.Productos.FirstOrDefault(p => p.IdProducto == idProducto);
            if (producto != null)
            {
                return Json(new { precio = producto.Precio });
            }
            return Json(new { error = "Producto no encontrado" });
        }


        // GET: DetalleVentums/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVentum = await _context.DetalleVenta.FindAsync(id);
            if (detalleVentum == null)
            {
                return NotFound();
            }
            ViewData["IdFactura"] = new SelectList(_context.Ventas, "IdFactura", "IdFactura", detalleVentum.IdFactura);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleVentum.IdProducto);
            return View(detalleVentum);
        }

        // POST: DetalleVentums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalleVenta,IdFactura,IdProducto,Cantidad,Subtotal")] DetalleVentum detalleVentum)
        {
            if (id != detalleVentum.IdDetalleVenta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(detalleVentum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DetalleVentumExists(detalleVentum.IdDetalleVenta))
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
            ViewData["IdFactura"] = new SelectList(_context.Ventas, "IdFactura", "IdFactura", detalleVentum.IdFactura);
            ViewData["IdProducto"] = new SelectList(_context.Productos, "IdProducto", "IdProducto", detalleVentum.IdProducto);
            return View(detalleVentum);
        }

        // GET: DetalleVentums/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var detalleVentum = await _context.DetalleVenta
                .Include(d => d.IdFacturaNavigation)
                .Include(d => d.IdProductoNavigation)
                .FirstOrDefaultAsync(m => m.IdDetalleVenta == id);
            if (detalleVentum == null)
            {
                return NotFound();
            }

            return View(detalleVentum);
        }

        // POST: DetalleVentums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var detalleVentum = await _context.DetalleVenta.FindAsync(id);
            if (detalleVentum != null)
            {
                _context.DetalleVenta.Remove(detalleVentum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DetalleVentumExists(int id)
        {
            return _context.DetalleVenta.Any(e => e.IdDetalleVenta == id);
        }
    }
}
