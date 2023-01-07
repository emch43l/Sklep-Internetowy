using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.Services;
using Sklep_Internetowy.ViewModels;

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
        public IActionResult GetProduct(string id)
        {
            Product? product = _pRepo.GetProductWithAditionalData(id);
            if(product == null)
            {
                return NotFound();
            }

            return Ok(ToObject(product));
        }

        [Route("all/{page}")]
        public IActionResult GetProducts(int page)
        {
            if (page == null || page < 1)
                return BadRequest();
            IEnumerable<Product> products = _pRepo.GetProducts();
            EntityPaginator<Product> paginator = new EntityPaginator<Product>(products);
            products = paginator.GetPaginatedData((int)page);
            return Ok( new { 
                products = products.Select( p => new { id = p.Guid.ToString(), p.Name, p.Price } ) 
            });

        }

        [Route("delete/{id}")]
        public IActionResult DeleteProduct(string id)
        {
            Product? removedProduct = _pRepo.RemoveProduct(id);
            if (removedProduct == null)
                return NotFound();
            return Ok();
        }

        [HttpPost]
        [Route("create")]
        public IActionResult CreateProduct(ApiProductCreateModel model)
        {
            if(ModelState.IsValid)
            {
                Producer? producer = _prodRepo.GetProducerByGuid(model.Producer);
                if(producer == null)
                    return BadRequest();

                ICollection<ProductCategory> categories = model.Categories.Select(c => GetCategoryByNameAndCreateIfNotExist(c)).ToList();

                Product product = model.MapTo();
                product.Producer = producer;
                product.ProductDetail = new ProductDetail()
                {
                    Creation_Date = DateTime.Now,
                    Description = model.Description,
                    Information = model.Informations.ToList()
                };
                product.Categories = categories;
                Product created = _pRepo.AddProduct(product);
                _pRepo.Save();
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

        public IActionResult UpdateProduct(string id, [FromBody] ApiProductCreateModel model)
        {
            if (ModelState.IsValid)
            {
                Producer? producer = _prodRepo.GetProducerByGuid(model.Producer);
                Product? product = _pRepo.GetProductWithAditionalData(id);
                if(product == null || producer == null)
                    return BadRequest();

                product.Categories = model.Categories.Select(c => GetCategoryByNameAndCreateIfNotExist(c)).ToList();
                product.Name = model.Name;
                product.Price = (decimal)model.Price;
                product.Producer = producer;
                product.ProductDetail.Description = model.Description;
                product.ProductDetail.Information = model.Informations.ToList();
                _pRepo.Save();
                return Ok(ToObject(product));
            }
            else
            {
                return BadRequest();
            }
        }

        private ProductCategory GetCategoryByNameAndCreateIfNotExist(string name)
        {
            ProductCategory? category = _pcRepo.GetProductCategoryByName(name);
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
