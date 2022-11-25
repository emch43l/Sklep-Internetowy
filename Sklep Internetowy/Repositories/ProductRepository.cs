using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Models.Contexts;
using Sklep_Internetowy.Repositories.Interfaces;

namespace Sklep_Internetowy.Repositories
{
    public class ProductRepository : IDisposable, IProductRepository
    {
        private bool disposed = false;

        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Product>? GetProducts()
        {
            return _context.Products;
        }

        public IEnumerable<Product>? GetProductsWithAditionalData()
        {
            return _context.Products
                .Include(p => p.Producer)
                .Include(p => p.Rating)
                .Include(p => p.Categories)
                .Include(p => p.ProductDetail)
                .ThenInclude(pd => pd.Images);
        }

        public Product? GetProductByGuid(Guid id)
        {
            return _context.Products
                .FirstOrDefault(p => p.Guid == id);
        }

        public Product? GetProductWithAditionalData(Guid id)
        {
            return _context.Products
                .Include(p => p.Producer)
                .Include(p => p.Rating)
                .Include(p => p.ProductDetail)
                .ThenInclude(pd => pd.Images)
                .FirstOrDefault(p => p.Guid == id);
        }

        public Product AddProduct(Product product)
        {
            return _context.Products.Add(product).Entity;
        }

        public Product? RemoveProduct(Guid id)
        {
            Product? product = GetProductByGuid(id);
            if (product != null)
                return _context.Products.Remove(product).Entity;
            return null;
        }

        public Product UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
