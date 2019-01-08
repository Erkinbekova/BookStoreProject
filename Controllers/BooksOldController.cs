using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlinePizzaWebApplication.Data;
using OnlinePizzaWebApplication.Models;
using OnlinePizzaWebApplication.Repositories;

namespace OnlinePizzaWebApplication.Controllers
{
    public class PizzasOldController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBookRepository _bookRepo;

        public PizzasOldController(AppDbContext context, IBookRepository bookRepo)
        {
            _context = context;
            _bookRepo = bookRepo;
        }

        // GET: Pizzas
        public async Task<IActionResult> Index()
        {
            return View(await _bookRepo.GetAllAsync());
        }

        // GET: Pizzas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookRepo.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Pizzas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,ImageUrl,IsBookOfTheWeek")] Books book)
        {
            if (ModelState.IsValid)
            {
                _bookRepo.Add(book);
                await _bookRepo.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Pizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookRepo.GetByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,ImageUrl,IsBookOfTheWeek")] Books book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookRepo.Update(book);
                    await _bookRepo.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            return View(book);
        }

        // GET: Pizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _bookRepo.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _bookRepo.GetByIdAsync(id);
            _bookRepo.Remove(book);
            await _bookRepo.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BookExists(int id)
        {
            return _bookRepo.Exists(id);
        }
    }
}
