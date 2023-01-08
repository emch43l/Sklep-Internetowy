using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;

namespace SklepInternetowyTest
{
    public class ProductCategoryRepoTest : IProductCategoryRepository
    {
        private readonly ISet<ProductCategory> _categories;

        public ProductCategoryRepoTest()
        {
            _categories = new HashSet<ProductCategory>();
        }
        public ProductCategory AddProductCategory(ProductCategory category)
        {
            _categories.Add(category);
            return category;
        }

        public IEnumerable<ProductCategory> GetCategories()
        {
            return _categories;
        }

        public IEnumerable<Product>? GetCategoryProductsByCategoryGuid(string id)
        {
            throw new NotImplementedException();
        }

        public ProductCategory? GetProductCategoryByGuid(string id)
        {
            return _categories.Where(c => c.Guid.ToString() == id).FirstOrDefault();
        }

        public ProductCategory? GetProductCategoryByName(string name)
        {
            return _categories.Where(c => c.Name.ToLower() == name.ToLower()).FirstOrDefault();
        }

        public ProductCategory? RemoveProductCategory(string id)
        {
            ProductCategory? category = _categories.Where(c => c.Guid.ToString() == id).FirstOrDefault();
            if (_categories.Remove(category))
                return category;
            return null;
        }

        public void Save()
        {

        }

        public ProductCategory UpdateProductCategory(ProductCategory category)
        {
            throw new NotImplementedException();
        }
    }
}
