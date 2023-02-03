using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Repositories.Interfaces
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<Product?> GetProductWithAssociatedEntities(Guid id);

        Task<List<Product>> GetProductsWithAssociatedEntities();

        Task<Product?> GetCheapest();

        Task<Product?> GetMostExpensive();
    }
}
