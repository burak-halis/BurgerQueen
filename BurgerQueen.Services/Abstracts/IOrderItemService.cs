using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IOrderItemService : IBaseService<OrderItem>
    {
        Task<IEnumerable<OrderItemListDTO>> GetItemsForOrderAsync(int orderId);
    }
}
