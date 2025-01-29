using BurgerQueen.ContextDb.Abstracts;
using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Concretes
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CartService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CartDTO> GetCartAsync(string userId)
        {
            var cartItems = await _unitOfWork.Repository<CartItem>().GetByAsync(ci => ci.UserId == userId);
            var cartDTO = new CartDTO
            {
                Items = new List<CartItemDTO>()
            };

            foreach (var cartItem in cartItems)
            {
                // Ürün türüne göre ilgili entity'yi çekiyoruz
                object product = null;
                switch (cartItem.ProductType)
                {
                    case "Burger":
                        product = await _unitOfWork.Repository<Burger>().GetByIdAsync(b => b.Id == cartItem.ProductId);
                        break;
                    case "Drink":
                        product = await _unitOfWork.Repository<Drink>().GetByIdAsync(d => d.Id == cartItem.ProductId);
                        break;
                    case "Fry":
                        product = await _unitOfWork.Repository<Fry>().GetByIdAsync(f => f.Id == cartItem.ProductId);
                        break;
                    case "SideItem":
                        product = await _unitOfWork.Repository<SideItem>().GetByIdAsync(si => si.Id == cartItem.ProductId);
                        break;
                    case "Sauce":
                        product = await _unitOfWork.Repository<Sauce>().GetByIdAsync(s => s.Id == cartItem.ProductId);
                        break;
                    case "Menu":
                        product = await _unitOfWork.Repository<Menu>().GetByIdAsync(m => m.Id == cartItem.ProductId);
                        break;
                }

                if (product != null)
                {
                    // Ürünün özelliklerini CartItemDTO'ya dönüştürüyoruz
                    var itemDTO = new CartItemDTO
                    {
                        Id = cartItem.Id,
                        Name = GetProductName(product),
                        Type = cartItem.ProductType,
                        ImagePath = GetProductImagePath(product),
                        Price = GetProductPrice(product),
                        Quantity = cartItem.Quantity
                    };
                    cartDTO.Items.Add(itemDTO);
                }
            }

            cartDTO.TotalPrice = cartDTO.Items.Sum(i => i.Price * i.Quantity);
            return cartDTO;
        }

        public async Task AddToCartAsync(string userId, int productId, string productType, int quantity = 1)
        {
            var cartItem = new CartItem
            {
                UserId = userId,
                ProductId = productId,
                ProductType = productType,
                Quantity = quantity
            };

            await _unitOfWork.Repository<CartItem>().AddAsync(cartItem);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(string userId, int cartItemId)
        {
            var cartItem = await _unitOfWork.Repository<CartItem>().FirstOrDefaultAsync(ci => ci.Id == cartItemId && ci.UserId == userId);
            if (cartItem != null)
            {
                await _unitOfWork.Repository<CartItem>().DeleteAsync(cartItem);
                await _unitOfWork.SaveChangesAsync();
            }
        }

        public async Task<int?> ProcessCheckoutAsync(string userId, CheckoutDTO checkoutInfo)
        {
            // TODO: Bu metod, ödeme işlemi mantığını içerecek. Şimdilik basit bir örnek:
            var cart = await GetCartAsync(userId);
            if (cart.Items.Any())
            {
                // Burada bir Order oluşturulabilir ve sepet temizlenebilir. 
                // Şimdilik sadece bir sipariş ID'si döndürelim:
                var orderId = new Random().Next(1000, 1000000); // Gerçek bir order ID oluşturma işlemi burada olmalıdır.
                await ClearCartAsync(userId);
                return orderId;
            }
            return null;
        }

        // Private metodlar, interface'te tanımlanmaz.
        private async Task ClearCartAsync(string userId)
        {
            var cartItems = await _unitOfWork.Repository<CartItem>().GetByAsync(ci => ci.UserId == userId);
            foreach (var item in cartItems)
            {
                await _unitOfWork.Repository<CartItem>().DeleteAsync(item);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        private string GetProductName(object product)
        {
            // Ürün türüne göre adı döndürür
            if (product is Burger burger) return burger.Name;
            if (product is Drink drink) return drink.Name;
            if (product is Fry fry) return fry.Name;
            if (product is SideItem sideItem) return sideItem.Name;
            if (product is Sauce sauce) return sauce.Name;
            if (product is Menu menu) return menu.Name;
            return string.Empty;
        }

        private string GetProductImagePath(object product)
        {
            // Ürün türüne göre resim yolunu döndürür
            if (product is Burger burger) return burger.ImagePath;
            if (product is Drink drink) return drink.ImagePath;
            if (product is Fry fry) return fry.ImagePath;
            if (product is SideItem sideItem) return sideItem.ImagePath;
            if (product is Sauce sauce) return sauce.ImagePath;
            if (product is Menu menu) return menu.ImagePath;
            return string.Empty;
        }

        private decimal GetProductPrice(object product)
        {
            // Ürün türüne göre fiyatı döndürür
            if (product is Burger burger) return burger.Price;
            if (product is Drink drink) return drink.Price;
            if (product is Fry fry) return fry.Price;
            if (product is SideItem sideItem) return sideItem.Price;
            if (product is Sauce sauce) return sauce.Price;
            if (product is Menu menu) return menu.TotalPrice ?? 0;
            return 0;
        }
    }
}

