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
    public class ReviewService : BaseService<Review>, IReviewService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReviewService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ReviewListDTO>> GetAllReviewsAsync()
        {
            var reviews = await base.GetAllDTO();
            return reviews.Select(r =>
            {
                var productName = "";
                if (r.BurgerId.HasValue) productName = r.Burger.Name;
                else if (r.DrinkId.HasValue) productName = r.Drink.Name;
                else if (r.FryId.HasValue) productName = r.Fry.Name;
                else if (r.SideItemId.HasValue) productName = r.SideItem.Name;
                else if (r.SauceId.HasValue) productName = r.Sauce.Name;
                else if (r.MenuId.HasValue) productName = r.Menu.Name;

                return new ReviewListDTO
                {
                    Id = r.Id,
                    Title = r.Title,
                    Content = r.Content,
                    Rating = r.Rating,
                    ProductName = productName,
                    UserName = r.User.UserName
                };
            });
        }

        public async Task<ReviewDTO> GetReviewByIdAsync(int id)
        {
            var review = await base.GetByIdDTO(r => r.Id == id);
            if (review == null) return null;

            return new ReviewDTO
            {
                Id = review.Id,
                Title = review.Title,
                Content = review.Content,
                Rating = review.Rating,
                ProductId = review.ProductId,
                UserId = review.UserId
            };
        }

        public async Task AddReviewAsync(ReviewAddDTO reviewDTO)
        {
            var review = new Review
            {
                Title = reviewDTO.Title,
                Content = reviewDTO.Content,
                Rating = reviewDTO.Rating,
                ProductId = reviewDTO.ProductId,
                UserId = reviewDTO.UserId,
                CreatedDate = DateTime.UtcNow
            };

            // Burada, ProductId'ye dayalı olarak uygun ürün türünün Id'sini ayarlayabilirsiniz.
            // Örneğin:
            // if (reviewDTO.ProductType == "Burger") review.BurgerId = reviewDTO.ProductId;
            // Bu, uygulamanızın mantığına göre değişir.

            await base.AddDTOAsync(review);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateReviewAsync(ReviewUpdateDTO reviewDTO)
        {
            var review = await base.GetByIdDTO(r => r.Id == reviewDTO.Id);
            if (review != null)
            {
                review.Title = reviewDTO.Title;
                review.Content = reviewDTO.Content;
                review.Rating = reviewDTO.Rating;
                review.ModifiedDate = DateTime.UtcNow;

                await base.UpdateDTOAsync(review);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteReviewAsync(int id)
        {
            await base.DeleteDTOAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<ProductDTO> GetProductForReview(int productId)
        {
            // Bu metod, review eklenirken hangi ürüne yorum yapılacağını belirlemek için kullanılabilir.
            // Hangi ürün türüne ait olduğunu kontrol etmeliyiz.
            ProductDTO productDTO = null;

            if (await _unitOfWork.Repository<Burger>().AnyAsync(b => b.Id == productId))
            {
                var burger = await _unitOfWork.Repository<Burger>().GetByIdAsync(b => b.Id == productId);
                productDTO = new ProductDTO { Id = burger.Id, Name = burger.Name };
            }
            else if (await _unitOfWork.Repository<Drink>().AnyAsync(d => d.Id == productId))
            {
                var drink = await _unitOfWork.Repository<Drink>().GetByIdAsync(d => d.Id == productId);
                productDTO = new ProductDTO { Id = drink.Id, Name = drink.Name };
            }
            // Diğer ürün türleri için devam edin...

            return productDTO;
        }
    }
}
