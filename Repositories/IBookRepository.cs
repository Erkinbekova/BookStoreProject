using OnlinePizzaWebApplication.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlinePizzaWebApplication.Repositories
{
    public interface IBookRepository
    {
        IEnumerable<Books> Books { get; }
        IEnumerable<Books> BooksOfTheWeek { get; }

        Books GetById(int? id);
        Task<Books> GetByIdAsync(int? id);

        Books GetByIdIncluded(int? id);
        Task<Books> GetByIdIncludedAsync(int? id);

        bool Exists(int id);

        IEnumerable<Books> GetAll();
        Task<IEnumerable<Books>> GetAllAsync();

        IEnumerable<Books> GetAllIncluded();
        Task<IEnumerable<Books>> GetAllIncludedAsync();

        void Add(Books book);
        void Update(Books book);
        void Remove(Books book);

        void SaveChanges();
        Task SaveChangesAsync();

    }
}
