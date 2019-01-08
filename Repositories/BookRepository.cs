using Microsoft.EntityFrameworkCore;
using OnlinePizzaWebApplication.Data;
using OnlinePizzaWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePizzaWebApplication.Repositories
{
    public class BookRepository : IBookRepository   
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Books> Books => _context.Books.Include(p => p.Category); //include here

        public IEnumerable<Books> BooksOfTheWeek => _context.Books.Where(p => p.IsBookOfTheWeek).Include(p => p.Category);

        // public IEnumerable<Books> Books => throw new NotImplementedException();

        // public IEnumerable<Books> BooksOfTheWeek => throw new NotImplementedException();

        public void Add(Books book)
        {
            _context.Add(book);
        }

        public IEnumerable<Books> GetAll()
        {
            return _context.Books.ToList();
        }

        public async Task<IEnumerable<Books>> GetAllAsync()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<IEnumerable<Books>> GetAllIncludedAsync()
        {
            return await _context.Books.Include(p => p.Category).ToListAsync();
        }

        public IEnumerable<Books> GetAllIncluded()
        {
            return _context.Books.Include(p => p.Category).ToList();
        }

        public Books GetById(int? id)
        {
            return _context.Books.FirstOrDefault(p => p.Id == id);
        }

        public async Task<Books> GetByIdAsync(int? id)
        {
            return await _context.Books.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Books GetByIdIncluded(int? id)
        {
            return _context.Books.Include(p => p.Category).FirstOrDefault(p => p.Id == id);
        }

        public async Task<Books> GetByIdIncludedAsync(int? id)
        {
            return await _context.Books.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public bool Exists(int id)
        {
            return _context.Books.Any(p => p.Id == id);
        }

        public void Remove(Books book)
        {
            _context.Remove(book);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void Update(Books book)
        {
            _context.Update(book);
        }

    }
}
