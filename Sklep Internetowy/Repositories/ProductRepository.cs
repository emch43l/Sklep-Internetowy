using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Contexts;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;

namespace Sklep_Internetowy.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context) { }

        public async Task<Product?> GetProductWithAssociatedEntities(Guid id)
        {
            return await _context.Products.Where(p => p.Guid == id)
                .Include(p => p.Producer)
                .Include(p => p.Categories)
                .Include(p => p.Rating)
                .Include(p => p.ProductDetail)
                .ThenInclude(d => d.Images)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Product>> GetProductsWithAssociatedEntities()
        {
            return await _context.Products
                .Include(p => p.Producer)
                .Include(p => p.Categories)
                .Include(p => p.Rating)
                .Include(p => p.ProductDetail)
                .ThenInclude(d => d.Images)
                .ToListAsync();
        }

        public async Task<Product?> GetCheapest()
        {
            return await _context.Products.OrderBy(p => p.Price).FirstOrDefaultAsync();
        }

        public async Task<Product?> GetMostExpensive()
        {
            return await _context.Products.OrderByDescending(p => p.Price).FirstOrDefaultAsync();
        }

    }
}
