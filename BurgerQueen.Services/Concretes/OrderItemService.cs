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
    public class OrderItemService : BaseService<OrderItem>, IOrderItemService
    {
        public OrderItemService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IEnumerable<OrderItemListDTO>> GetItemsForOrderAsync(int orderId)
        {
            var orderItems = await base.GetByDTO(oi => oi.OrderId == orderId);

            return orderItems.Select(oi => new OrderItemListDTO
            {
                Id = oi.Id,
                Name = oi.BurgerId.HasValue ? "Burger" :
                       (oi.DrinkId.HasValue ? "Drink" :
                       (oi.FriesId.HasValue ? "Fries" :
                       (oi.SideItemId.HasValue ? "Side Item" :
                       (oi.SauceId.HasValue ? "Sauce" : "Unknown")))),
                // Burada sadece bir örnek ad kullanıldı. Gerçek uygulamada, bağlı ürünün adı alınarak kullanılmalı.
                Size = oi.BurgerSize ?? oi.DrinkSize ?? oi.FriesSize ?? "",
                Quantity = oi.Quantity,
                Price = oi.Price
            });
        }
    }
}
