
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
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Sklep_Internetowy.Controllers
{
    [Authorize(Roles = "admin")]
    [Route("admin/products")]
    public class ProductsController : Controller
    {
        private readonly DataContext _context;

        private readonly string _appEnviroment;

        private readonly IProductService _pService;

        public ProductsController(IWebHostEnvironment environment, IProductService pService , DataContext context)
        {
            _context = context;
            _appEnviroment = environment.WebRootPath;
            _pService = pService; 
        }

        [Route("")]
        [HttpGet]
        public IActionResult Index(int page)
        {
            page = (page < 1) ? 1 : page;
            EntityPaginator<Product> paginator = new EntityPaginator<Product>(_context.Products.Include(p => p.ProductDetail).ToList());
            paginator.SetPageEntityNumber(10);

            IEnumerable<Product> products = paginator.GetPaginatedData(page);
            List<int> pagesNumber = paginator.GetPagesNumber(page);

            return View(new Tuple<IEnumerable<Product>,List<int>,int>(products,pagesNumber,page));
        }

        [Route("delete/{id}")]
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _pService.Delete(id);
            
            return RedirectToAction("Index");

        }

        [Route("create")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View(await _pService.GetModel(new ProductViewModel()));
        }

        [Route("create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductViewModel model)
        {

            if (ModelState.IsValid)
            {
                Product? product = await _pService.Add(model);
                if (await _pService.Add(model) != null)
                {
                    return RedirectToRoute("Images_Index", new { id = product.Guid });
                }

                if (_pService.GetErrorsCount() > 0)
                {
                    _pService.AddErrorsToModelState(ModelState);
                }

            }

            return View(await _pService.GetModel(model));
        }

        [Route("edit/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ProductViewModel? model = await _pService.GetModel(id);

            if(model == null)
                return NotFound();

            return View(model);
        }

        [Route("edit/{id}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductViewModel data)
        {
            if (ModelState.IsValid)
            {
                if (await _pService.Update(data) != null)
                {
                    return RedirectToAction("Index");
                }

                if (_pService.GetErrorsCount() > 0)
                {
                    _pService.AddErrorsToModelState(ModelState);
                }
            }

            ProductViewModel? model = await _pService.GetModel(data.Id);

            if (model == null)
                return NotFound();

            return View(model);


        }

        //[Route("/admin/products/images/{id}")]
        //public async Task<IActionResult> DeletImage(Guid pId, Guid iId)
        //{
        //    string? referer = HttpContext.Request.Headers["referer"];
        //    if (referer == null)
        //        referer = string.Empty;

        //    await _pService.DeleteImage(pId, iId);

        //    return View(await _pService.GetModel(pId));

        //}

        //[HttpPost]
        //[Route("/admin/products/images/edit/{id}")]
        //public async Task<IActionResult> AddImages(Guid id, [FromForm] ImageModel image)
        //{
        //    string? referer = HttpContext.Request.Headers["referer"];
        //    if (referer == null)
        //        referer = string.Empty;

        //    await _pService.AddImage(id, image);
        //    return View(await _pService.GetModel(id));
        //}
    }
}
