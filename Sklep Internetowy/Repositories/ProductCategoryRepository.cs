using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Contexts;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;

namespace Sklep_Internetowy.Repositories
{
    public class ProductCategoryRepository : RepositoryBase<ProductCategory>, IProductCategoryRepository
    {
        public ProductCategoryRepository(DataContext context) : base(context) { }

        public async Task<ProductCategory?> GetCategoryByName(string name)
        {
            return await _context.ProductCategories
                .Where(c => c.Name.ToLower() == name.ToLower())
                .FirstOrDefaultAsync();
        }
    }
}
