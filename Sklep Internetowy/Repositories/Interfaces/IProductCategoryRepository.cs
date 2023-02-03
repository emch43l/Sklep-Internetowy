using Sklep_Internetowy.Models;

namespace Sklep_Internetowy.Repositories.Interfaces
{
    public interface IProductCategoryRepository : IRepositoryBase<ProductCategory>
    {
        Task<ProductCategory?> GetCategoryByName(string name);
    }
}
