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
    public class TypeReunionsController : Controller
    {
        private readonly GestDocsContext _context;

        public TypeReunionsController(GestDocsContext context)
        {
            _context = context;
        }

        // GET: TypeReunions
        public async Task<IActionResult> Index()
        {
            return View(await _context.TypeReunions.ToListAsync());
        }

        // GET: TypeReunions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeReunion = await _context.TypeReunions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (typeReunion == null)
            {
                return NotFound();
            }

            return View(typeReunion);
        }

        // GET: TypeReunions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TypeReunions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Libelle")] TypeReunion typeReunion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(typeReunion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(typeReunion);
        }

        // GET: TypeReunions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeReunion = await _context.TypeReunions.FindAsync(id);
            if (typeReunion == null)
            {
                return NotFound();
            }
            return View(typeReunion);
        }

        // POST: TypeReunions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Libelle")] TypeReunion typeReunion)
        {
            if (id != typeReunion.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(typeReunion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TypeReunionExists(typeReunion.ID))
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
            return View(typeReunion);
        }

        // GET: TypeReunions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var typeReunion = await _context.TypeReunions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (typeReunion == null)
            {
                return NotFound();
            }

            return View(typeReunion);
        }

        // POST: TypeReunions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var typeReunion = await _context.TypeReunions.FindAsync(id);
            _context.TypeReunions.Remove(typeReunion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TypeReunionExists(int id)
        {
            return _context.TypeReunions.Any(e => e.ID == id);
        }
    }
}
