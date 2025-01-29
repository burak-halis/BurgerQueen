using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IPromotionService
    {
        Task<IEnumerable<PromotionListDTO>> GetAllPromotionsAsync();
        Task<PromotionUpdateDTO> GetPromotionByIdAsync(int id);
        Task<PromotionListDTO> AddPromotionAsync(PromotionAddDTO promotion);
        Task UpdatePromotionAsync(PromotionUpdateDTO promotion);
        Task DeletePromotionAsync(int id);
        Task<PromotionUpdateDTO> ApplyPromotionAsync(int id);
    }
}
