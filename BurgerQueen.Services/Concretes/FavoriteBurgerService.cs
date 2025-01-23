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
    public class FavoriteBurgerService : BaseService<FavoriteBurger>, IFavoriteBurgerService
    {
        public FavoriteBurgerService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddFavoriteBurgerAsync(FavoriteBurgerAddDTO dto)
        {
            var favoriteBurger = new FavoriteBurger
            {
                UserId = dto.UserId,
                BurgerId = dto.BurgerId,
                AddedDate = DateTime.UtcNow
            };
            await base.AddDTOAsync(favoriteBurger);
        }

        public async Task UpdateFavoriteBurgerAsync(FavoriteBurgerUpdateDTO dto)
        {
            var favoriteBurger = await base.GetByIdDTO(fb => fb.UserId == dto.UserId && fb.BurgerId == dto.BurgerId);
            if (favoriteBurger != null)
            {
                favoriteBurger.Notes = dto.Notes;
                await base.UpdateDTOAsync(favoriteBurger);
            }
            else
            {
                throw new Exception("Favori burger bulunamadı.");
            }
        }

        public async Task DeleteFavoriteBurgerAsync(int burgerId, string userId)
        {
            var favoriteBurger = await base.GetByIdDTO(fb => fb.UserId == userId && fb.BurgerId == burgerId);
            if (favoriteBurger != null)
            {
                await base.DeleteDTOAsync(favoriteBurger.Id);
            }
            else
            {
                throw new Exception("Favori burger bulunamadı.");
            }
        }

        public async Task<FavoriteBurgerUpdateDTO> GetFavoriteBurgerByIdAsync(int burgerId, string userId)
        {
            var favoriteBurger = await base.GetByIdDTO(fb => fb.UserId == userId && fb.BurgerId == burgerId);
            if (favoriteBurger != null)
            {
                return new FavoriteBurgerUpdateDTO
                {
                    UserId = favoriteBurger.UserId,
                    BurgerId = favoriteBurger.BurgerId,
                    Notes = favoriteBurger.Notes
                };
            }
            return null;
        }

        public async Task<List<FavoriteBurgerListDTO>> GetAllFavoriteBurgersAsync(string userId)
        {
            var favoriteBurgers = await base.GetByDTO(fb => fb.UserId == userId);
            return favoriteBurgers.Select(fb => new FavoriteBurgerListDTO
            {
                UserId = fb.UserId,
                BurgerId = fb.BurgerId,
                BurgerName = fb.Burger.Name, // Navigasyon özelliği varsa, burada kullanılır
                AddedDate = fb.AddedDate,
                Notes = fb.Notes
            }).ToList();
        }
    }
}
