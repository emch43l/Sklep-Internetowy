using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product>? GetProducts();

        public IEnumerable<Product>? GetProductsWithAditionalData();

        public Product? GetProductByGuid(Guid id);

        public Product? GetProductWithAditionalData(Guid id);

        public Product AddProduct(Product product);

        public Product? RemoveProduct(Guid id);

        public Product UpdateProduct(Product product);

        public void Save();


    }
}
