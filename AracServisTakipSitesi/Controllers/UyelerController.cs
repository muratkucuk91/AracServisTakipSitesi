
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AracServisTakipSitesi.Models;

using Microsoft.EntityFrameworkCore;

namespace AracServisTakipSitesi.Controllers
{
    public class UyelerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UyelerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Uyelers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Uyeler.ToListAsync());
        }

        // GET: Uyelers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uyeler = await _context.Uyeler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uyeler == null)
            {
                return NotFound();
            }

            return View(uyeler);
        }

        // GET: Uyelers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Uyelers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ad,Adres,Sehir,PostaKodu,Telefon")] Uyeler uyeler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uyeler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(uyeler);
        }

        // GET: Uyelers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uyeler = await _context.Uyeler.FindAsync(id);
            if (uyeler == null)
            {
                return NotFound();
            }
            return View(uyeler);
        }

        // POST: Uyelers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ad,Adres,Sehir,PostaKodu,Telefon")] Uyeler uyeler)
        {
            if (id != uyeler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uyeler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UyelerExists(uyeler.Id))
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
            return View(uyeler);
        }

        // GET: Uyelers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uyeler = await _context.Uyeler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (uyeler == null)
            {
                return NotFound();
            }

            return View(uyeler);
        }

        // POST: Uyelers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uyeler = await _context.Uyeler.FindAsync(id);
            _context.Uyeler.Remove(uyeler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UyelerExists(int id)
        {
            return _context.Uyeler.Any(e => e.Id == id);
        }
    }
}
