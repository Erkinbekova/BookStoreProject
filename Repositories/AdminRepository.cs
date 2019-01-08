using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlinePizzaWebApplication.Data;
using OnlinePizzaWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePizzaWebApplication.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;
        private readonly IServiceProvider _serviceProvider;

        public AdminRepository(AppDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task SeedDatabaseAsync()
        {
            var _roleManager = _serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = _serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

            var cat1 = new Categories { Name = "Fantastic", Description = "The BookShop's Fantastic books." };
            var cat2 = new Categories { Name = "Horror", Description = "The BookShop's Horror books." };
            var cat3 = new Categories { Name = "Poetry", Description = "The BookShop's Poetry books." };

            var cats = new List<Categories>()
            {
                cat1, cat2, cat3
            };

            var piz1 = new Books { Name = "Harry Potter and Cursed child", Price = 7000.00M, Category = cat1, Description = "The official playscript of the original West End production of Harry Potter and the Cursed Child.", ImageUrl = "https://cdn.shopify.com/s/files/1/1153/0428/products/9781781105528_d68ae96b-ac6c-4b02-bf10-8de28589d51a_large.jpg?v=1512058138", IsBookOfTheWeek = false };
            var piz2 = new Books { Name = "Harry Potter and the Philosopher's Stone", Price = 7000.00M, Category = cat3, Description = "Turning the envelope over, his hand trembling, Harry saw a purple wax seal bearing a coat of arms; a lion, an eagle, a badger and a snake surrounding a large letter ‘H’.", ImageUrl = "https://cdn.shopify.com/s/files/1/1153/0428/products/9781781100219_large.jpg?v=1512057994", IsBookOfTheWeek = false };
            var piz3 = new Books { Name = "Harry Potter and the Chamber of Secrets", Price = 7500.00M, Category = cat1, Description = "There is a plot, Harry Potter. A plot to make most terrible things happen at Hogwarts School of Witchcraft and Wizardry this year.", ImageUrl = "https://cdn.shopify.com/s/files/1/1153/0428/products/9781781100226_large.jpg?v=1512058008", IsBookOfTheWeek = true };
            var piz4 = new Books { Name = "Harry Potter and the Prisoner of Azkaban", Price = 6500.00M, Category = cat1, Description = "Welcome to the Knight Bus, emergency transport for the stranded witch or wizard. Just stick out your wand hand, step on board and we can take you anywhere you want to go.", ImageUrl = "https://cdn.shopify.com/s/files/1/1153/0428/products/9781781100233_large.jpg?v=1512058021", IsBookOfTheWeek = false };
            var piz5 = new Books { Name = "Harry Potter and the Goblet of Fire", Price = 8500.00M, Category = cat2, Description = "There will be three tasks, spaced throughout the school year, and they will test the champions in many different ways …", ImageUrl = "https://cdn.shopify.com/s/files/1/1153/0428/products/9781781105672_large.jpg?v=1512058035", IsBookOfTheWeek = true };
            var piz6 = new Books { Name = "Harry Potter and the Order of the Phoenix", Price = 8000.00M, Category = cat1, Description = "You are sharing the Dark Lord's thoughts and emotions. The Headmaster thinks it inadvisable for this to continue. He wishes me to teach you how to close your mind to the Dark Lord.", ImageUrl = "https://cdn.shopify.com/s/files/1/1153/0428/products/9781781100240_large.jpg?v=1512058049", IsBookOfTheWeek = true };
            var piz7 = new Books { Name = "Harry Potter and the Half-Blood Prince", Price = 7000.00M, Category = cat1, Description = "There it was, hanging in the sky above the school: the blazing green skull with a serpent tongue, the mark Death Eaters left behind whenever they had entered a building… wherever they had murdered… ", ImageUrl = "https://cdn.shopify.com/s/files/1/1153/0428/products/9781781100257_large.jpg?v=1512058060", IsBookOfTheWeek = false };
            var piz8 = new Books { Name = "Harry Potter and the Deathly Hallows", Price = 8900.00M, Category = cat2, Description = "Give me Harry Potter,’ said Voldemort's voice, ‘and none shall be harmed. Give me Harry Potter, and I shall leave the school untouched. Give me Harry Potter, and you will be rewarded.", ImageUrl = "https://cdn.shopify.com/s/files/1/1153/0428/products/9781781100264_0912cb63-727c-48e0-beff-cda2ebaed8e3_large.jpg?v=1523287912", IsBookOfTheWeek = false };
            var piz9 = new Books { Name = "The Tales of Beedle the Bard", Price = 6900.00M, Category = cat3, Description = "There were once three brothers who were travelling along a lonely, winding road at twilight...", ImageUrl = "https://cdn.shopify.com/s/files/1/1153/0428/products/9781781106754_9c68e0f8-b7a5-4492-b28d-f7363d4dec61_large.jpg?v=1517910784", IsBookOfTheWeek = false };
            var piz10 = new Books { Name = "Harry Potter: A History of Magic", Price = 7500.00M, Category = cat1, Description = "Harry Potter: A History of Magic is the official eBook of the once-in-a-lifetime exhibition from the brilliant curators of the British Library.", ImageUrl = "https://cdn.shopify.com/s/files/1/1153/0428/products/9781781109236_large.jpg?v=1517910823", IsBookOfTheWeek = false };

            var pizs = new List<Books>()
            {
                piz1, piz2, piz3, piz4, piz5, piz6, piz7, piz8, piz9, piz10
            };

            var user1 = new IdentityUser { UserName = "user1@gmail.com", Email = "user1@gmail.com" };
            var user2 = new IdentityUser { UserName = "user2@gmail.com", Email = "user2@gmail.com" };
            var user3 = new IdentityUser { UserName = "user3@gmail.com", Email = "user3@gmail.com" };
            var user4 = new IdentityUser { UserName = "user4@gmail.com", Email = "user4@gmail.com" };
            var user5 = new IdentityUser { UserName = "user5@gmail.com", Email = "user5@gmail.com" };

            string userPassword = "Password123";

            var users = new List<IdentityUser>()
            {
                user1, user2, user3, user4, user5
            };

            foreach (var user in users)
            {
                //Bug with Core 2.1/2.2: Throws Disposed Exception after multiple calls.
                var result1 = _userManager.CreateAsync(user, userPassword).Result;
            }

         

           

            var ord1 = new Order
            {
                FirstName = "Zhanel",
                LastName = "Erkinbekova",
                Address = "Moldagulova 32",
                City = "Almaty",
                Country = "Kazakhstan",
                Email = "erkinbekovazhanel@gmail.com",
                OrderPlaced = DateTime.Now.AddDays(-2),
                PhoneNumber = "87772369955",
                User = user1,
                OrderTotal = 370.00M,
            };

            var ord2 = new Order { };
            var ord3 = new Order { };

            var orderLines = new List<OrderDetail>()
            {
                new OrderDetail { Order=ord1, Book=piz1, Amount=2, Price=piz1.Price},
                new OrderDetail { Order=ord1, Book=piz3, Amount=1, Price=piz3.Price},
                new OrderDetail { Order=ord1, Book=piz5, Amount=3, Price=piz5.Price},
            };

            var orders = new List<Order>()
            {
                ord1
            };

            _context.Categories.AddRange(cats);
            // _context.Pizzas.AddRange(pizs);
            // _context.Reviews.AddRange(revs);
            _context.Orders.AddRange(orders);
            _context.OrderDetails.AddRange(orderLines);
        

            var result2 = _context.SaveChangesAsync().Result;
            await Task.CompletedTask;
        }

        public async Task ClearDatabaseAsync()
        {
        

            var shoppingCartItems = _context.ShoppingCartItems.ToList();
            _context.ShoppingCartItems.RemoveRange(shoppingCartItems);

            var users = _context.Users.ToList();
            var userRoles = _context.UserRoles.ToList();

            foreach (var user in users)
            {
                if (!userRoles.Any(r => r.UserId == user.Id))
                {
                    _context.Users.Remove(user);
                }
                //if (!user.Roles.Any())
                //{
                //    _context.Users.Remove(user);
                //}
            }

            var orderDetails = _context.OrderDetails.ToList();
            _context.OrderDetails.RemoveRange(orderDetails);

            var orders = _context.Orders.ToList();
            _context.Orders.RemoveRange(orders);

            var pizzas = _context.Books.ToList();
            _context.Books.RemoveRange(pizzas);

            var categories = _context.Categories.ToList();
            _context.Categories.RemoveRange(categories);

            var result = _context.SaveChangesAsync().Result;
            await Task.CompletedTask;
        }

    }
}
