using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using BurgerQueen.UI.Models.VM.ReviewVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BurgerQueen.UI.Controllers
{
    public class ReviewController : Controller
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return View(reviews);
        }

        [Authorize(Roles = "User")]
        [HttpGet]
        public async Task<IActionResult> AddReview(int productId)
        {
            var product = await _reviewService.GetProductForReview(productId);
            if (product == null)
            {
                return NotFound();
            }

            var vm = new AddReviewVM
            {
                ProductId = productId,
                ProductName = product.Name
            };
            return View(vm);
        }

        [Authorize(Roles = "User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddReview(AddReviewVM model)
        {
            if (ModelState.IsValid)
            {
                var reviewDTO = new ReviewAddDTO
                {
                    ProductId = model.ProductId,
                    Title = model.Title,
                    Content = model.Content,
                    Rating = model.Rating,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                await _reviewService.AddReviewAsync(reviewDTO);
                return RedirectToAction("Details", "Product", new { id = model.ProductId });
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> EditReview(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }

            var vm = new EditReviewVM
            {
                Id = review.Id,
                ProductId = review.ProductId,
                Title = review.Title,
                Content = review.Content,
                Rating = review.Rating
            };
            return View(vm);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditReview(EditReviewVM model)
        {
            if (ModelState.IsValid)
            {
                var reviewDTO = new ReviewUpdateDTO
                {
                    Id = model.Id,
                    ProductId = model.ProductId,
                    Title = model.Title,
                    Content = model.Content,
                    Rating = model.Rating
                };

                await _reviewService.UpdateReviewAsync(reviewDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound();
            }
            return View(review);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteReview")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteReviewConfirmed(int id)
        {
            await _reviewService.DeleteReviewAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}