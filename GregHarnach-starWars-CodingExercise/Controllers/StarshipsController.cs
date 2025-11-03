using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GregHarnach_starWars_CodingExercise.Data;
using GregHarnach_starWars_CodingExercise.Models;

namespace GregHarnach_starWars_CodingExercise.Controllers
{
    public class StarshipsController : Controller
    {
        private readonly AppDbContext _db;
        private readonly ILogger<StarshipsController> _logger;
             

        public StarshipsController(AppDbContext db, ILogger<StarshipsController> logger)
        {
            _db = db;
            _logger = logger;
        }

        // GET: /Starships
        public async Task<IActionResult> Index()
        {
            try
            {
                var items = await _db.Starships.AsNoTracking().ToListAsync();
                return View(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Starships index");
                TempData["Error"] = "Unexpected error loading starships.";
                return View(Enumerable.Empty<Starship>());
            }
        }

        // GET: /Starships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                _logger.LogWarning("Details called with null id");
                return NotFound();
            }

            try
            {
                var starship = await _db.Starships
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (starship == null)
                {
                    _logger.LogInformation("Starship {Id} not found", id);
                    return NotFound();
                }

                return View(starship);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting details for Starship {Id}", id);
                TempData["Error"] = "Unexpected error loading starship details.";
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: /Starships/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Starships/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Model,Manufacturer,CostInCredits,Length,MaxAtmospheringSpeed,Crew,Passengers,CargoCapacity,Consumables,HyperdriveRating,MGLT,StarshipClass,PilotsCsv,FilmsCsv,SwapiUrl")] Starship starship)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogDebug("Create ModelState invalid: {@ModelState}", ModelState);
                return View(starship);
            }

            try
            {
                _db.Add(starship);
                await _db.SaveChangesAsync();
                TempData["Message"] = "Starship created.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error creating starship {@Starship}", starship);
                ModelState.AddModelError(string.Empty, "Could not save starship. Please try again.");
                return View(starship);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating starship {@Starship}", starship);
                ModelState.AddModelError(string.Empty, "Unexpected error creating starship.");
                return View(starship);
            }
        }

        // GET: /Starships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var starship = await _db.Starships.FindAsync(id);
                if (starship == null) return NotFound();
                return View(starship);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Edit for Starship {Id}", id);
                TempData["Error"] = "Unexpected error loading starship.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: /Starships/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Model,Manufacturer,CostInCredits,Length,MaxAtmospheringSpeed,Crew,Passengers,CargoCapacity,Consumables,HyperdriveRating,MGLT,StarshipClass,PilotsCsv,FilmsCsv,SwapiUrl")] Starship starship)
        {
            if (id != starship.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                _logger.LogDebug("Edit ModelState invalid for {Id}", id);
                return View(starship);
            }

            try
            {
                _db.Entry(starship).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                TempData["Message"] = "Starship updated.";
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateConcurrencyException cx)
            {
                if (!await _db.Starships.AnyAsync(e => e.Id == id))
                {
                    _logger.LogWarning("Concurrency edit failed: Starship {Id} no longer exists", id);
                    return NotFound();
                }

                _logger.LogError(cx, "Concurrency error updating Starship {Id}", id);
                ModelState.AddModelError(string.Empty, "The record you attempted to edit was modified by another user. Try again.");
                return View(starship);
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error updating Starship {Id}", id);
                ModelState.AddModelError(string.Empty, "Could not save changes. Please try again.");
                return View(starship);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating Starship {Id}", id);
                ModelState.AddModelError(string.Empty, "Unexpected error updating starship.");
                return View(starship);
            }
        }

        // GET: /Starships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            try
            {
                var starship = await _db.Starships
                    .AsNoTracking()
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (starship == null) return NotFound();

                return View(starship);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading Delete for Starship {Id}", id);
                TempData["Error"] = "Unexpected error loading starship.";
                return RedirectToAction(nameof(Index));
            }
        }

        // POST: /Starships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var starship = await _db.Starships.FindAsync(id);
                if (starship != null)
                {
                    _db.Starships.Remove(starship);
                    await _db.SaveChangesAsync();
                    TempData["Message"] = "Starship deleted.";
                }
                else
                {
                    _logger.LogWarning("DeleteConfirmed: Starship {Id} not found", id);
                }
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogError(dbEx, "Database error deleting Starship {Id}", id);
                TempData["Error"] = "Could not delete starship (database constraint?).";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting Starship {Id}", id);
                TempData["Error"] = "Unexpected error deleting starship.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
