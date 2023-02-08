using Sklep_Internetowy.Models;
using Sklep_Internetowy.ViewModels;
using Sklep_Internetowy.ViewModels.Models;

namespace Sklep_Internetowy.Services.Interfaces
{
    public interface IProductService : IEntityServiceBase
    {
        Task<Product?> Add(Product product);

        Task<Product?> Add(ProductViewModel model);

        Task<Product?> Update(ProductViewModel product);

        Task<Product?> Delete(Guid id, bool deleteImages = false);

        Task<ProductViewModel?> GetModel(Guid id);

        Task<ProductViewModel> GetModel(ProductViewModel model);

        Task<bool> DeleteImage(Guid pId, Guid iId);

        Task<Product?> AddImage(Guid id, ImageModel image);

    }
}
