using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using BurgerQueen.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IOrderService : IBaseService<Order>
    {
        Task<OrderListDTO> AddOrderDTO(OrderAddDTO dto);
        Task UpdateOrderDTO(OrderUpdateDTO dto);
        Task DeleteOrderDTO(int id);
        Task<OrderUpdateDTO> GetOrderById(int id);
        Task<List<OrderListDTO>> GetOrdersAll();
        Task<List<OrderItemListDTO>> GetOrderItemsByOrderId(int orderId);

        Task<IEnumerable<OrderListDTO>> GetUserOrders(string userId);


    }
}
