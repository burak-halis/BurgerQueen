﻿using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using BurgerQueen.Shared.Enums;
using BurgerQueen.UI.Models.VM.OrderVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BurgerQueen.UI.Controllers
{
    [Authorize(Roles = "Admin, User")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IOrderItemService _orderItemService;

        public OrderController(IOrderService orderService, IOrderItemService orderItemService)
        {
            _orderService = orderService;
            _orderItemService = orderItemService;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderService.GetOrdersAll();
            return View(orders);
        }

        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderVM = new OrderDetailsVM
            {
                Order = new OrderUpdateVM
                {
                    Id = order.Id,
                    DeliveryAddress = order.DeliveryAddress,
                    ExpectedDeliveryDate = order.ExpectedDeliveryDate,
                    PaymentMethod = order.PaymentMethod,
                    OrderNotes = order.OrderNotes,
                    Status = order.Status,
                    PaymentStatus = order.PaymentStatus,
                    OrderDate = order.OrderDate,
                    TotalPrice = order.TotalPrice
                },
                OrderItems = await _orderItemService.GetItemsForOrderAsync(id)
            };

            return View(orderVM);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderAddVM model)
        {
            if (ModelState.IsValid)
            {
                var orderDTO = await _orderService.AddOrderDTO(model.ToOrderAddDTO()); // ViewModel'den DTO'ya dönüşüm
                return RedirectToAction(nameof(Details), new { id = orderDTO.Id });
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderVM = new OrderUpdateVM
            {
                Id = order.Id,
                DeliveryAddress = order.DeliveryAddress,
                ExpectedDeliveryDate = order.ExpectedDeliveryDate,
                PaymentMethod = order.PaymentMethod,
                OrderNotes = order.OrderNotes,
                Status = order.Status,
                PaymentStatus = order.PaymentStatus,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice
            };

            return View(orderVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(OrderUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _orderService.UpdateOrderDTO(model.ToOrderUpdateDTO()); // ViewModel'den DTO'ya dönüşüm
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Sipariş güncellenirken bir hata oluştu.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Cancel(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelConfirmed(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }

            var orderUpdateDTO = new OrderUpdateDTO
            {
                Id = order.Id,
                DeliveryAddress = order.DeliveryAddress,
                ExpectedDeliveryDate = order.ExpectedDeliveryDate,
                PaymentMethod = order.PaymentMethod,
                OrderNotes = order.OrderNotes,
                Status = OrderStatus.Cancelled,
                PaymentStatus = order.PaymentStatus,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice
            };

            await _orderService.UpdateOrderDTO(orderUpdateDTO);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> OrderHistory()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = await _orderService.GetUserOrders(userId);
            return View(orders);
        }

        public async Task<IActionResult> TrackOrder(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(new OrderUpdateVM
            {
                Id = order.Id,
                DeliveryAddress = order.DeliveryAddress,
                ExpectedDeliveryDate = order.ExpectedDeliveryDate,
                PaymentMethod = order.PaymentMethod,
                OrderNotes = order.OrderNotes,
                Status = order.Status,
                PaymentStatus = order.PaymentStatus,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice
            });
        }
    }
}