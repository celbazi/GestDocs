using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestDoc.Data;
using GestDoc.Models;
using GestDoc.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;

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
            var gestDocsContext = _context.Reunions
                                        .Include(r => r.TypeReunion)
                                        .Include(r => r.Participants)
                                            .ThenInclude(r=>r.Adherent)
                                         .AsNoTracking();
           
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
                .Include(r => r.Participants)
                    .ThenInclude(r => r.Adherent)
                .Include(r => r.Documents)
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
            //ViewData["TypeReunionID"] = new SelectList(_context.TypeReunions, "ID", "Libelle");
            //return View();

           

            var reunion = new Reunion();
            reunion.Participants = new List<Participation>();
            PopulateParticipantsAssignes(reunion);
            ViewData["TypeReunionID"] = new SelectList(_context.TypeReunions, "ID", "Libelle");
            return View();

        }

        // POST: Reunions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DateReunion,Remarque,TypeReunionID")] Reunion reunion, string[] selectedAdherents,  List<IFormFile> files)
        {
            /*
            if (ModelState.IsValid)
            {
                _context.Add(reunion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeReunionID"] = new SelectList(_context.TypeReunions, "ID", "ID", reunion.TypeReunionID);
            return View(reunion);
            */
            if (selectedAdherents != null)
            {
                reunion.Participants = new List<Participation>();
                foreach (var adherent in selectedAdherents)
                {
                    var adherentToAdd = new Participation { ReunionID = reunion.ID, AdherentID = int.Parse(adherent) };
                    reunion.Participants.Add(adherentToAdd);
                }
                reunion.Documents = new List<Document>();
                foreach (var file in files)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await file.CopyToAsync(memoryStream);
                        
                        // Upload the file if less than 2 MB
                        if (memoryStream.Length < 2097152)
                        {
                            var documentToAdd = new Document { ReunionID = reunion.ID, URL = file.FileName,MimeType=file.ContentType, Content = memoryStream.ToArray() };
                            reunion.Documents.Add(documentToAdd);

                        }
                        else
                        {
                            ModelState.AddModelError("File", "The file is too large.");
                        }
                    }

                    
                }
            }
            if (ModelState.IsValid)
            {
                _context.Add(reunion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateParticipantsAssignes(reunion);
            return View(reunion);
        }

        // GET: Reunions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var reunion = await _context.Reunions
                                    .Include(r => r.Participants)
                                    .ThenInclude(r => r.Adherent)
                                    .Include(r => r.Documents)
                                    .FirstOrDefaultAsync(m => m.ID == id);
                //.FindAsync(id);
                                        
            if (reunion == null)
            {
                return NotFound();
            }
            PopulateParticipantsAssignes(reunion);
            ViewData["TypeReunionID"] = new SelectList(_context.TypeReunions, "ID", "Libelle", reunion.TypeReunionID);
            return View(reunion);
        }

        // POST: Reunions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,DateReunion,Remarque,TypeReunionID")] Reunion reunion, string[] selectedAdherents, List<IFormFile> files, string[] selectedDocuments)
        {
          
            if (id == null)
            {
                return NotFound();
            }

            var reunionToUpdate = await _context.Reunions
                                    .Include(r => r.Participants)
                                    .ThenInclude(r => r.Adherent)
                                    .Include(r => r.Documents)
                                    .FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<Reunion>(
                reunionToUpdate,
                "",
                i => i.DateReunion, i => i.Remarque, i => i.Remarque, i => i.TypeReunion))
            {
                //if (String.IsNullOrWhiteSpace(reunionToUpdate.OfficeAssignment?.Location))
                //{
                //    reunionToUpdate.OfficeAssignment = null;
                //}
                UpdateReunionParticipants(selectedAdherents, reunionToUpdate);
                UpdateReunionDocuments(selectedDocuments, reunionToUpdate,  files);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateReunionParticipants(selectedAdherents, reunionToUpdate);
            PopulateParticipantsAssignes(reunionToUpdate);
            return View(reunionToUpdate);
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
        private void PopulateParticipantsAssignes(Reunion Reunion)
        {
            var allAdherents = _context.Adherents;
            var reunionParticipants = new HashSet<int>(Reunion.Participants.Select(c => c.AdherentID));
            var viewModel = new List<ParticipantsAssignes>();
            foreach (var adherent in allAdherents)
            {
                viewModel.Add(new ParticipantsAssignes
                {
                    AdherentID = adherent.ID,
                    Nom = adherent.Nom,
                    Assigned = reunionParticipants.Contains(adherent.ID)
                });
            }
            ViewData["Adherents"] = viewModel;
        }
        private void UpdateReunionParticipants(string[] selectedAdherents, Reunion reunionToUpdate)
        {
            if (selectedAdherents == null)
            {
                reunionToUpdate.Participants = new List<Participation>();
                return;
            }

            var selectedAdherentsHS = new HashSet<string>(selectedAdherents);
            var reunionParticipants = new HashSet<int>
                (reunionToUpdate.Participants.Select(c => c.Adherent.ID));
            foreach (var adherent in _context.Adherents)
            {
                if (selectedAdherentsHS.Contains(adherent.ID.ToString()))
                {
                    if (!reunionParticipants.Contains(adherent.ID))
                    {
                        reunionToUpdate.Participants.Add(new Participation { ReunionID = reunionToUpdate.ID, AdherentID = adherent.ID });
                    }
                }
                else
                {

                    if (reunionParticipants.Contains(adherent.ID))
                    {
                        Participation adherentToRemove = reunionToUpdate.Participants.FirstOrDefault(i => i.AdherentID == adherent.ID);
                        _context.Remove(adherentToRemove);
                    }
                }
            }
        }
        private void UpdateReunionDocuments(string[] selectedDocuments, Reunion reunionToUpdate, List<IFormFile> files)
        {
            if (selectedDocuments == null)
            {
                reunionToUpdate.Documents = new List<Document>();
              //  return;
            }

            var selectedDocumentHS = new HashSet<string>(selectedDocuments);
            var reunionDocuments = new HashSet<int>
                (reunionToUpdate.Documents.Select(c => c.ID));
            foreach (var document in reunionDocuments)
            {
                if (!selectedDocumentHS.Contains(document.ToString()))
                {
                     Document documentToRemove = reunionToUpdate.Documents.FirstOrDefault(i => i.ID == document);
                    _context.Remove(documentToRemove);
                }
               
            }
            foreach (var file in files)
            {
                using (var memoryStream = new MemoryStream())
                {
                     file.CopyToAsync(memoryStream);
                    // Upload the file if less than 2 MB
                    if (memoryStream.Length < 2097152)
                    {
                        var documentToAdd = new Document { ReunionID = reunionToUpdate.ID, URL = file.FileName, MimeType = file.ContentType, Content = memoryStream.ToArray() };
                        reunionToUpdate.Documents.Add(documentToAdd);

                    }
                    else
                    {
                        ModelState.AddModelError("File", "The file is too large.");
                    }
                }


            }
        }

        public FileContentResult DownloadFile(int Id)
        {
                var download = _context.Documents.Where(d => d.ID == Id).FirstOrDefault();
                if (download != null)
                {
                    string fileName = download.URL;
                    return File(download.Content, download.MimeType, fileName);
                }
                else
                {
                    return null;
                }
            
        }
    }
}
