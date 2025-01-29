using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface ICartService
    {
        Task<CartDTO> GetCartAsync(string userId);
        Task AddToCartAsync(string userId, int productId, string productType, int quantity = 1);
        Task RemoveFromCartAsync(string userId, int cartItemId);
        Task<int?> ProcessCheckoutAsync(string userId, CheckoutDTO checkoutInfo);
    }
}
