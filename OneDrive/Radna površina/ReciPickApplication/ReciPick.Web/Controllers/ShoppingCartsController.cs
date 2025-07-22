using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReciPick.Service.Interface;

namespace ReciPick.Web.Controllers
{
    [Authorize]
    public class ShoppingCartsController : Controller
    {
        private readonly IShoppingCartService _shoppingCartService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService)
        {
            _shoppingCartService = shoppingCartService;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _shoppingCartService.GetCartByUserIdAsync(userId);
            var total = await _shoppingCartService.GetCartTotalAsync(userId);
            ViewBag.Total = total;
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> ClearCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _shoppingCartService.ClearCartAsync(userId);
            TempData["Message"] = "Cart cleared successfully!";
            return RedirectToAction(nameof(Index));
        }
    }
}