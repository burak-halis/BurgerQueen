using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using BurgerQueen.UI.Models.VM.SideItemVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerQueen.UI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SideItemController : Controller
    {
        private readonly ISideItemService _sideItemService;

        public SideItemController(ISideItemService sideItemService)
        {
            _sideItemService = sideItemService;
        }

        public async Task<IActionResult> Index()
        {
            var sideItems = await _sideItemService.GetSideItemsAll();
            return View(sideItems);
        }

        public async Task<IActionResult> Details(int id)
        {
            var sideItem = await _sideItemService.GetSideItemById(id);
            if (sideItem == null)
            {
                return NotFound();
            }
            return View(sideItem);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SideItemAddVM model)
        {
            if (ModelState.IsValid)
            {
                await _sideItemService.AddSideItemDTO(model.ToSideItemAddDTO()); // ViewModel'den DTO'ya dönüşüm
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var sideItem = await _sideItemService.GetSideItemById(id);
            if (sideItem == null)
            {
                return NotFound();
            }
            return View(sideItem.ToSideItemUpdateVM()); // DTO'dan ViewModel'e dönüşüm
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(SideItemUpdateVM model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _sideItemService.UpdateSideItemDTO(model.ToSideItemUpdateDTO()); // ViewModel'den DTO'ya dönüşüm
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    ModelState.AddModelError("", "Yan ürün güncellenirken bir hata oluştu.");
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var sideItem = await _sideItemService.GetSideItemById(id);
            if (sideItem == null)
            {
                return NotFound();
            }
            return View(sideItem);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _sideItemService.DeleteSideItemDTO(id);
            return RedirectToAction(nameof(Index));
        }
    }
}