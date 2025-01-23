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
    public class FavoriteSauceService : BaseService<FavoriteSauce>, IFavoriteSauceService
    {
        public FavoriteSauceService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddFavoriteSauceAsync(FavoriteSauceAddDTO dto)
        {
            var favoriteSauce = new FavoriteSauce
            {
                UserId = dto.UserId,
                SauceId = dto.SauceId,
                AddedDate = DateTime.UtcNow
            };
            await base.AddDTOAsync(favoriteSauce);
        }

        public async Task UpdateFavoriteSauceAsync(FavoriteSauceUpdateDTO dto)
        {
            var favoriteSauce = await base.GetByIdDTO(fs => fs.UserId == dto.UserId && fs.SauceId == dto.SauceId);
            if (favoriteSauce != null)
            {
                favoriteSauce.Notes = dto.Notes;
                await base.UpdateDTOAsync(favoriteSauce);
            }
            else
            {
                throw new Exception("Favori sos bulunamadı.");
            }
        }

        public async Task DeleteFavoriteSauceAsync(int sauceId, string userId)
        {
            var favoriteSauce = await base.GetByIdDTO(fs => fs.UserId == userId && fs.SauceId == sauceId);
            if (favoriteSauce != null)
            {
                await base.DeleteDTOAsync(favoriteSauce.Id);
            }
            else
            {
                throw new Exception("Favori sos bulunamadı.");
            }
        }

        public async Task<FavoriteSauceUpdateDTO> GetFavoriteSauceByIdAsync(int sauceId, string userId)
        {
            var favoriteSauce = await base.GetByIdDTO(fs => fs.UserId == userId && fs.SauceId == sauceId);
            if (favoriteSauce != null)
            {
                return new FavoriteSauceUpdateDTO
                {
                    UserId = favoriteSauce.UserId,
                    SauceId = favoriteSauce.SauceId,
                    Notes = favoriteSauce.Notes
                };
            }
            return null;
        }

        public async Task<List<FavoriteSauceListDTO>> GetAllFavoriteSaucesAsync(string userId)
        {
            var favoriteSauces = await base.GetByDTO(fs => fs.UserId == userId);
            return favoriteSauces.Select(fs => new FavoriteSauceListDTO
            {
                UserId = fs.UserId,
                SauceId = fs.SauceId,
                SauceName = fs.Sauce.Name, // Navigasyon özelliği varsa, burada kullanılır
                AddedDate = fs.AddedDate,
                Notes = fs.Notes
            }).ToList();
        }
    }
}
