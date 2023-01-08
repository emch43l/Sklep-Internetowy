using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;

namespace SklepInternetowyTest
{
    public class ProductRepoTest : IProductRepository
    {
        private readonly ISet<Product> _products;
        public ProductRepoTest()
        {
            _products = new HashSet<Product>();
        }
        public Product AddProduct(Product product)
        {
            _products.Add(product);
            return product;
        }

        public Product? GetCheapestProduct()
        {
            return _products.OrderBy(p => p.Price).FirstOrDefault();
        }

        public Product? GetMostExpensiveProduct()
        {
            return _products.OrderByDescending(p => p.Price).FirstOrDefault();
        }

        public Product? GetProductByGuid(string id)
        {
            return _products.Where(p => p.Guid.ToString() == id.ToLower()).FirstOrDefault();
        }

        public IEnumerable<Product>? GetProducts()
        {
            return _products;
        }

        public IEnumerable<Product>? GetProductsWithAditionalData()
        {
            return _products;
        }

        public Product? GetProductWithAditionalData(string id)
        {
            return _products.Where(p => p.Guid.ToString() == id.ToLower()).FirstOrDefault();
        }

        public Product? RemoveProduct(string id)
        {
            Product prod = _products.Where(p => p.Guid.ToString() == id).FirstOrDefault();
            _products.Remove(prod);
            return prod;
        }

        public void Save()
        {

        }

        public Product UpdateProduct(Product product)
        {
            throw new NotImplementedException();
        }
    }
}
