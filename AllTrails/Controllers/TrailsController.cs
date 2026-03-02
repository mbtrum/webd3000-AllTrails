using AllTrails.Data;
using AllTrails.Models;
using AllTrails.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllTrails.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TrailsController : Controller
    {
        private readonly AllTrailsContext _context;
        private readonly AzureBlobService _blobService;

        public TrailsController(AllTrailsContext context, AzureBlobService azureBlobService)
        {
            _context = context;
            _blobService = azureBlobService;
        }

        // GET: Trails
        public async Task<IActionResult> Index()
        {
            return View(await _context.Trail.ToListAsync());
        }

        // GET: Trails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trail = await _context.Trail
                .FirstOrDefaultAsync(m => m.Id == id);


            if (trail == null)
            {
                return NotFound();
            }

            return View(trail);
        }

        // GET: Trails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Trails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,ImageUrl,LengthKm,ElevationGainMetres,EstimatedDuration")] Trail trail, IFormFile? ImageFile)
        {
            if (ModelState.IsValid)
            {
                // upload image file
                if (ImageFile != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName); // f6f4d054-36fb-4d51-a5ac-d7229eb1e093.png
                    string blobURL = await _blobService.UploadFileAsync(ImageFile, filename);

                    trail.ImageUrl = blobURL;
                }

                // Save in datbase
                _context.Add(trail);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Index));
            }
            return View(trail);
        }

        // GET: Trails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trail = await _context.Trail.FindAsync(id);

            if (trail == null)
            {
                return NotFound();
            }

            return View(trail);
        }

        // POST: Trails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,ImageUrl,LengthKm,ElevationGainMetres,EstimatedDuration")] Trail trail, IFormFile? ImageFile)
        {
            if (id != trail.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // upload image file
                if (ImageFile != null)
                {
                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName); // f6f4d054-36fb-4d51-a5ac-d7229eb1e093.png
                    string blobURL = await _blobService.UploadFileAsync(ImageFile, filename);

                    trail.ImageUrl = blobURL;
                }

                try
                {
                    _context.Update(trail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrailExists(trail.Id))
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

            return View(trail);
        }

        // GET: Trails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trail = await _context.Trail
                .FirstOrDefaultAsync(m => m.Id == id);

            if (trail == null)
            {
                return NotFound();
            }

            return View(trail);
        }

        // POST: Trails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trail = await _context.Trail.FindAsync(id);
            
            if (trail != null)
            {
                _context.Trail.Remove(trail);
            }

            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool TrailExists(int id)
        {
            return _context.Trail.Any(e => e.Id == id);
        }
    }
}
