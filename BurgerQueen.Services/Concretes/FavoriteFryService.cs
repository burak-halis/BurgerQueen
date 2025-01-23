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
    public class FavoriteFryService : BaseService<FavoriteFry>, IFavoriteFryService
    {
        public FavoriteFryService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddFavoriteFryAsync(FavoriteFryAddDTO dto)
        {
            var favoriteFry = new FavoriteFry
            {
                UserId = dto.UserId,
                FriesId = dto.FriesId,
                AddedDate = DateTime.UtcNow
            };
            await base.AddDTOAsync(favoriteFry);
        }

        public async Task UpdateFavoriteFryAsync(FavoriteFryUpdateDTO dto)
        {
            var favoriteFry = await base.GetByIdDTO(ff => ff.UserId == dto.UserId && ff.FriesId == dto.FriesId);
            if (favoriteFry != null)
            {
                favoriteFry.Notes = dto.Notes;
                await base.UpdateDTOAsync(favoriteFry);
            }
            else
            {
                throw new Exception("Favori patates kızartması bulunamadı.");
            }
        }

        public async Task DeleteFavoriteFryAsync(int friesId, string userId)
        {
            var favoriteFry = await base.GetByIdDTO(ff => ff.UserId == userId && ff.FriesId == friesId);
            if (favoriteFry != null)
            {
                await base.DeleteDTOAsync(favoriteFry.Id);
            }
            else
            {
                throw new Exception("Favori patates kızartması bulunamadı.");
            }
        }

        public async Task<FavoriteFryUpdateDTO> GetFavoriteFryByIdAsync(int friesId, string userId)
        {
            var favoriteFry = await base.GetByIdDTO(ff => ff.UserId == userId && ff.FriesId == friesId);
            if (favoriteFry != null)
            {
                return new FavoriteFryUpdateDTO
                {
                    UserId = favoriteFry.UserId,
                    FriesId = favoriteFry.FriesId,
                    Notes = favoriteFry.Notes
                };
            }
            return null;
        }

        public async Task<List<FavoriteFryListDTO>> GetAllFavoriteFriesAsync(string userId)
        {
            var favoriteFries = await base.GetByDTO(ff => ff.UserId == userId);
            return favoriteFries.Select(ff => new FavoriteFryListDTO
            {
                UserId = ff.UserId,
                FriesId = ff.FriesId,
                FriesName = ff.Fries.Name, // Navigasyon özelliği varsa, burada kullanılır
                AddedDate = ff.AddedDate,
                Notes = ff.Notes
            }).ToList();
        }
    }
}
