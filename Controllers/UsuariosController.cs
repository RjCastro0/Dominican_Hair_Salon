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
    public class UsuariosController : Controller
    {
        private readonly HairSalonContext _context;

        public UsuariosController(HairSalonContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var hairSalonContext = _context.Usuarios.Include(u => u.RoleNavigation).Include(u => u.SucursalNavigation);
            return View(await hairSalonContext.ToListAsync());
        }

        // GET: Usuarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.RoleNavigation)
                .Include(u => u.SucursalNavigation)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            ViewData["Role"] = new SelectList(_context.Roles, "RoleName", "RoleName");
            ViewData["Sucursal"] = new SelectList(_context.Sucursals, "SucursalesId", "SucursalesId");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,Password,Sucursal,Status,Role")] Usuario usuario)
        {
            
            _context.Add(usuario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            ViewData["Role"] = new SelectList(_context.Roles, "RoleName", "RoleName", usuario.Role);
            ViewData["Sucursal"] = new SelectList(_context.Sucursals, "SucursalesId", "SucursalesId", usuario.Sucursal);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            ViewData["Role"] = new SelectList(_context.Roles, "RoleName", "RoleName", usuario.Role);
            ViewData["Sucursal"] = new SelectList(_context.Sucursals, "SucursalesId", "SucursalesId", usuario.Sucursal);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,Password,Sucursal,Status,Role")] Usuario usuario)
        {
            if (id != usuario.UserId)
            {
                return NotFound();
            }

            try
            {
                _context.Update(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsuarioExists(usuario.UserId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

                
            }
           
            ViewData["Role"] = new SelectList(_context.Roles, "RoleName", "RoleName", usuario.Role);
            ViewData["Sucursal"] = new SelectList(_context.Sucursals, "SucursalesId", "SucursalesId", usuario.Sucursal);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Usuarios == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios
                .Include(u => u.RoleNavigation)
                .Include(u => u.SucursalNavigation)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (usuario == null)
            {
                return NotFound();
            }

            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'HairSalonContext.Usuarios'  is null.");
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
          return (_context.Usuarios?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
