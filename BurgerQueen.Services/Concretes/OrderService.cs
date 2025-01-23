using BurgerQueen.ContextDb.Abstracts;
using BurgerQueen.ContextDb.Concretes;
using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using BurgerQueen.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Concretes
{
    public class OrderService : BaseService<Order>, IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBaseRepository<OrderItem> _orderItemRepository;

        public OrderService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _orderItemRepository = _unitOfWork.Repository<OrderItem>();
        }

        public async Task<OrderListDTO> AddOrderDTO(OrderAddDTO dto)
        {
            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Pending,
                DeliveryAddress = dto.DeliveryAddress,
                ExpectedDeliveryDate = dto.ExpectedDeliveryDate,
                PaymentMethod = dto.PaymentMethod,
                OrderNotes = dto.OrderNotes,
                TotalPrice = dto.OrderItems.Sum(item => item.Price * item.Quantity),
                ShippingFee = 0 // Bu değeri hesaplamak için mantık ekleyebilirsiniz
            };

            await base.AddDTOAsync(order);
            await _unitOfWork.SaveChangesAsync(); // Order'ı kaydettikten sonra ID'sini almak için save et

            foreach (var item in dto.OrderItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    MenuId = item.MenuId,
                    BurgerId = item.BurgerId,
                    DrinkId = item.DrinkId,
                    FriesId = item.FriesId,
                    SideItemId = item.SideItemId,
                    SauceId = item.SauceId,
                    BurgerSize = item.BurgerSize,
                    DrinkSize = item.DrinkSize,
                    FriesSize = item.FriesSize,
                    Quantity = item.Quantity,
                    Customizations = item.Customizations,
                    Price = item.Price
                };
                await _orderItemRepository.AddAsync(orderItem);
            }
            await _unitOfWork.SaveChangesAsync();

            return new OrderListDTO
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalPrice = order.TotalPrice
            };
        }

        public async Task UpdateOrderDTO(OrderUpdateDTO dto)
        {
            var order = await base.GetByIdDTO(o => o.Id == dto.Id);
            if (order != null)
            {
                order.DeliveryAddress = dto.DeliveryAddress;
                order.ExpectedDeliveryDate = dto.ExpectedDeliveryDate;
                order.PaymentMethod = dto.PaymentMethod;
                order.OrderNotes = dto.OrderNotes;
                order.Status = dto.Status;
                order.PaymentStatus = dto.PaymentStatus;

                await base.UpdateDTOAsync(order);
            }
            else
            {
                throw new Exception("Sipariş bulunamadı.");
            }
        }

        public async Task DeleteOrderDTO(int id)
        {
            await base.DeleteDTOAsync(id);
        }

        public async Task<OrderUpdateDTO> GetOrderById(int id)
        {
            var order = await base.GetByIdDTO(o => o.Id == id);
            if (order != null)
            {
                return new OrderUpdateDTO
                {
                    Id = order.Id,
                    DeliveryAddress = order.DeliveryAddress,
                    ExpectedDeliveryDate = order.ExpectedDeliveryDate,
                    PaymentMethod = order.PaymentMethod,
                    OrderNotes = order.OrderNotes,
                    Status = order.Status,
                    PaymentStatus = order.PaymentStatus
                };
            }
            return null;
        }

        public async Task<List<OrderListDTO>> GetOrdersAll()
        {
            var orders = await base.GetAllDTO();
            return orders.Select(o => new OrderListDTO
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                Status = o.Status,
                TotalPrice = o.TotalPrice
            }).ToList();
        }

        public async Task<List<OrderItemListDTO>> GetOrderItemsByOrderId(int orderId)
        {
            var items = await _orderItemRepository.GetBy(i => i.OrderId == orderId);
            return items.Select(i => new OrderItemListDTO
            {
                Id = i.Id,
                Name = i.Burger != null ? i.Burger.Name :
                      (i.Drink != null ? i.Drink.Name :
                      (i.Fries != null ? i.Fries.Name :
                      (i.SideItem != null ? i.SideItem.Name :
                      (i.Sauce != null ? i.Sauce.Name : "Unknown")))),
                Size = i.BurgerSize ?? i.DrinkSize ?? i.FriesSize ?? "",
                Quantity = i.Quantity,
                Price = i.Price
            }).ToList();
        }
    }
}
