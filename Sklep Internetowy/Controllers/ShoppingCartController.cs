using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Models;
using Sklep_Internetowy.Repositories.Interfaces;
using Sklep_Internetowy.ViewModels;
using Sklep_Internetowy.ViewModels.Models;

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
            return View(await _cRepo.GetCartByUser(user));
        }

        [Route("cart/add")]
        [HttpPost]
        public async Task<IActionResult> Create(AddToCartViewModel item)
        {
            if(ModelState.IsValid)
            {
                AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
                Product? product = await _pRepo.GetOneByGuid(item.ProductId);

                if (user == null || product == null)
                {
                    return NotFound();
                }

                Cart? cart = await _cRepo.GetCartByUser(user);
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


                await _cRepo.SaveChanges();

                return RedirectToAction("Index", "ShoppingCart");
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Delete(Guid ProductId)
        {

            AppUser? user = await _userManager.GetUserAsync(HttpContext.User);
            Product? product = await _pRepo.GetOneByGuid(ProductId);
            if (user == null || product == null)
            {
                return RedirectToAction("Index");
            }

            Cart? cart = await _cRepo.GetCartByUser(user);

            if(cart == null)
                return RedirectToAction("Index");

            cart.Items.Remove(cart.Items.Where(c => c.ProductId == product.Id).FirstOrDefault());
            await _cRepo.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
