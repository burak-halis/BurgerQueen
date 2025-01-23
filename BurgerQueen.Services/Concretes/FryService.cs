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
    public class FryService : BaseService<Fry>, IFryService
    {
        public FryService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddFryDTO(FryAddDTO dto)
        {
            var fry = new Fry
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Calories = dto.Calories,
                Sizes = dto.Sizes,
                IsSpicy = dto.IsSpicy,
                ImagePath = dto.ImagePath,
                Type = dto.Type
            };
            await base.AddDTOAsync(fry);
        }

        public async Task UpdateFryDTO(FryUpdateDTO dto)
        {
            try
            {
                var fry = await base.GetByIdDTO(f => f.Id == dto.Id);
                if (fry != null)
                {
                    fry.Name = dto.Name;
                    fry.Description = dto.Description;
                    fry.Price = dto.Price;
                    fry.Calories = dto.Calories;
                    fry.Sizes = dto.Sizes;
                    fry.IsSpicy = dto.IsSpicy;
                    fry.ImagePath = dto.ImagePath;
                    fry.Type = dto.Type;

                    await base.UpdateDTOAsync(fry);
                }
                else
                {
                    throw new Exception("Fry bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güncelleme işleminde bir hata var: " + ex.Message);
            }
        }

        public async Task DeleteFryDTO(int id)
        {
            await base.DeleteDTOAsync(id);
        }

        public async Task<FryUpdateDTO> GetFryById(int id)
        {
            var fry = await base.GetByIdDTO(f => f.Id == id);
            if (fry != null)
            {
                return new FryUpdateDTO
                {
                    Id = fry.Id,
                    Name = fry.Name,
                    Description = fry.Description,
                    Price = fry.Price,
                    Calories = fry.Calories,
                    Sizes = fry.Sizes,
                    IsSpicy = fry.IsSpicy,
                    ImagePath = fry.ImagePath,
                    Type = fry.Type
                };
            }
            return null;
        }

        public async Task<List<FryListDTO>> GetFriesAll()
        {
            var fries = await base.GetAllDTO();
            return fries.Select(f => new FryListDTO
            {
                Id = f.Id,
                Name = f.Name,
                Price = f.Price,
                ImagePath = f.ImagePath
            }).ToList();
        }
    }
}
