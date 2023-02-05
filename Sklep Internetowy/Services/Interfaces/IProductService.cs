using Sklep_Internetowy.Models;
using Sklep_Internetowy.ViewModels;
using Sklep_Internetowy.ViewModels.Models;

namespace Sklep_Internetowy.Services.Interfaces
{
    public interface IProductService : IEntityServiceBase
    {
        Task<Product?> Add(Product product);

        Task<Product?> Add(CreateProductViewModel model);

        Task<Product?> Update(ProductEditViewModel product);

        Task<Product?> Delete(Guid id, bool deleteImages = false);

        Task<ProductEditViewModel?> GetModel(Guid id);

        Task<CreateProductViewModel> GetModel(CreateProductViewModel model);

        Task<bool> DeleteImage(Guid pId, Guid iId);

        Task<Product?> AddImage(Guid id, ImageModel image);
    }
}
