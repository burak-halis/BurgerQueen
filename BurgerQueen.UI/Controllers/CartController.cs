using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.UI.Models.VM.CartVM;


namespace BurgerQueen.UI.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [Authorize]
        public async Task<IActionResult> ViewCart()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartService.GetCartAsync(userId);
            return View(cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, string productType, int quantity = 1)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.AddToCartAsync(userId, productId, productType, quantity);
            return Json(new { success = true, message = "Ürün sepetinize eklendi." });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartService.RemoveFromCartAsync(userId, cartItemId);
            return Json(new { success = true, message = "Ürün sepetinizden çıkarıldı." });
        }

        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await _cartService.GetCartAsync(userId);
            return View(cart);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutVM model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var orderId = await _cartService.ProcessCheckoutAsync(userId, model.ToCheckoutDTO());

                if (orderId.HasValue)
                {
                    return RedirectToAction("OrderConfirmation", "Order", new { id = orderId.Value });
                }
            }
            // ModelState'in geçersiz olması durumunda
            var cart = await _cartService.GetCartAsync(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(cart);
        }
    }
}