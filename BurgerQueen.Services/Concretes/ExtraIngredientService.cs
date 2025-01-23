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
    public class ExtraIngredientService : BaseService<ExtraIngredient>, IExtraIngredientService
    {
        public ExtraIngredientService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddExtraIngredientDTO(ExtraIngredientAddDTO dto)
        {
            var extraIngredient = new ExtraIngredient
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Category = dto.Category,
                IsGlutenFree = dto.IsGlutenFree,
                IsVegan = dto.IsVegan,
                ImagePath = dto.ImagePath,
                UnitOfMeasure = dto.UnitOfMeasure
            };
            await base.AddDTOAsync(extraIngredient);
        }

        public async Task UpdateExtraIngredientDTO(ExtraIngredientUpdateDTO dto)
        {
            try
            {
                var extraIngredient = await base.GetByIdDTO(ei => ei.Id == dto.Id);
                if (extraIngredient != null)
                {
                    extraIngredient.Name = dto.Name;
                    extraIngredient.Description = dto.Description;
                    extraIngredient.Price = dto.Price;
                    extraIngredient.Category = dto.Category;
                    extraIngredient.IsGlutenFree = dto.IsGlutenFree;
                    extraIngredient.IsVegan = dto.IsVegan;
                    extraIngredient.ImagePath = dto.ImagePath;
                    extraIngredient.UnitOfMeasure = dto.UnitOfMeasure;

                    await base.UpdateDTOAsync(extraIngredient);
                }
                else
                {
                    throw new Exception("ExtraIngredient bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güncelleme işleminde bir hata var: " + ex.Message);
            }
        }

        public async Task DeleteExtraIngredientDTO(int id)
        {
            await base.DeleteDTOAsync(id);
        }

        public async Task<ExtraIngredientUpdateDTO> GetExtraIngredientById(int id)
        {
            var extraIngredient = await base.GetByIdDTO(ei => ei.Id == id);
            if (extraIngredient != null)
            {
                return new ExtraIngredientUpdateDTO
                {
                    Id = extraIngredient.Id,
                    Name = extraIngredient.Name,
                    Description = extraIngredient.Description,
                    Price = extraIngredient.Price,
                    Category = extraIngredient.Category,
                    IsGlutenFree = extraIngredient.IsGlutenFree,
                    IsVegan = extraIngredient.IsVegan,
                    ImagePath = extraIngredient.ImagePath,
                    UnitOfMeasure = extraIngredient.UnitOfMeasure
                };
            }
            return null;
        }

        public async Task<List<ExtraIngredientListDTO>> GetExtraIngredientsAll()
        {
            var extraIngredients = await base.GetAllDTO();
            return extraIngredients.Select(ei => new ExtraIngredientListDTO
            {
                Id = ei.Id,
                Name = ei.Name,
                Price = ei.Price,
                Category = ei.Category,
                ImagePath = ei.ImagePath
            }).ToList();
        }
    }
}
