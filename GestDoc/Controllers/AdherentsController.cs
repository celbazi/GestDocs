﻿using System;
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
    public class AdherentsController : Controller
    {
        private readonly GestDocsContext _context;

        public AdherentsController(GestDocsContext context)
        {
            _context = context;
        }

        // GET: Adherents
        public async Task<IActionResult> Index()
        {
            return View(await _context.Adherents.ToListAsync());
        }

        // GET: Adherents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adherent = await _context.Adherents
                .FirstOrDefaultAsync(m => m.ID == id);
            if (adherent == null)
            {
                return NotFound();
            }

            return View(adherent);
        }

        // GET: Adherents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Adherents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nom,Prenom,Photo,IsMember")] Adherent adherent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adherent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(adherent);
        }

        // GET: Adherents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adherent = await _context.Adherents.FindAsync(id);
            if (adherent == null)
            {
                return NotFound();
            }
            return View(adherent);
        }

        // POST: Adherents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nom,Prenom,Photo,IsMember")] Adherent adherent)
        {
            if (id != adherent.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adherent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdherentExists(adherent.ID))
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
            return View(adherent);
        }

        // GET: Adherents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adherent = await _context.Adherents
                .FirstOrDefaultAsync(m => m.ID == id);
            if (adherent == null)
            {
                return NotFound();
            }

            return View(adherent);
        }

        // POST: Adherents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adherent = await _context.Adherents.FindAsync(id);
            _context.Adherents.Remove(adherent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdherentExists(int id)
        {
            return _context.Adherents.Any(e => e.ID == id);
        }
    }
}
