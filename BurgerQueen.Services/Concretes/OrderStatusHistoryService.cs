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
    public class OrderStatusHistoryService : BaseService<OrderStatusHistory>, IOrderStatusHistoryService
    {
        public OrderStatusHistoryService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task<IEnumerable<OrderStatusHistoryListDTO>> GetStatusHistoryForOrderAsync(int orderId)
        {
            var statusHistories = await base.GetByDTO(osh => osh.OrderId == orderId);

            return statusHistories.Select(osh => new OrderStatusHistoryListDTO
            {
                Id = osh.Id,
                Status = osh.Status,
                ChangeTimestamp = osh.ChangeTimestamp
            });
        }
    }
}
