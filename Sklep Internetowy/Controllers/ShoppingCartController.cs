using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.ViewModels;

namespace Sklep_Internetowy.Controllers
{
    [Authorize(Roles = "user,admin")]
    public class ShoppingCartController : Controller
    {

        private readonly UserManager<AppUser> _userManager;

        private readonly ICartRepository _cRepo;

        private readonly IProductRepository _pRepo;

        public ShoppingCartController(UserManager<AppUser> userManager, ICartRepository cRepo, IProductRepository pRepo)
        {
            _pRepo= pRepo;
            _cRepo = cRepo;
            _userManager = userManager;
        }

        [Route("/cart")]
        public async Task <IActionResult> Index()
        {
            AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
            if (user == null)
            {
                return NotFound();
            }
            return View(_cRepo.GetUserCart(user));
        }

        [Route("cart/add")]
        [HttpPost]
        public async Task<IActionResult> Create(AddToCartModel item)
        {
            if(ModelState.IsValid)
            {
                AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
                Product? product = _pRepo.GetProductByGuid(item.ProductId);

                if (user == null || product == null)
                {
                    return NotFound();
                }

                Cart cart = _cRepo.GetUserCart(user);
                CartItem? cartItem = cart.Items.FirstOrDefault(c => c.Product == product);

                if (cartItem == null)
                {
                    cart.Items.Add(new CartItem
                    {
                        Product = product,
                        CreationDate = DateTime.Now,
                        Count = item.Count
                    });
                }
                else
                {
                    cartItem.Count += item.Count;
                }


                _cRepo.Save();

                return RedirectToAction("Index", "ShoppingCart");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(string ProductId)
        {

            AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
            Product? product = _pRepo.GetProductByGuid(ProductId);
            if (user == null || product == null)
            {
                return RedirectToAction("Index");
            }

            Cart cart = _cRepo.GetUserCart(user);
            cart.Items.Remove(cart.Items.Where(c => c.ProductId == product.Id).FirstOrDefault());
            _cRepo.Save();
            return RedirectToAction("Index");
        }
    }
}
