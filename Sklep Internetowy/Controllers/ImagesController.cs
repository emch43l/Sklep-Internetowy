using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Services.Interfaces;
using Sklep_Internetowy.ViewModels;
using Sklep_Internetowy.ViewModels.Models;

namespace Sklep_Internetowy.Controllers
{
    [Route("admin/images")]
    public class ImagesController : Controller
    {
        private readonly IProductService _productService;
        public ImagesController(IProductService service) 
        {
            _productService = service;
        }

        [Route("{id}", Name = "Images_Index")]
        [HttpGet]
        public async Task<IActionResult> Index(Guid id)
        {
            ProductViewModel? productViewModel = await _productService.GetModel(id);

            if (productViewModel == null)
                return NotFound();

            ViewData["Name"] = productViewModel.Name;
            ViewData["Images"] = productViewModel.CurrentImages;
            ViewData["ProductId"] = productViewModel.Id;

            return View(new ImageModel());
        }

        [Route("{id}")]
        [HttpPost]
        public async Task<IActionResult> Index(Guid Id, [FromForm] ImageModel images)
        {
            ProductViewModel? productViewModel = await _productService.GetModel(Id);

            if (productViewModel == null)
                return NotFound();

            ViewData["Name"] = productViewModel.Name;
            ViewData["Images"] = productViewModel.CurrentImages;

            if (ModelState.IsValid)
            {
                await _productService.AddImage(Id, images);

                if (_productService.GetErrorsCount() > 0)
                {
                    _productService.AddErrorsToModelState(ModelState);
                    return View(images);
                }

                return RedirectToRoute("Images_Index", new { id = Id });

            }

            return View(images);

           
        }

        [Route("delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteImage(Guid id, Guid imageId)
        {
            ProductViewModel? productViewModel = await _productService.GetModel(id);

            if (productViewModel == null)
                return NotFound();

            if (!await _productService.DeleteImage(id, imageId))
                return NotFound();

            return RedirectToRoute("Images_Index", new { id = id });
        }
    }
}
