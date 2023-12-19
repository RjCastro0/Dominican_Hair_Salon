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
    public class TicketDeVentaController : Controller
    {
        private readonly HairSalonContext _context;

        public TicketDeVentaController(HairSalonContext context)
        {
            _context = context;
        }

        // GET: TicketDeVenta
        public async Task<IActionResult> Index()
        {
            var hairSalonContext = _context.TicketDeVenta.Include(t => t.Surcursal);
            return View(await hairSalonContext.ToListAsync());
        }

        // GET: TicketDeVenta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TicketDeVenta == null)
            {
                return NotFound();
            }

            var ticketDeVenta = await _context.TicketDeVenta
                .Include(t => t.Surcursal)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticketDeVenta == null)
            {
                return NotFound();
            }

            return View(ticketDeVenta);
        }

        // GET: TicketDeVenta/Create
        public IActionResult Create()
        {
            ViewData["SurcursalId"] = new SelectList(_context.Sucursals, "SucursalesId", "SucursalesId");

            return View();
        }

        // POST: TicketDeVenta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TicketId,SurcursalId,Fecha,Empleada,Precio,ClienteNombre")] TicketDeVenta ticketDeVenta)
        {
            ticketDeVenta.Fecha = DateTime.Now;
           
            _context.Add(ticketDeVenta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

            ViewData["SurcursalId"] = new SelectList(_context.Sucursals, "SucursalesId", "SucursalesId", ticketDeVenta.SurcursalId);
            return View(ticketDeVenta);
        }

        // GET: TicketDeVenta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TicketDeVenta == null)
            {
                return NotFound();
            }

            var ticketDeVenta = await _context.TicketDeVenta.FindAsync(id);
            if (ticketDeVenta == null)
            {
                return NotFound();
            }
            ViewData["SurcursalId"] = new SelectList(_context.Sucursals, "SucursalesId", "SucursalesId", ticketDeVenta.SurcursalId);
            return View(ticketDeVenta);
        }

        // POST: TicketDeVenta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TicketId,SurcursalId,Fecha,Empleada,Precio,ClienteNombre")] TicketDeVenta ticketDeVenta
            )
        {
            if (id != ticketDeVenta.TicketId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticketDeVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketDeVentaExists(ticketDeVenta.TicketId))
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
            ViewData["SurcursalId"] = new SelectList(_context.Sucursals, "SucursalesId", "SucursalesId", ticketDeVenta.SurcursalId);
            return View(ticketDeVenta);
        }

        // GET: TicketDeVenta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TicketDeVenta == null)
            {
                return NotFound();
            }

            var ticketDeVenta = await _context.TicketDeVenta
                .Include(t => t.Surcursal)
                .FirstOrDefaultAsync(m => m.TicketId == id);
            if (ticketDeVenta == null)
            {
                return NotFound();
            }

            return View(ticketDeVenta);
        }

        // POST: TicketDeVenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TicketDeVenta == null)
            {
                return Problem("Entity set 'HairSalonContext.TicketDeVenta'  is null.");
            }
            var ticketDeVenta = await _context.TicketDeVenta.FindAsync(id);
            if (ticketDeVenta != null)
            {
                _context.TicketDeVenta.Remove(ticketDeVenta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketDeVentaExists(int id)
        {
          return (_context.TicketDeVenta?.Any(e => e.TicketId == id)).GetValueOrDefault();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddVentaAndServicio([Bind("TicketId,ServicioId,Fecha,Empleada,Precio,ClienteNombre")] TicketDeVenta ticketDeVenta, [Bind("ServicioId")] Venta venta)
        {
            if (ModelState.IsValid)
            {
                // Lógica para agregar TicketDeVenta y Venta
                _context.Add(ticketDeVenta);
                await _context.SaveChangesAsync();

                venta.TicketId = ticketDeVenta.TicketId;
                _context.Add(venta);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Si la validación falla, vuelve a mostrar el formulario
            // con los mensajes de error
            return View(ticketDeVenta);
        }
    }
}
