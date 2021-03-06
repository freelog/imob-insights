using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using imob_insights.Data;
using imob_insights.Models;

namespace imob_insights.Controllers
{
    [Route("api/[controller]")]
    public class Table1Controller : Controller
    {
        private readonly TestContext _context;

        public Table1Controller(TestContext context)
        {
            var db = context.Database.GetDbConnection();
            db.Open();//ca marche mais il faut faire le open ....


            _context = context;    
        }

        // GET: Table1
        [HttpGet(Name = "GetList")]
        public async Task<IActionResult> GetList()
        {
            var result = await _context.Table1.ToListAsync();
            return new ObjectResult(result);
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Table1.ToListAsync());
        }

        // GET: Table1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var table1 = await _context.Table1
                .SingleOrDefaultAsync(m => m.Id == id);
            if (table1 == null)
            {
                return NotFound();
            }

            return View(table1);
        }

        // GET: Table1/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Table1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,text")] Table1 table1)
        {
            if (ModelState.IsValid)
            {
                _context.Add(table1);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(table1);
        }

        // GET: Table1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var table1 = await _context.Table1.SingleOrDefaultAsync(m => m.Id == id);
            if (table1 == null)
            {
                return NotFound();
            }
            return View(table1);
        }

        // POST: Table1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,text")] Table1 table1)
        {
            if (id != table1.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(table1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Table1Exists(table1.Id))
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
            return View(table1);
        }

        // GET: Table1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var table1 = await _context.Table1
                .SingleOrDefaultAsync(m => m.Id == id);
            if (table1 == null)
            {
                return NotFound();
            }

            return View(table1);
        }

        // POST: Table1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var table1 = await _context.Table1.SingleOrDefaultAsync(m => m.Id == id);
            _context.Table1.Remove(table1);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool Table1Exists(int id)
        {
            return _context.Table1.Any(e => e.Id == id);
        }
    }
}
