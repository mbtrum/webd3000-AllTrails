using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AllTrails.Data;
using AllTrails.Models;

namespace AllTrails.Controllers
{
    public class TripReportsController : Controller
    {
        private readonly AllTrailsContext _context;

        public TripReportsController(AllTrailsContext context)
        {
            _context = context;
        }

        // GET: TripReports
        public async Task<IActionResult> Index()
        {
            var allTrailsContext = _context.TripReport.Include(t => t.Trail);
            return View(await allTrailsContext.ToListAsync());
        }

        // GET: TripReports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripReport = await _context.TripReport
                .Include(t => t.Trail)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tripReport == null)
            {
                return NotFound();
            }

            return View(tripReport);
        }

        // GET: TripReports/Create
        public IActionResult Create(int? trailId) // optional trail id
        {
            ViewData["TrailId"] = new SelectList(_context.Trail, "Id", "Name", trailId); 

            return View();
        }

        // POST: TripReports/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rating,Description,TrailId")] TripReport tripReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tripReport);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["TrailId"] = new SelectList(_context.Trail, "Id", "Name", tripReport.TrailId);

            return View(tripReport);
        }

        // GET: TripReports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripReport = await _context.TripReport
                .Include(t => t.Trail)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (tripReport == null)
            {
                return NotFound();
            }
            
            ViewData["TrailId"] = new SelectList(_context.Trail, "Id", "Name", tripReport.TrailId);
            
            return View(tripReport);
        }

        // POST: TripReports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rating,Description,CreatedDate,TrailId")] TripReport tripReport)
        {
            if (id != tripReport.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tripReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TripReportExists(tripReport.Id))
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

            ViewData["TrailId"] = new SelectList(_context.Trail, "Id", "Name", tripReport.TrailId);
            
            return View(tripReport);
        }

        // GET: TripReports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tripReport = await _context.TripReport
                .Include(t => t.Trail)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (tripReport == null)
            {
                return NotFound();
            }

            return View(tripReport);
        }

        // POST: TripReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tripReport = await _context.TripReport.FindAsync(id);
            
            if (tripReport != null)
            {
                _context.TripReport.Remove(tripReport);
            }

            await _context.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        private bool TripReportExists(int id)
        {
            return _context.TripReport.Any(e => e.Id == id);
        }
    }
}
