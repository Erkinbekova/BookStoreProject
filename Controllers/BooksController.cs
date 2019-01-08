using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlinePizzaWebApplication.Models;
using OnlinePizzaWebApplication.Repositories;
using Microsoft.AspNetCore.Authorization;
using OnlinePizzaWebApplication.ViewModels;
using Newtonsoft.Json;
using OnlinePizzaWebApplication.Data;

namespace OnlinePizzaWebApplication.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BooksController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IBookRepository _bookRepo;
        private readonly ICategoryRepository _categoryRepo;

        public BooksController(AppDbContext context, IBookRepository bookRepo, ICategoryRepository categoryRepo)
        {
            _context = context;
            _bookRepo = bookRepo;
            _categoryRepo = categoryRepo;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _bookRepo.GetAllIncludedAsync());
        }

        // GET: Books
        [AllowAnonymous]
        public async Task<IActionResult> ListAll()
        {
            var model = new SearchBooksViewModel()
            {
                BookList = await _bookRepo.GetAllIncludedAsync(),
                SearchText = null
            };

            return View(model);
        }

        private async Task<List<Books>> GetBookSearchList(string userInput)
        {
            userInput = userInput.ToLower().Trim();

            var result = _context.Books.Include(p => p.Category)
                .Where(p => p
                    .Name.ToLower().Contains(userInput))
                    .Select(p => p).OrderBy(p => p.Name);

            return await result.ToListAsync();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> AjaxSearchList(string searchString)
        {
            var bookList = await GetBookSearchList(searchString);
            
            return PartialView(bookList);
        }

        // GET: Books
        [AllowAnonymous]
        public async Task<IActionResult> ListCategory(string categoryName)
        {
            bool categoryExtist = _context.Categories.Any(c => c.Name == categoryName);
            if (!categoryExtist)
            {
                return NotFound();
            }

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == categoryName);

            if (category == null)
            {
                return NotFound();
            }

            bool anyBooks = await _context.Books.AnyAsync(x => x.Category == category);
            if (!anyBooks)
            {
                return NotFound($"No Books were found in the category: {categoryName}");
            }

            var books = _context.Books.Where(x => x.Category == category)
                .Include(x => x.Category);

            ViewBag.CurrentCategory = category.Name;
            return View(books);
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _bookRepo.GetByIdIncludedAsync(id);

            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Books/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> DisplayDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _bookRepo.GetByIdIncludedAsync(id);

            

           // var listOfReviews = await _context.Reviews.Where(x => x.PizzaId == id).Select(x => x).ToListAsync();
           // ViewBag.Reviews = listOfReviews;
           // double score;
           // if (_context.Reviews.Any(x => x.PizzaId == id))
            // {
            //    // var review = _context.Reviews.Where(x => x.PizzaId == id);
            //    // score = review.Average(x => x.Grade);
            //    // score = Math.Round(score, 2);
            // }
            // else
            // {
            //     score = 0;
            // }
           // ViewBag.AverageReviewScore = score;

            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // GET: Books
        [AllowAnonymous]
        public async Task<IActionResult> SearchBooks()
        {
            var model = new SearchBooksViewModel()
            {
                BookList = await _bookRepo.GetAllIncludedAsync(),
                SearchText = null
            };

            return View(model);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["CategoriesId"] = new SelectList(_categoryRepo.GetAll(), "Id", "Name");
            return View();
        }

        // POST: Pizzas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description,ImageUrl,IsBookOfTheWeek,CategoriesId")] Books books)
        {
            if (ModelState.IsValid)
            {
                _bookRepo.Add(books);
                await _bookRepo.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CategoriesId"] = new SelectList(_categoryRepo.GetAll(), "Id", "Name", books.CategoriesId);
            return View(books);
        }

        // GET: Pizzas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _bookRepo.GetByIdAsync(id);

            if (books == null)
            {
                return NotFound();
            }
            ViewData["CategoriesId"] = new SelectList(_categoryRepo.GetAll(), "Id", "Name", books.CategoriesId);
            return View(books);
        }

        // POST: Pizzas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description,ImageUrl,IsBookOfTheWeek,CategoriesId")] Books books)
        {
            if (id != books.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _bookRepo.Update(books);
                    await _bookRepo.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BooksExists(books.Id))
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
            ViewData["CategoriesId"] = new SelectList(_categoryRepo.GetAll(), "Id", "Name", books.CategoriesId);
            return View(books);
        }

        // GET: Pizzas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var books = await _bookRepo.GetByIdIncludedAsync(id);

            if (books == null)
            {
                return NotFound();
            }

            return View(books);
        }

        // POST: Pizzas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var books = await _bookRepo.GetByIdAsync(id);
            _bookRepo.Remove(books);
            await _bookRepo.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BooksExists(int id)
        {
            return _bookRepo.Exists(id);
        }
    }
}
