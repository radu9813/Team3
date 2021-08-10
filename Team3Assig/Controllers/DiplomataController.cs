using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team3Assig.Data;
using Team3Assig.Models;

namespace Team3Assig.Controllers
{
    public class DiplomataController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DiplomataController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Diplomata
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Diploma.Include(d => d.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Diplomata/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diploma = await _context.Diploma
                .Include(d => d.Student)
                .FirstOrDefaultAsync(m => m.DiplomaId == id);
            if (diploma == null)
            {
                return NotFound();
            }

            return View(diploma);
        }

        // GET: Diplomata/Create
        public IActionResult Create()
        {
            ViewData["DiplomaId"] = new SelectList(_context.Student, "StudentId", "StudentId");
            return View();
        }

        // POST: Diplomata/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DiplomaId,Thesis,Abstract,Completeness,Supervisor")] Diploma diploma)
        {
            if (ModelState.IsValid)
            {
                _context.Add(diploma);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DiplomaId"] = new SelectList(_context.Student, "StudentId", "StudentId", diploma.DiplomaId);
            return View(diploma);
        }

        // GET: Diplomata/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diploma = await _context.Diploma.FindAsync(id);
            if (diploma == null)
            {
                return NotFound();
            }
            ViewData["DiplomaId"] = new SelectList(_context.Student, "StudentId", "StudentId", diploma.DiplomaId);
            return View(diploma);
        }

        // POST: Diplomata/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DiplomaId,Thesis,Abstract,Completeness,Supervisor")] Diploma diploma)
        {
            if (id != diploma.DiplomaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(diploma);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DiplomaExists(diploma.DiplomaId))
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
            ViewData["DiplomaId"] = new SelectList(_context.Student, "StudentId", "StudentId", diploma.DiplomaId);
            return View(diploma);
        }

        // GET: Diplomata/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var diploma = await _context.Diploma
                .Include(d => d.Student)
                .FirstOrDefaultAsync(m => m.DiplomaId == id);
            if (diploma == null)
            {
                return NotFound();
            }

            return View(diploma);
        }

        // POST: Diplomata/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var diploma = await _context.Diploma.FindAsync(id);
            _context.Diploma.Remove(diploma);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DiplomaExists(int id)
        {
            return _context.Diploma.Any(e => e.DiplomaId == id);
        }
    }
}
