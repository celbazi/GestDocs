using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestDoc.Data;
using GestDoc.Models;

namespace GestDoc.Controllers
{
    public class ReunionsController : Controller
    {
        private readonly GestDocsContext _context;

        public ReunionsController(GestDocsContext context)
        {
            _context = context;
        }

        // GET: Reunions
        public async Task<IActionResult> Index()
        {
            var gestDocsContext = _context.Reunions.Include(r => r.TypeReunion);
            return View(await gestDocsContext.ToListAsync());
        }

        // GET: Reunions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reunion = await _context.Reunions
                .Include(r => r.TypeReunion)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reunion == null)
            {
                return NotFound();
            }

            return View(reunion);
        }

        // GET: Reunions/Create
        public IActionResult Create()
        {
            ViewData["TypeReunionID"] = new SelectList(_context.TypeReunions, "ID", "Libelle");
            return View();
        }

        // POST: Reunions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DateReunion,Remarque,TypeReunionID")] Reunion reunion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(reunion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeReunionID"] = new SelectList(_context.TypeReunions, "ID", "ID", reunion.TypeReunionID);
            return View(reunion);
        }

        // GET: Reunions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reunion = await _context.Reunions.FindAsync(id);
            if (reunion == null)
            {
                return NotFound();
            }
            ViewData["TypeReunionID"] = new SelectList(_context.TypeReunions, "ID", "ID", reunion.TypeReunionID);
            return View(reunion);
        }

        // POST: Reunions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DateReunion,Remarque,TypeReunionID")] Reunion reunion)
        {
            if (id != reunion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(reunion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ReunionExists(reunion.ID))
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
            ViewData["TypeReunionID"] = new SelectList(_context.TypeReunions, "ID", "ID", reunion.TypeReunionID);
            return View(reunion);
        }

        // GET: Reunions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reunion = await _context.Reunions
                .Include(r => r.TypeReunion)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (reunion == null)
            {
                return NotFound();
            }

            return View(reunion);
        }

        // POST: Reunions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var reunion = await _context.Reunions.FindAsync(id);
            _context.Reunions.Remove(reunion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ReunionExists(int id)
        {
            return _context.Reunions.Any(e => e.ID == id);
        }
    }
}
