using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Repositories.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product>? GetProducts();

        public IEnumerable<Product>? GetProductsWithAditionalData();

        public Product? GetProductByGuid(string id);

        public Product? GetProductWithAditionalData(string id);

        public Product AddProduct(Product product);

        public Product? RemoveProduct(string id);

        public Product UpdateProduct(Product product);

        public void Save();

        public Product? GetMostExpensiveProduct();

        public Product? GetCheapestProduct();


    }
}
