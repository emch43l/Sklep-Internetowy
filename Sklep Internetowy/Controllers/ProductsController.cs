
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Services;
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

        private readonly IProductService _pService;

        public ProductsController(IWebHostEnvironment environment, IProductService pService , DataContext context)
        {
            _context = context;
            _appEnviroment = environment.WebRootPath;
            _pService = pService; 
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
            await _pService.Delete(id);
            
            return RedirectToAction("Index");

        }

        [Route("/admin/products/create")]
        public async Task<IActionResult> Create()
        {
            List<SelectListItem> producersSelectList = new List<SelectListItem>();

            (await _context.Producers.ToListAsync())
                .ForEach(p => producersSelectList.Add( new SelectListItem( p.Name, p.Guid.ToString() )));

            return View(new CreateProductViewModel { 
                Producers = producersSelectList, 
                Categories = _context.ProductCategories.ToList()
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/products/create")]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                if (await _pService.Add(model) != null)
                    RedirectToAction("Index");

                if (_pService.GetErrorsCount() > 0)
                {
                    foreach (var error in _pService.GetErrors())
                        ModelState.AddModelError(error.Key, error.Value);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Could not create product !");
                }

            }

            return View(_pService.GetModel(model));
        }

        [HttpGet]
        [Route("/admin/products/edit/{id}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            ProductEditViewModel? model = await _pService.GetModel(id);
            if(model == null)
                return NotFound();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("/admin/products/edit/")]
        public async Task<IActionResult> Edit(ProductEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (await _pService.Update(model) != null)
                    return RedirectToAction("Index");

                if (_pService.GetErrorsCount() > 0)
                {
                    foreach (var error in _pService.GetErrors())
                        ModelState.AddModelError(error.Key, error.Value);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Could not update product !");
                }
            }

            return await this.Edit(model.Id);


        }

        [Route("/admin/products/image/delete/{pId}/{iId}")]
        public async Task<IActionResult> DeleteProductImage(Guid pId, Guid iId)
        {
            string? referer = HttpContext.Request.Headers["referer"];
            if (referer == null)
                referer = string.Empty;

            await _pService.DeleteImage(pId, iId);


            return Redirect(referer);
            
        }

        [HttpPost]
        [Route("/admin/products/image/add/{id}")]
        public async Task<IActionResult> AddProductImage(Guid id, [FromForm] ImageModel images)
        {
            string? referer = HttpContext.Request.Headers["referer"];
            if (referer == null)
                referer = string.Empty;

            await _pService.AddImage(id, images);
            return Redirect(referer);
        }
    }
}
