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
    public class FavoriteDrinkService : BaseService<FavoriteDrink>, IFavoriteDrinkService
    {
        public FavoriteDrinkService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddFavoriteDrinkAsync(FavoriteDrinkAddDTO dto)
        {
            var favoriteDrink = new FavoriteDrink
            {
                UserId = dto.UserId,
                DrinkId = dto.DrinkId,
                AddedDate = DateTime.UtcNow
            };
            await base.AddDTOAsync(favoriteDrink);
        }

        public async Task UpdateFavoriteDrinkAsync(FavoriteDrinkUpdateDTO dto)
        {
            var favoriteDrink = await base.GetByIdDTO(fd => fd.UserId == dto.UserId && fd.DrinkId == dto.DrinkId);
            if (favoriteDrink != null)
            {
                favoriteDrink.Notes = dto.Notes;
                await base.UpdateDTOAsync(favoriteDrink);
            }
            else
            {
                throw new Exception("Favori içecek bulunamadı.");
            }
        }

        public async Task DeleteFavoriteDrinkAsync(int drinkId, string userId)
        {
            var favoriteDrink = await base.GetByIdDTO(fd => fd.UserId == userId && fd.DrinkId == drinkId);
            if (favoriteDrink != null)
            {
                await base.DeleteDTOAsync(favoriteDrink.Id);
            }
            else
            {
                throw new Exception("Favori içecek bulunamadı.");
            }
        }

        public async Task<FavoriteDrinkUpdateDTO> GetFavoriteDrinkByIdAsync(int drinkId, string userId)
        {
            var favoriteDrink = await base.GetByIdDTO(fd => fd.UserId == userId && fd.DrinkId == drinkId);
            if (favoriteDrink != null)
            {
                return new FavoriteDrinkUpdateDTO
                {
                    UserId = favoriteDrink.UserId,
                    DrinkId = favoriteDrink.DrinkId,
                    Notes = favoriteDrink.Notes
                };
            }
            return null;
        }

        public async Task<List<FavoriteDrinkListDTO>> GetAllFavoriteDrinksAsync(string userId)
        {
            var favoriteDrinks = await base.GetByDTO(fd => fd.UserId == userId);
            return favoriteDrinks.Select(fd => new FavoriteDrinkListDTO
            {
                UserId = fd.UserId,
                DrinkId = fd.DrinkId,
                DrinkName = fd.Drink.Name, // Navigasyon özelliği varsa, burada kullanılır
                AddedDate = fd.AddedDate,
                Notes = fd.Notes
            }).ToList();
        }
    }
}
