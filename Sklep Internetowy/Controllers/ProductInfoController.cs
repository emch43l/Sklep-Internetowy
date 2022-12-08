using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.Services;
using Sklep_Internetowy.ViewModels;
using System.Reflection.PortableExecutable;

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
            string referer = Request.Headers["Referer"].ToString();
            if(ModelState.IsValid)
            {
                Product? product = _pRepo.GetProductWithAditionalData(opinion.Id);
                AppUser? user = await _userManager.GetUserAsync(Request.HttpContext.User);
                if (product != null && user != null)
                {
                    product.Rating.Add(new ProductRating
                    {
                        Description = opinion.Text,
                        Rating = opinion.Rating,
                        User = user
                    });

                    _pRepo.Save();
                }
            }

            return Redirect(referer);
        }

        public IActionResult Index(string id)
        {
            ViewData["ImagesPath"] = _reader.GetDirectory(TargetFolder.Images);
            Product? product = (id == null) ? null : _pRepo.GetProductWithAditionalData(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(new Tuple<Product,AddToCartModel>(product,new AddToCartModel() { ProductId = product.Guid.ToString() }));
        }
    }
}
