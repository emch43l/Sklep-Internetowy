using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.Services;
using Sklep_Internetowy.Services.Interfaces;
using Sklep_Internetowy.Utils.Validation;
using Sklep_Internetowy.ViewModels;
using Sklep_Internetowy.ViewModels.DTO;

namespace Sklep_Internetowy.Controllers
{
    [Route("admin/api/products")]
    [ApiController]
    public class AdminProductApiController : ControllerBase
    {
        private IProductRepository _pRepo;
        private IDirectoryConfigurationReader _reader;
        private IProducerRepository _prodRepo;
        private IProductCategoryRepository _pcRepo;
        public AdminProductApiController(
            IProductRepository productRepo, 
            IDirectoryConfigurationReader ireader, 
            IProducerRepository producerRepo, 
            IProductCategoryRepository categoryRepo
        ) 
        {
            _pRepo= productRepo;
            _prodRepo = producerRepo;
            _reader = ireader;
            _pcRepo = categoryRepo;
        }

        [Route("{id}")]
        public async Task<IActionResult> GetProduct(Guid id)
        {
            if(ModelState.IsValid)
            {
                Product? product = await _pRepo.GetProductWithAssociatedEntities(id);
                if(product == null)
                {
                    return NotFound();
                }

                return Ok(ToObject(product));
            }

            return BadRequest();
        }

        [Route("all/{page}")]
        public async Task<IActionResult> GetProducts(int page)
        {
            if (page < 1)
                return BadRequest();

            EntityPaginator<Product> paginator = new EntityPaginator<Product>(await _pRepo.GetAll());
            IEnumerable<Product> paginatedEntities = paginator.GetPaginatedData((int)page);

            return Ok( new { 
                products = paginatedEntities.Select( p => new { id = p.Guid.ToString(), p.Name, p.Price } ) 
            });

        }

        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            Product removedProduct = await _pRepo.Remove(id);
            if (removedProduct == null)
                return NotFound();
            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateProduct(APICreateProductDTO model)
        {
            if(ModelState.IsValid)
            {
                Producer? producer = await _prodRepo.GetOneByGuid(model.Producer);
                if(producer == null)
                    return BadRequest();

                ICollection<ProductCategory> categories = model.Categories.Select(c => GetCategoryByNameAndCreateIfNotExist(c).Result).ToList();

                Product product = model.MapTo();
                product.Producer = producer;
                product.ProductDetail = new ProductDetail()
                {
                    Creation_Date = DateTime.Now,
                    Description = model.Description,
                    Information = model.Informations.ToList()
                };
                product.Categories = categories;
                Product created = await _pRepo.Add(product);
                await _pRepo.SaveChanges();
                return Created(Request.PathBase + "/" +  created.Guid.ToString(), new
                {
                    id = created.Guid.ToString(),
                    name = created.Name,
                    price = created.Price,
                    informations = created.ProductDetail.Information,
                    descrition = created.ProductDetail.Description,
                    producer = created.Producer.Name,
                    categories = created.Categories.Select(c => new { id = c.Guid.ToString(), c.Name }).ToList(),
                });
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("update/{id}")]

        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] APICreateProductDTO model)
        {
            if (ModelState.IsValid)
            {
                Producer? producer = await _prodRepo.GetOneByGuid(model.Producer);
                Product? product = await _pRepo.GetProductWithAssociatedEntities(id);
                if(product == null || producer == null)
                    return BadRequest();

                product.Categories = model.Categories.Select(c => GetCategoryByNameAndCreateIfNotExist(c).Result).ToList();
                product.Name = model.Name;
                product.Price = (decimal)model.Price;
                product.Producer = producer;
                product.ProductDetail.Description = model.Description;
                product.ProductDetail.Information = model.Informations.ToList();
                await _pRepo.SaveChanges();
                return Ok(ToObject(product));
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<ProductCategory> GetCategoryByNameAndCreateIfNotExist(string name)
        {
            ProductCategory? category = await _pcRepo.GetCategoryByName(name);
            if (category == null)
                return new ProductCategory() { Name = name };
            return category;
        }

        private object ToObject(Product product)
        {
            return new
            {
                name = product.Name,
                price = product.Price,
                rating = product.Rating.Count == 0 ? 0 : product.Rating.Sum(r => r.Rating) / product.Rating.Count,
                categories = product.Categories.Select(c => new { id = c.Guid.ToString(), name = c.Name }).ToArray(),
                producer = product.Producer.Name,
                description = product.ProductDetail.Description,
                informations = product.ProductDetail.Information,
                images = product.ProductDetail.Images.Select(i => new
                {
                    id = i.Guid.ToString(),
                    url = Request.Host.Value + "/" + _reader.GetDirectory(TargetFolder.Images) + "/" + i.Name
                }).ToArray()
            };
        }
    }
}
