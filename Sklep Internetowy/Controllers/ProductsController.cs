
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Sklep_Internetowy.Services;
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.Repositories;
using Microsoft.AspNetCore.Authorization;
using Sklep_Internetowy.ViewModels.Models;
using Sklep_Internetowy.Contexts;
using Sklep_Internetowy.Services.Interfaces;
using Sklep_Internetowy.ViewModels;

namespace Sklep_Internetowy.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("/admin/products")]
    public class ProductsController : Controller
    {
        private readonly DataContext _context;

        private readonly string _appEnviroment;

        private readonly IFileUploader _fileUploader;

        private readonly IProducerRepository _prodRepo;
        private readonly IProductRepository _pRepo;
        private readonly IProductCategoryRepository _pcRepo;
        private readonly IImageRepository _imgRepo;

        public ProductsController(IWebHostEnvironment environment, IFileUploader fileUploader, DataContext context)
        {
            _context = context;
            _fileUploader = fileUploader;
            _appEnviroment = environment.WebRootPath;

            //repositories initialization

            _prodRepo = new ProducerRepository(_context);
            _pRepo = new ProductRepository(_context);
            _pcRepo = new ProductCategoryRepository(_context);
            _imgRepo = new ImageRepository(_context);
        }

        [Route("/admin/products")]
        public IActionResult Index(int page)
        {
            page = (page < 1) ? 1 : page;
            EntityPaginator<Product> paginator = new EntityPaginator<Product>(_context.Products.Include(p => p.ProductDetail).ToList());
            paginator.SetPageEntityNumber(5);
            IEnumerable<Product> products = paginator.GetPaginatedData(page);
            List<int> pagesNumber = paginator.GetPagesNumber(page);
            return View(new Tuple<IEnumerable<Product>,List<int>,int>(products,pagesNumber,page));
        }

        [Route("/admin/products/delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Product? product = await _pRepo.GetProductWithAssociatedEntities(id);
            if(product == null)
                return RedirectToAction("Index");
            _fileUploader.SetTargetFolderTo(TargetFolder.Images);
            foreach(Image img in product.ProductDetail.Images)
            {
                if(product.ProductDetail.Images.Remove(img))
                    _fileUploader.DeleteFile(img);
            }
            await _pRepo.Remove(id);
            await _pRepo.SaveChanges();
            
            return RedirectToAction("Index");

        }

        [Route("/admin/products/create")]
        public IActionResult Create()
        {
            return View(new CreateProductViewModel { Producers = GetProducers(), Categories = _context.ProductCategories.ToList()});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/products/create")]
        public async Task<IActionResult> Create(CreateProductViewModel product)
        {
            _fileUploader.SetTargetFolderTo(TargetFolder.Images);

            ProductDetail productDetails = new ProductDetail();
            Product entity = new Product();
            List<UploadedFile> images = new List<UploadedFile>();

            if (ModelState.IsValid)
            {
                Producer? producer = _prodRepo.GetProducerByGuid(product.ProducerId);
                if(producer == null)
                {
                    ModelState.AddModelError("ProducerNotFound", "Selected producer doesnt exist !");
                    return View(product);
                }

                entity.Name = product.Name;
                entity.ProductDetail = productDetails;
                entity.Price = decimal.Parse(product.Price.ToString());
                entity.Producer = producer;
                entity.Categories = new List<ProductCategory>();

                productDetails.Description = product.Description;
                productDetails.Creation_Date = DateTime.Now;
                productDetails.Information = product.AditionalInformations.Where(s => s != string.Empty && s != null).ToList();

                if(product.CategoryId != null)
                {
                    foreach(string id in product.CategoryId)
                    {
                        ProductCategory? productCategory = _pcRepo.GetProductCategoryByGuid(id);
                        if(productCategory != null)
                            entity.Categories.Add(productCategory);
                    }
                }
   

                if(product.Images != null && product.Images.Count != 0) 
                {
                    foreach (IFormFile image in product.Images)
                        images.Add(await _fileUploader.UploadFile(image));

                    productDetails.Images = images.Select(image =>
                        new Image() 
                        {
                            Title = image.File.FileName,
                            Name = image.UploadedFileName
                        }
                    ).ToList();
                }

                Product createdEntity = _pRepo.AddProduct(entity);
                _pRepo.Save();

                return RedirectToAction("Index","Home");
            }
            product.Categories = new List<ProductCategory>(_pcRepo.GetCategories());
            product.Producers = GetProducers();
            return View(product);
        }

        [HttpGet]
        [Route("/admin/products/edit/{id}")]
        public IActionResult Edit(string id)
        {
            Product? product = _pRepo.GetProductWithAditionalData(id);
            if (product == null)
                return NotFound();
            return View(new EditProductDTO()
            {
                Id = product.Guid.ToString(),
                Name = product.Name,
                Price = (double)product.Price,
                Categories = _pcRepo.GetCategories().ToList(),
                Producers = GetProducers(),
                AditionalInformations = product.ProductDetail.Information,
                CategoryId = product.Categories.Select(p => p.Guid.ToString()).ToList(),
                Description = product.ProductDetail.Description,
                Files = product.ProductDetail.Images.ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/products/edit/{id}")]
        public IActionResult Edit(EditProductDTO model)
        {
            if (!ModelState.IsValid)
            {
                System.Diagnostics.Debug.WriteLine("---------------------");
                IEnumerable<ModelError> errors = ModelState.Values.SelectMany(v => v.Errors);
                errors.Select(c => { System.Diagnostics.Debug.WriteLine(c.ErrorMessage); return 1; });
                System.Diagnostics.Debug.WriteLine("---------------------");
                return Edit(model.Id);
            }

            Product? product = _pRepo.GetProductWithAditionalData(model.Id);
            if (product == null)
                return RedirectToAction("Index");
            Producer? producer = _prodRepo.GetProducerByGuid(model.ProducerId.ToLower());
            if(producer == null)
            {
                System.Diagnostics.Debug.WriteLine("---------------------");
                ModelState.AddModelError("ProducerId", "This producer doesnt exist !");
                return Edit(model.Id);
            }
            IEnumerable<ProductCategory> categories = model.CategoryId.Select(c => _pcRepo.GetProductCategoryByGuid(c));
            product.Categories = categories.ToList();
            product.Name = model.Name;
            product.Price = (decimal)model.Price;
            product.ProductDetail.Description = model.Description;
            product.ProductDetail.Information = model.AditionalInformations;
            _pRepo.Save();
            return RedirectToAction("Index");

        }

        [Route("/admin/products/image/delete/{pId}/{iId}")]
        public IActionResult DeleteProductImage(string pId, string iId)
        {
            string? referer = HttpContext.Request.Headers["referer"];
            if (referer == null)
                referer = String.Empty;

            _fileUploader.SetTargetFolderTo(TargetFolder.Images);
            Product? product = _pRepo.GetProductWithAditionalData(pId);
            if (product == null)
                return Redirect(referer);

            Image? image = product.ProductDetail.Images.Where(i => i.Guid.ToString() == iId).FirstOrDefault();
            if(product.ProductDetail.Images.Remove(image))
            {
                _fileUploader.DeleteFile(image);
            }

            _pRepo.Save();
            return Redirect(referer);
            
        }

        [HttpPost]
        [Route("/admin/products/image/add/{id}")]
        public async Task<IActionResult> AddProductImage(string id, [FromForm] ImageModel image)
        {
            string? referer = HttpContext.Request.Headers["referer"];
            if (referer == null)
                referer = String.Empty;

            if (!ModelState.IsValid)
                return Redirect(referer);

            _fileUploader.SetTargetFolderTo(TargetFolder.Images);
            Product? product = _pRepo.GetProductWithAditionalData(id);
            if (product == null)
                return Redirect(referer);

            foreach(var img in image.Images)
            {
                UploadedFile? file = await _fileUploader.UploadFile(img);
                if (file != null)
                    product.ProductDetail.Images.Add(new Image()
                    {
                        Title = file.File.FileName,
                        Name = file.UploadedFileName
                    });
            }

            _pRepo.Save();
            return Redirect(referer);
        }

        private List<SelectListItem> GetProducers()
        {
            return _context.Producers.Select(p => new SelectListItem() { Text = p.Name, Value = p.Guid.ToString()}).ToList();
        }

        private List<SelectListItem> GetCategories()
        {
            return _context.ProductCategories.Select(p => new SelectListItem() { Text = p.Name, Value = p.Name }).ToList();
        }
    }
}
