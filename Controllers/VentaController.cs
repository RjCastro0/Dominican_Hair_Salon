using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dominican_Hair_Salon.Models;

namespace Dominican_Hair_Salon.Controllers
{
    public class VentaController : Controller
    {
        private readonly HairSalonContext _context;

        public VentaController(HairSalonContext context)
        {
            _context = context;
        }

        // GET: Venta
        public async Task<IActionResult> Index()
        {
            var hairSalonContext = _context.Venta.Include(v => v.Servicio).Include(v => v.Ticket);
            return View(await hairSalonContext.ToListAsync());
        }

        // GET: Venta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var venta = await _context.Venta
                .Include(v => v.Servicio)
                .Include(v => v.Ticket)
                .FirstOrDefaultAsync(m => m.VentaId == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // GET: Venta/Create
        public IActionResult Create()
        {
            ViewData["ServicioId"] = new SelectList(_context.Menus, "ServicioId", "ServicioId");
            ViewData["TicketId"] = new SelectList(_context.TicketDeVenta, "TicketId", "TicketId");
            return View();
        }

        // POST: Venta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VentaId,TicketId,ServicioId")] Venta venta)
        {

            _context.Add(venta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        
            
            return View(venta);
        }

        // GET: Venta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var venta = await _context.Venta.FindAsync(id);
            if (venta == null)
            {
                return NotFound();
            }
            ViewData["ServicioId"] = new SelectList(_context.Menus, "ServicioId", "ServicioId", venta.ServicioId);
            ViewData["TicketId"] = new SelectList(_context.TicketDeVenta, "TicketId", "TicketId", venta.TicketId);
            return View(venta);
        }

        // POST: Venta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VentaId,TicketId,ServicioId")] Venta venta)
        {
            if (id != venta.VentaId)
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
                    if (!VentaExists(venta.VentaId))
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
            ViewData["ServicioId"] = new SelectList(_context.Menus, "ServicioId", "ServicioId", venta.ServicioId);
            ViewData["TicketId"] = new SelectList(_context.TicketDeVenta, "TicketId", "TicketId", venta.TicketId);
            return View(venta);
        }

        // GET: Venta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var venta = await _context.Venta
                .Include(v => v.Servicio)
                .Include(v => v.Ticket)
                .FirstOrDefaultAsync(m => m.VentaId == id);
            if (venta == null)
            {
                return NotFound();
            }

            return View(venta);
        }

        // POST: Venta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Venta == null)
            {
                return Problem("Entity set 'HairSalonContext.Venta'  is null.");
            }
            var venta = await _context.Venta.FindAsync(id);
            if (venta != null)
            {
                _context.Venta.Remove(venta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentaExists(int id)
        {
          return (_context.Venta?.Any(e => e.VentaId == id)).GetValueOrDefault();
        }

        [HttpPost]
        public JsonResult InsertVentas(List<Venta> ventas)
        {
            try
            {
                // Truncate Table para eliminar todos los registros antiguos
                _context.Database.ExecuteSqlRaw("TRUNCATE TABLE [Ventas]");

                // Verifica si la lista es nula y asigna una lista vacía si es necesario
                if (ventas == null)
                {
                    ventas = new List<Venta>();
                }

                // Agrega cada venta a la base de datos
                foreach (Venta venta in ventas)
                {
                    ViewData["ServicioId"] = new SelectList(_context.Menus, "ServicioId", "ServicioId", venta.ServicioId);
                    ViewData["TicketId"] = new SelectList(_context.TicketDeVenta, "TicketId", "TicketId", venta.TicketId);
                    _context.Venta.Add(venta);
                }

                // Guarda los cambios en la base de datos
                int insertedRecords = _context.SaveChanges();

                return Json(insertedRecords);
            }
            catch (Exception ex)
            {
                // Maneja cualquier excepción que pueda ocurrir durante la operación
                return Json(ex.Message);
            }
        }
    }
}

