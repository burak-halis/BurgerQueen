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
    public class PromotionService : BaseService<Promotion>, IPromotionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PromotionService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PromotionListDTO>> GetAllPromotionsAsync()
        {
            var promotions = await base.GetAllDTO();
            return promotions.Select(p => new PromotionListDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                StartDate = p.StartDate,
                EndDate = p.EndDate,
                DiscountPercentage = p.DiscountPercentage,
                IsActive = p.IsActive
            }).ToList();
        }

        public async Task<PromotionUpdateDTO> GetPromotionByIdAsync(int id)
        {
            var promotion = await base.GetByIdDTO(p => p.Id == id);
            if (promotion == null)
            {
                return null;
            }
            return new PromotionUpdateDTO
            {
                Id = promotion.Id,
                Name = promotion.Name,
                Description = promotion.Description,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate,
                DiscountPercentage = promotion.DiscountPercentage,
                IsActive = promotion.IsActive
            };
        }

        public async Task<PromotionListDTO> AddPromotionAsync(PromotionAddDTO promotionDTO)
        {
            var promotion = new Promotion
            {
                Name = promotionDTO.Name,
                Description = promotionDTO.Description,
                StartDate = promotionDTO.StartDate,
                EndDate = promotionDTO.EndDate,
                DiscountPercentage = promotionDTO.DiscountPercentage,
                IsActive = true // Yeni eklenen bir promosyon varsayılan olarak aktif olabilir
            };

            await base.AddDTOAsync(promotion);
            await _unitOfWork.SaveChangesAsync();

            return new PromotionListDTO
            {
                Id = promotion.Id,
                Name = promotion.Name,
                Description = promotion.Description,
                StartDate = promotion.StartDate,
                EndDate = promotion.EndDate,
                DiscountPercentage = promotion.DiscountPercentage,
                IsActive = promotion.IsActive
            };
        }

        public async Task UpdatePromotionAsync(PromotionUpdateDTO promotionDTO)
        {
            var promotion = await base.GetByIdDTO(p => p.Id == promotionDTO.Id);
            if (promotion != null)
            {
                promotion.Name = promotionDTO.Name;
                promotion.Description = promotionDTO.Description;
                promotion.StartDate = promotionDTO.StartDate;
                promotion.EndDate = promotionDTO.EndDate;
                promotion.DiscountPercentage = promotionDTO.DiscountPercentage;
                promotion.IsActive = promotionDTO.IsActive;

                await base.UpdateDTOAsync(promotion);
            }
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeletePromotionAsync(int id)
        {
            await base.DeleteDTOAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<PromotionUpdateDTO> ApplyPromotionAsync(int id)
        {
            var promotion = await base.GetByIdDTO(p => p.Id == id);
            if (promotion != null)
            {
                promotion.IsActive = true; // Promosyonu aktif hale getiriyoruz
                await base.UpdateDTOAsync(promotion);
                await _unitOfWork.SaveChangesAsync();

                return new PromotionUpdateDTO
                {
                    Id = promotion.Id,
                    Name = promotion.Name,
                    Description = promotion.Description,
                    StartDate = promotion.StartDate,
                    EndDate = promotion.EndDate,
                    DiscountPercentage = promotion.DiscountPercentage,
                    IsActive = promotion.IsActive
                };
            }
            return null;
        }
    }
}
