using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Models.Contexts;
using Sklep_Internetowy.Repositories.Interfaces;

namespace Sklep_Internetowy.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository, IDisposable
    {
        private bool disposed = false;

        private readonly DataContext _context;

        public ProductCategoryRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductCategory> GetCategories()
        {
            return _context.ProductCategories.ToList();
        }

        public ProductCategory? GetProductCategoryByGuid(string id)
        {
            id = id.ToUpper();
            return _context.ProductCategories.FirstOrDefault(pc => pc.Guid.ToString() == id);
        }

        public IEnumerable<Product>? GetCategoryProductsByCategoryGuid(string id)
        {
            id = id.ToUpper();
            return _context.ProductCategories
                .Include(pc => pc.Products)
                .FirstOrDefault(pc => pc.Guid.ToString() == id)?
                .Products;
        }

        public ProductCategory AddProductCategory(ProductCategory category)
        {
            return _context.ProductCategories.Add(category).Entity;
        }

        public ProductCategory UpdateProductCategory(ProductCategory category)
        {
            throw new NotImplementedException();
        }

        public ProductCategory? RemoveProductCategory(string id)
        {
            id = id.ToUpper();
            ProductCategory? entity = _context.ProductCategories.FirstOrDefault(pc => pc.Guid.ToString() == id);
            if (entity != null)
                return _context.ProductCategories.Remove(entity).Entity;
            return null;
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
