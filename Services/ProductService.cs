using ISE309_SecondHandMarket.Data;
using ISE309_SecondHandMarket.Models;
using Microsoft.EntityFrameworkCore;

namespace ISE309_SecondHandMarket.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAll() => _context.Products.Include(p => p.Category).ToList();

        public Product GetById(int id) => _context.Products.Include(p => p.Category).FirstOrDefault(p => p.Id == id);

        public Product GetByIdNoTracking(int id) => _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            var tracked = _context.Products.Local.FirstOrDefault(e => e.Id == product.Id);
            if (tracked != null)
            {
                _context.Entry(tracked).State = EntityState.Detached;
            }

            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
        }
    }
}