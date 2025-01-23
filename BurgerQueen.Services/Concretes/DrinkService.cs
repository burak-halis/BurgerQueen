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
    public class DrinkService : BaseService<Drink>, IDrinkService
    {
        public DrinkService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddDrinkDTO(DrinkAddDTO dto)
        {
            var drink = new Drink
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Calories = dto.Calories,
                Sizes = dto.Sizes,
                ImagePath = dto.ImagePath
            };
            await base.AddDTOAsync(drink);
        }

        public async Task UpdateDrinkDTO(DrinkUpdateDTO dto)
        {
            try
            {
                var drink = await base.GetByIdDTO(d => d.Id == dto.Id);
                if (drink != null)
                {
                    drink.Name = dto.Name;
                    drink.Description = dto.Description;
                    drink.Price = dto.Price;
                    drink.Calories = dto.Calories;
                    drink.Sizes = dto.Sizes;
                    drink.ImagePath = dto.ImagePath;

                    await base.UpdateDTOAsync(drink);
                }
                else
                {
                    throw new Exception("Drink bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güncelleme işleminde bir hata var: " + ex.Message);
            }
        }

        public async Task DeleteDrinkDTO(int id)
        {
            await base.DeleteDTOAsync(id);
        }

        public async Task<DrinkUpdateDTO> GetDrinkById(int id)
        {
            var drink = await base.GetByIdDTO(d => d.Id == id);
            if (drink != null)
            {
                return new DrinkUpdateDTO
                {
                    Id = drink.Id,
                    Name = drink.Name,
                    Description = drink.Description,
                    Price = drink.Price,
                    Calories = drink.Calories,
                    Sizes = drink.Sizes,
                    ImagePath = drink.ImagePath
                };
            }
            return null;
        }

        public async Task<List<DrinkListDTO>> GetDrinksAll()
        {
            var drinks = await base.GetAllDTO();
            return drinks.Select(d => new DrinkListDTO
            {
                Id = d.Id,
                Name = d.Name,
                Price = d.Price,
                ImagePath = d.ImagePath
            }).ToList();
        }
    }
}
