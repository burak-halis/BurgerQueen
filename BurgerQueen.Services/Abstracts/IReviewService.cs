using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewListDTO>> GetAllReviewsAsync();
        Task<ReviewDTO> GetReviewByIdAsync(int id);
        Task AddReviewAsync(ReviewAddDTO review);
        Task UpdateReviewAsync(ReviewUpdateDTO review);
        Task DeleteReviewAsync(int id);
        Task<ProductDTO> GetProductForReview(int productId);
    }
}
