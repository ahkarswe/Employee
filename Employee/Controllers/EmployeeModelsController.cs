using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Employee.Data;
using Employee.Models;
using Microsoft.AspNetCore.Authorization;

namespace Employee.Controllers
{
    public class EmployeeModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmployeeModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeModels
        public async Task<IActionResult> Index()
        {
              return _context.EmployeeModel != null ? 
                          View(await _context.EmployeeModel.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.EmployeeModel'  is null.");
        }

        // GET: EmployeeModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeModel == null)
            {
                return NotFound();
            }

            var EmployeeModel = await _context.EmployeeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (EmployeeModel == null)
            {
                return NotFound();
            }

            return View(EmployeeModel);
        }
        [Authorize]
        // GET: EmployeeModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NRC_No,Join_Date,Employee_No,Address,City")] EmployeeModel EmployeeModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(EmployeeModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(EmployeeModel);
        }

        // GET: EmployeeModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeModel == null)
            {
                return NotFound();
            }

            var EmployeeModel = await _context.EmployeeModel.FindAsync(id);
            if (EmployeeModel == null)
            {
                return NotFound();
            }
            return View(EmployeeModel);
        }

        // POST: EmployeeModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NRC_No,Join_Date,Employee_No,Address,City")] EmployeeModel EmployeeModel)
        {
            if (id != EmployeeModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(EmployeeModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeModelExists(EmployeeModel.Id))
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
            return View(EmployeeModel);
        }

        // GET: EmployeeModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeModel == null)
            {
                return NotFound();
            }

            var EmployeeModel = await _context.EmployeeModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (EmployeeModel == null)
            {
                return NotFound();
            }

            return View(EmployeeModel);
        }

        // POST: EmployeeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeModel == null)
            {
                return Problem("Entity set 'ApplicationDbContext.EmployeeModel'  is null.");
            }
            var EmployeeModel = await _context.EmployeeModel.FindAsync(id);
            if (EmployeeModel != null)
            {
                _context.EmployeeModel.Remove(EmployeeModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeModelExists(int id)
        {
          return (_context.EmployeeModel?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
