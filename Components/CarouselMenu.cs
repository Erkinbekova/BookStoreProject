using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlinePizzaWebApplication.Data;
using OnlinePizzaWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePizzaWebApplication.Components
{
    public class CarouselMenu : ViewComponent
    {
        private readonly AppDbContext _context;
        public CarouselMenu(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var books = await _context.Books.Where(x => x.IsBookOfTheWeek).ToListAsync();
            return View(books);
        }
    }
}
