using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SportLogger.Data;
using SportLogger.Models;
using Microsoft.AspNetCore.Authorization;

namespace SportLogger.Controllers
{
    public class SkiDayController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SkiDayController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: SkiDay
        public async Task<IActionResult> Index()
        {
            return View(await _context.SkiDay.OrderByDescending(x => x.SkiDate).ToListAsync());
        }

        // GET: SkiDay/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skiDay = await _context.SkiDay
                .SingleOrDefaultAsync(m => m.Id == id);
            if (skiDay == null)
            {
                return NotFound();
            }

            return View(skiDay);
        }

        public List<ResortReference> GetResortList()
        {
            List<ResortReference> resortList = new List<ResortReference>();

            resortList = (from name in _context.ResortReference select name).ToList();

            return resortList;
        }

        // GET: SkiDay/Create
        public IActionResult Create()
        {
            List<ResortReference> resortList = GetResortList();

            //resortList.Insert(0, new ResortReference { ResortName = "Select a Resort" });

            ViewBag.ResortList = new SelectList(resortList, "ResortName", "ResortName");

            return View();
        }

        // POST: SkiDay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SkiDate,Resort,Vertical,Partners,NewSnow24,NewSnow72,Temperature,Comments")] SkiDay skiDay)
        {
            if (SkiDateExists(skiDay.SkiDate))
            {
                var msg = string.Format("Ski date {0} already exists", skiDay.SkiDate.ToString("MM/dd/yyyy"));

                List<ResortReference> resortList = GetResortList();

                ViewBag.ResortList = new SelectList(resortList, "ResortName", "ResortName");

                ModelState.AddModelError("SkiDate", msg);
            }

            if (ModelState.IsValid)
            {
                _context.Add(skiDay);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(skiDay);
        }

        // GET: SkiDay/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skiDay = await _context.SkiDay.SingleOrDefaultAsync(m => m.Id == id);
            if (skiDay == null)
            {
                return NotFound();
            }
            var r = skiDay.Resort;
            
            ViewBag.ResortList = new SelectList(GetResortList(), "ResortName", "ResortName", r);

            return View(skiDay);
        }

        // POST: SkiDay/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SkiDate,Resort,Vertical,Partners,NewSnow24,NewSnow72,Temperature,Comments")] SkiDay skiDay)
        {
            if (id != skiDay.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skiDay);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkiDayExists(skiDay.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(skiDay);
        }

        // GET: SkiDay/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skiDay = await _context.SkiDay
                .SingleOrDefaultAsync(m => m.Id == id);
            if (skiDay == null)
            {
                return NotFound();
            }

            return View(skiDay);
        }

        // POST: SkiDay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skiDay = await _context.SkiDay.SingleOrDefaultAsync(m => m.Id == id);
            _context.SkiDay.Remove(skiDay);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool SkiDayExists(int id)
        {
            return _context.SkiDay.Any(e => e.Id == id);
        }

        private bool SkiDateExists(DateTime date)
        {
            return _context.SkiDay.Any(e => e.SkiDate == date);
        }
    }
}
