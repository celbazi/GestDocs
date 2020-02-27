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
    public class ParticipationsController : Controller
    {
        private readonly GestDocsContext _context;

        public ParticipationsController(GestDocsContext context)
        {
            _context = context;
        }

        // GET: Participations
        public async Task<IActionResult> Index()
        {
            var gestDocsContext = _context.Participations.Include(p => p.Adherent).Include(p => p.Reunion);
            return View(await gestDocsContext.ToListAsync());
        }

        // GET: Participations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participation = await _context.Participations
                .Include(p => p.Adherent)
                .Include(p => p.Reunion)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (participation == null)
            {
                return NotFound();
            }

            return View(participation);
        }

        // GET: Participations/Create
        public IActionResult Create()
        {
            ViewData["AdherentID"] = new SelectList(_context.Adherents, "ID", "ID");
            ViewData["ReunionID"] = new SelectList(_context.Reunions, "ID", "ID");
            return View();
        }

        // POST: Participations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,ReunionID,AdherentID")] Participation participation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(participation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AdherentID"] = new SelectList(_context.Adherents, "ID", "ID", participation.AdherentID);
            ViewData["ReunionID"] = new SelectList(_context.Reunions, "ID", "ID", participation.ReunionID);
            return View(participation);
        }

        // GET: Participations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participation = await _context.Participations.FindAsync(id);
            if (participation == null)
            {
                return NotFound();
            }
            ViewData["AdherentID"] = new SelectList(_context.Adherents, "ID", "ID", participation.AdherentID);
            ViewData["ReunionID"] = new SelectList(_context.Reunions, "ID", "ID", participation.ReunionID);
            return View(participation);
        }

        // POST: Participations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,ReunionID,AdherentID")] Participation participation)
        {
            if (id != participation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(participation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParticipationExists(participation.ID))
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
            ViewData["AdherentID"] = new SelectList(_context.Adherents, "ID", "ID", participation.AdherentID);
            ViewData["ReunionID"] = new SelectList(_context.Reunions, "ID", "ID", participation.ReunionID);
            return View(participation);
        }

        // GET: Participations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var participation = await _context.Participations
                .Include(p => p.Adherent)
                .Include(p => p.Reunion)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (participation == null)
            {
                return NotFound();
            }

            return View(participation);
        }

        // POST: Participations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var participation = await _context.Participations.FindAsync(id);
            _context.Participations.Remove(participation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ParticipationExists(int id)
        {
            return _context.Participations.Any(e => e.ID == id);
        }
    }
}
