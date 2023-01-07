using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Repositories.Interfaces
{
    public interface IProductCategoryRepository
    {
        public IEnumerable<ProductCategory> GetCategories();

        public ProductCategory? GetProductCategoryByGuid(string id);

        public IEnumerable<Product>? GetCategoryProductsByCategoryGuid(string id); 

        public ProductCategory AddProductCategory(ProductCategory category);

        public ProductCategory UpdateProductCategory(ProductCategory category);

        public ProductCategory? RemoveProductCategory(string id);

        public ProductCategory? GetProductCategoryByName(string name);

        public void Save();
    }
}
