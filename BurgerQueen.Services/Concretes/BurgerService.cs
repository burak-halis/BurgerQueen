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
    public class BurgerService : BaseService<Burger>, IBurgerService
    {
        public BurgerService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddBurgerDTO(BurgerAddDTO dto)
        {
            var burger = new Burger
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Calories = dto.Calories,
                IsVegetarian = dto.IsVegetarian,
                IsGlutenFree = dto.IsGlutenFree,
                Ingredients = dto.Ingredients,
                ImagePath = dto.ImagePath
            };
            await base.AddDTOAsync(burger);
        }

        public async Task UpdateBurgerDTO(BurgerUpdateDTO dto)
        {
            try
            {
                var burger = await base.GetByIdDTO(b => b.Id == dto.Id);
                if (burger != null)
                {
                    burger.Name = dto.Name;
                    burger.Description = dto.Description;
                    burger.Price = dto.Price;
                    burger.Calories = dto.Calories;
                    burger.IsVegetarian = dto.IsVegetarian;
                    burger.IsGlutenFree = dto.IsGlutenFree;
                    burger.Ingredients = dto.Ingredients;
                    burger.ImagePath = dto.ImagePath;

                    await base.UpdateDTOAsync(burger);
                }
                else
                {
                    throw new Exception("Burger bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güncelleme işleminde bir hata var: " + ex.Message);
            }
        }

        public async Task DeleteBurgerDTO(int id)
        {
            await base.DeleteDTOAsync(id);
        }

        public async Task<BurgerUpdateDTO> GetBurgerById(int id)
        {
            var burger = await base.GetByIdDTO(b => b.Id == id);
            if (burger != null)
            {
                return new BurgerUpdateDTO
                {
                    Id = burger.Id,
                    Name = burger.Name,
                    Description = burger.Description,
                    Price = burger.Price,
                    Calories = burger.Calories,
                    IsVegetarian = burger.IsVegetarian,
                    IsGlutenFree = burger.IsGlutenFree,
                    Ingredients = burger.Ingredients,
                    ImagePath = burger.ImagePath
                };
            }
            return null;
        }

        public async Task<List<BurgerListDTO>> GetBurgersAll()
        {
            var burgers = await base.GetAllDTO();
            return burgers.Select(b => new BurgerListDTO
            {
                Id = b.Id,
                Name = b.Name,
                Price = b.Price,
                ImagePath = b.ImagePath
            }).ToList();
        }
    }
}
