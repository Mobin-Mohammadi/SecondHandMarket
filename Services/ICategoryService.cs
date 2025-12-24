using ISE309_SecondHandMarket.Models;
using System.Collections.Generic;

namespace ISE309_SecondHandMarket.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category? GetById(int id);
        void Add(Category category);
        void Update(Category category);
        void Delete(int id);
    }
}
