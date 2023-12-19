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

            var ventum = await _context.Venta
                .Include(v => v.Servicio)
                .Include(v => v.Ticket)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ventum == null)
            {
                return NotFound();
            }

            return View(ventum);
        }

        // GET: Venta/Create
        public IActionResult Create()
        {
            // ... tu lógica para obtener opciones de servicio, por ejemplo:
            var servicios = _context.Menus.ToList();

            // Mapea los servicios a SelectListItem
            var servicioItems = servicios.Select(s => new SelectListItem { Value = s.ServicioId.ToString(), Text = s.NombreServicio }).ToList();

            // Agrega un elemento por defecto si es necesario
            servicioItems.Insert(0, new SelectListItem { Value = "", Text = "Seleccione un servicio" });

            ViewBag.ServicioItems = servicioItems;

            return View();
        }

        // POST: Venta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,ServicioId")] Venta ventum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ventum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Si hay un error en el modelo, vuelve a cargar las opciones para el dropdown
            var servicios = _context.Menus.ToList();
            var servicioItems = servicios.Select(s => new SelectListItem { Value = s.ServicioId.ToString(), Text = s.NombreServicio }).ToList();
            servicioItems.Insert(0, new SelectListItem { Value = "", Text = "Seleccione un servicio" });
            ViewBag.ServicioItems = servicioItems;

            // También puedes volver a cargar las opciones para TicketId si es necesario

            return View(ventum);
        }

        // GET: Venta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta.FindAsync(id);
            if (ventum == null)
            {
                return NotFound();
            }
            ViewData["ServicioId"] = new SelectList(_context.Menus, "ServicioId", "NombreServicio", ventum.ServicioId);
            ViewData["TicketId"] = new SelectList(_context.TicketDeVenta, "TicketId", "TicketId", ventum.TicketId);
            return View(ventum);
        }

        // POST: Venta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,ServicioId")] Venta ventum)
        {
            if (id != ventum.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ventum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VentumExists(ventum.TicketId))
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
            ViewData["ServicioId"] = new SelectList(_context.Menus, "ServicioId", "NombreServicio", ventum.ServicioId);
            ViewData["TicketId"] = new SelectList(_context.TicketDeVenta, "TicketId", "TicketId", ventum.TicketId);
            return View(ventum);
        }

        // GET: Venta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Venta == null)
            {
                return NotFound();
            }

            var ventum = await _context.Venta
                .Include(v => v.Servicio)
                .Include(v => v.Ticket)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ventum == null)
            {
                return NotFound();
            }

            return View(ventum);
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
            var ventum = await _context.Venta.FindAsync(id);
            if (ventum != null)
            {
                _context.Venta.Remove(ventum);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VentumExists(int id)
        {
          return (_context.Venta?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }
    }
}
