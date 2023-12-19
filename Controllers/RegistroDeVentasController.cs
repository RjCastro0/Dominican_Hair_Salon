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
    public class RegistroDeVentasController : Controller
    {
        private readonly HairSalonContext _context;

        public RegistroDeVentasController(HairSalonContext context)
        {
            _context = context;
        }

        // GET: RegistroDeVentas
        public async Task<IActionResult> Index()
        {
            var hairSalonContext = _context.RegistroDeVentas.Include(r => r.Ticket);
            return View(await hairSalonContext.ToListAsync());
        }

        // GET: RegistroDeVentas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RegistroDeVentas == null)
            {
                return NotFound();
            }

            var registroDeVenta = await _context.RegistroDeVentas
                .Include(r => r.Ticket)
                .FirstOrDefaultAsync(m => m.VentaId == id);
            if (registroDeVenta == null)
            {
                return NotFound();
            }

            return View(registroDeVenta);
        }

        // GET: RegistroDeVentas/Create
        public IActionResult Create()
        {
            ViewData["TicketId"] = new SelectList(_context.TicketDeVenta, "TicketId", "TicketId");
            return View();
        }

        // POST: RegistroDeVentas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("VentaId,TicketId,Total,Fecha")] RegistroDeVenta registroDeVenta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroDeVenta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TicketId"] = new SelectList(_context.TicketDeVenta, "TicketId", "TicketId", registroDeVenta.TicketId);
            return View(registroDeVenta);
        }

        // GET: RegistroDeVentas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.RegistroDeVentas == null)
            {
                return NotFound();
            }

            var registroDeVenta = await _context.RegistroDeVentas.FindAsync(id);
            if (registroDeVenta == null)
            {
                return NotFound();
            }
            ViewData["TicketId"] = new SelectList(_context.TicketDeVenta, "TicketId", "TicketId", registroDeVenta.TicketId);
            return View(registroDeVenta);
        }

        // POST: RegistroDeVentas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("VentaId,TicketId,Total,Fecha")] RegistroDeVenta registroDeVenta)
        {
            if (id != registroDeVenta.VentaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(registroDeVenta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroDeVentaExists(registroDeVenta.VentaId))
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
            ViewData["TicketId"] = new SelectList(_context.TicketDeVenta, "TicketId", "TicketId", registroDeVenta.TicketId);
            return View(registroDeVenta);
        }

        // GET: RegistroDeVentas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.RegistroDeVentas == null)
            {
                return NotFound();
            }

            var registroDeVenta = await _context.RegistroDeVentas
                .Include(r => r.Ticket)
                .FirstOrDefaultAsync(m => m.VentaId == id);
            if (registroDeVenta == null)
            {
                return NotFound();
            }

            return View(registroDeVenta);
        }

        // POST: RegistroDeVentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.RegistroDeVentas == null)
            {
                return Problem("Entity set 'HairSalonContext.RegistroDeVentas'  is null.");
            }
            var registroDeVenta = await _context.RegistroDeVentas.FindAsync(id);
            if (registroDeVenta != null)
            {
                _context.RegistroDeVentas.Remove(registroDeVenta);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroDeVentaExists(int id)
        {
          return (_context.RegistroDeVentas?.Any(e => e.VentaId == id)).GetValueOrDefault();
        }
    }
}
