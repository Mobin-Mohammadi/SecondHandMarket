using ISE309_SecondHandMarket.Models;

namespace ISE309_SecondHandMarket.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        Product GetByIdNoTracking(int id);
        Task AddAsync(Product product);
        Task UpdateAsync(Product product);
        void Delete(int id);
    }
}