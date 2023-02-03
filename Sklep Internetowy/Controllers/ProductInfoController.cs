using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.Services;
using Sklep_Internetowy.Services.Interfaces;
using Sklep_Internetowy.ViewModels.Models;

namespace Sklep_Internetowy.Controllers
{
    public class ProductInfoController : Controller
    {
        private readonly IProductRepository _pRepo;

        private readonly IDirectoryConfigurationReader _reader;

        private readonly UserManager<AppUser> _userManager;

        private readonly SignInManager<AppUser> _signInManager;

        public ProductInfoController(
            IProductRepository pRepo, 
            IDirectoryConfigurationReader reader,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _pRepo = pRepo;
            _reader = reader;
            _signInManager = signInManager;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "user,admin")]
        public async Task<IActionResult> AddOpinion(AddOpinionModel opinion) 
        {
            if(ModelState.IsValid)
            {
                Product? product = await _pRepo.GetProductWithAssociatedEntities(opinion.Id);
                AppUser? user = await _userManager.GetUserAsync(Request.HttpContext.User);

                if (product != null && user != null)
                {
                    product.Rating.Add(new ProductRating
                    {
                        Description = opinion.Text,
                        Rating = opinion.Rating,
                        User = user
                    });

                    await _pRepo.SaveChanges();
                    return RedirectToAction("Index", "ProductInfo" , new { id = opinion.Id });
                }

            }

            return RedirectToAction("Index", "Home");

        }

        public async Task<IActionResult> Index(Guid id, string? ErrorMessage)
        {
            if (ErrorMessage != null)
                ModelState.AddModelError("errorMsg", ErrorMessage);
            ViewData["ImagesPath"] = _reader.GetDirectory(TargetFolder.Images);
            Product? product = await _pRepo.GetProductWithAssociatedEntities(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(new Tuple<Product,AddToCartViewModel>(product,new AddToCartViewModel() { ProductId = product.Guid.ToString() }));
        }
    }
}
