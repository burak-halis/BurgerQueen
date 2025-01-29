using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using BurgerQueen.UI.Models.VM.PromotionVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerQueen.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PromotionController : Controller
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        public async Task<IActionResult> Index()
        {
            var promotions = await _promotionService.GetAllPromotionsAsync();
            return View(promotions);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PromotionAddVM model)
        {
            if (ModelState.IsValid)
            {
                await _promotionService.AddPromotionAsync(model.ToPromotionAddDTO());
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var promotion = await _promotionService.GetPromotionByIdAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }

            var promotionVM = new PromotionUpdateVM
            {
                Id = promotion.Id,
                Name = promotion.Name,
                Description = promotion.Description,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate,
                DiscountPercentage = promotion.DiscountPercentage,
                IsActive = promotion.IsActive
            };

            return View(promotionVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PromotionUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _promotionService.UpdatePromotionAsync(model.ToPromotionUpdateDTO());
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Promosyon güncellenirken bir hata oluştu.");
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var promotion = await _promotionService.GetPromotionByIdAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }
            return View(promotion);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _promotionService.DeletePromotionAsync(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> ApplyPromotion(int id)
        {
            var promotion = await _promotionService.ApplyPromotionAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}