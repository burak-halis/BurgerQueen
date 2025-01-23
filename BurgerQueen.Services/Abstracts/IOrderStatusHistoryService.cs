using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IOrderStatusHistoryService : IBaseService<OrderStatusHistory>
    {
        Task<IEnumerable<OrderStatusHistoryListDTO>> GetStatusHistoryForOrderAsync(int orderId);
    }
}
