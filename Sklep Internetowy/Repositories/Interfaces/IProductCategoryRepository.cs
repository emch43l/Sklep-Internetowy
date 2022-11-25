using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Repositories.Interfaces
{
    public interface IProductCategoryRepository
    {
        public IEnumerable<ProductCategory>? GetCategories();

        public ProductCategory? GetProductCategoryByGuid(Guid id);

        public IEnumerable<Product>? GetCategoryProductsByCategoryGuid(Guid id); 

        public ProductCategory AddProductCategory(ProductCategory category);

        public ProductCategory UpdateProductCategory(ProductCategory category);

        public ProductCategory? RemoveProductCategory(Guid id);
    }
}
