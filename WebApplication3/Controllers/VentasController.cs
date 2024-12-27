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
    public class VentasController : Controller
    {
        private readonly Prueba1Context _context;

        public VentasController(Prueba1Context context)
        {
            _context = context;
        }

        // GET: Ventas
        public async Task<IActionResult> Index()
        {
            var prueba1Context = _context.Ventas.Include(v => v.ClienteNavigation).Include(v => v.ProductosNavigation);
            return View(await prueba1Context.ToListAsync());
        }

        // GET: Ventas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.ClienteNavigation)
                .Include(v => v.ProductosNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Ventas/Create
        public IActionResult Create()
        {
            ViewData["Cliente"] = new SelectList(_context.Clientes, "Dni", "Nombres");
            ViewData["Productos"] = new SelectList(_context.Productos, "IdProducto", "NomProducto");
            return View();
        }

        // POST: Ventas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFactura,Cliente,Productos,Fecha,ImportFinal")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(venta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cliente"] = new SelectList(_context.Clientes, "Dni", "Nombres", venta.Cliente);
            ViewData["Productos"] = new SelectList(_context.Productos, "IdProducto", "NomProducto", venta.Productos);
            return View(venta);
        }

        // GET: Ventas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["Cliente"] = new SelectList(_context.Clientes, "Dni", "Nombres", venta.Cliente);
            ViewData["Productos"] = new SelectList(_context.Productos, "IdProducto", "NomProducto", venta.Productos);
            return View(venta);
        }

        // POST: Ventas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdFactura,Cliente,Productos,Fecha,ImportFinal")] Venta venta)
        {
            if (id != venta.IdFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(venta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentaExists(venta.IdFactura))
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
            ViewData["Cliente"] = new SelectList(_context.Clientes, "Dni", "Nombres", venta.Cliente);
            ViewData["Productos"] = new SelectList(_context.Productos, "IdProducto", "NomProducto", venta.Productos);
            return View(venta);
        }

        // GET: Ventas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venta = await _context.Ventas
                .Include(v => v.ClienteNavigation)
                .Include(v => v.ProductosNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Ventas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var venta = await _context.Ventas.FindAsync(id);
            if (venta != null)
            {
                _context.Ventas.Remove(venta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
            return _context.Ventas.Any(e => e.IdFactura == id);
        }
    }
}
