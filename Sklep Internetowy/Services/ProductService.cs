using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sklep_Internetowy.Contexts;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories;
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.Services.Interfaces;
using Sklep_Internetowy.ViewModels;
using Sklep_Internetowy.ViewModels.Models;

namespace Sklep_Internetowy.Services
{
    public class ProductService : EntityServiceBase, IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IProducerRepository _producerRepository;
        private readonly IFileUploader _fileUploader;

        public ProductService(DataContext context, IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
            _productRepository = new ProductRepository(context);
            _productCategoryRepository = new ProductCategoryRepository(context);
            _producerRepository = new ProducerRepository(context);
        }

        public Task<Product?> Add(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<Product?> Add(CreateProductViewModel model)
        {
            Producer? producer = await _producerRepository.GetOneByGuid(model.ProducerId);
            Product product = new Product();
            ProductDetail productDetail = new ProductDetail();

            if (producer == null)
            {
                AddError(new ServiceError(ErrorType.Major,string.Empty, "Selected producer doesnt exist !"));
                return null;
            }


            product.Name = model.Name;
            product.ProductDetail = productDetail;
            product.Price = decimal.Parse(model.Price.ToString());
            product.Producer = producer;
            product.Categories = new List<ProductCategory>();

            productDetail.Description = model.Description;
            productDetail.Creation_Date = DateTime.Now;
            productDetail.Information = model.AditionalInformations.Where(s => s != string.Empty && s != null).ToList();

            foreach (Guid id in model.CategoryId)
            {
                ProductCategory? productCategory = await _productCategoryRepository.GetOneByGuid(id);
                if (productCategory != null)
                {
                    product.Categories.Add(productCategory);
                }
                else
                {
                    AddError(new ServiceError(ErrorType.Minor, string.Empty, $"Could not find category with id: {id} !"));
                }
            }


            if (model.Images.Images.Count() != 0)
            {
                List<UploadedFile> images = new List<UploadedFile>();

                foreach (IFormFile image in model.Images.Images)
                    images.Add(await _fileUploader.UploadFile(image));

                productDetail.Images = images.Select(image =>
                    new Image()
                    {
                        Title = image.File.FileName,
                        Name = image.UploadedFileName
                    }
                ).ToList();
            }

            Product createdEntity = await _productRepository.Add(product);
            await _productRepository.SaveChanges();

            return createdEntity;

        }

        public async Task<Product?> AddImage(Guid id, ImageModel image)
        {
            _fileUploader.SetTargetFolderTo(TargetFolder.Images);

            Product? product = await _productRepository.GetProductWithAssociatedEntities(id);
            if (product == null)
            {
                AddError(new ServiceError(ErrorType.Major, string.Empty, $"Could not find product with id: {id} !"));
                return null;
            }

            foreach (var img in image.Images)
            {
                UploadedFile? file = await _fileUploader.UploadFile(img);
                if (file != null)
                {
                    product.ProductDetail.Images.Add(new Image()
                    {
                        Title = file.File.FileName,
                        Name = file.UploadedFileName
                    });
                }
                else
                {
                    AddError(new ServiceError(ErrorType.Minor, string.Empty, $"Could not uplad file: {img.Name} !"));
                }
            }

            await _producerRepository.SaveChanges();
            return product;
        }

        public async Task<Product?> Delete(Guid id, bool deleteImages = false)
        {
            Product? product = await _productRepository.GetProductWithAssociatedEntities(id);

            if (product == null)
            {
                AddError(new ServiceError(ErrorType.Major, string.Empty, $"Could not find product with id: {id} !"));
                return null;
            }

            _fileUploader.SetTargetFolderTo(TargetFolder.Images);

            foreach (Image img in product.ProductDetail.Images)
            {
                if (product.ProductDetail.Images.Remove(img))
                    _fileUploader.DeleteFile(img);
            }

            await _productRepository.Remove(id);
            await _productRepository.SaveChanges();

            return product;

        }

        public async Task<bool> DeleteImage(Guid pId, Guid iId)
        {
            Product? product = await _productRepository.GetProductWithAssociatedEntities(pId);
            if (product == null)
            {
                AddError(new ServiceError(ErrorType.Major, string.Empty, $"Could not find product with id: {pId} !"));
                return false;
            }
            
            Image? image = product.ProductDetail.Images.Where(i => i.Guid == iId).FirstOrDefault();
            bool isRemoved = product.ProductDetail.Images.Remove(image);

            if(isRemoved)
            {
                _fileUploader.DeleteFile(image);
                await _producerRepository.SaveChanges();
                return true;
            }

            AddError(new ServiceError(ErrorType.Major, string.Empty, $"Could not remove image: {image.Name} !"));

            return false;
    
        }

        public async Task<ProductEditViewModel?> GetModel(Guid id)
        {
            Product? product = await _productRepository.GetProductWithAssociatedEntities(id);

            if (product == null)
            {
                AddError(new ServiceError(ErrorType.Major, string.Empty, $"Could not create model !"));
                return null;
            }

            return new ProductEditViewModel()
            {
                Id = product.Guid,
                Name = product.Name,
                Price = (double)product.Price,
                Categories = await _productCategoryRepository.GetAll(),
                Producers = GetProducersSelectist(),
                AditionalInformations = product.ProductDetail.Information,
                CategoryId = product.Categories.Select(p => p.Guid).ToList(),
                Description = product.ProductDetail.Description,
                Files = product.ProductDetail.Images.ToList()
            };
        }

        public async Task<CreateProductViewModel> GetModel(CreateProductViewModel model)
        {
            model.Categories = await _productCategoryRepository.GetAll();
            model.Producers = GetProducersSelectist();

            return model;
        }

        public async Task<Product?> Update(ProductEditViewModel model)
        {
            Product? product = await _productRepository.GetProductWithAssociatedEntities(model.Id);

            if (product == null)
            {
                AddError(new ServiceError(ErrorType.Major, nameof(product.Guid), "Could not find target product !"));
                return null;
            }

            Producer? producer = await _producerRepository.GetOneByGuid(model.ProducerId);
            if (producer == null)
            {
                AddError(new ServiceError(ErrorType.Major, nameof(model.ProducerId), "Could not find target producer !"));
                return null;
            }

            #pragma warning disable CS8619 // Obsługa wartości null dla typów referencyjnych w wartości jest niezgodna z typem docelowym.
            IEnumerable<ProductCategory> categories = model.CategoryId.Select(c => 
            {
                ProductCategory? category = _productCategoryRepository.GetOneByGuid(c).Result;
                if(category == null)
                {
                    AddError(new ServiceError(ErrorType.Major, nameof(model.ProducerId), $"Could not find category with id: {c} !"));
                }

                return category;

             }).Where(c => c != null).ToList();
            #pragma warning restore CS8619 // Obsługa wartości null dla typów referencyjnych w wartości jest niezgodna z typem docelowym.

            product.Categories = categories.ToList();
            product.Name = model.Name;
            product.Price = (decimal)model.Price;
            product.ProductDetail.Description = model.Description;
            product.ProductDetail.Information = model.AditionalInformations;
            
            await _productRepository.SaveChanges();

            return product;

        }

        private List<SelectListItem> GetProducersSelectist()
        {
            return _producerRepository.GetAll().Result.Select(c => new SelectListItem(c.Name, c.Guid.ToString())).ToList();
        }
    }
}
