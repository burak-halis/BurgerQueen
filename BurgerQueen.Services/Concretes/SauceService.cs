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
    public class SauceService : BaseService<Sauce>, ISauceService
    {
        public SauceService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddSauceDTO(SauceAddDTO dto)
        {
            var sauce = new Sauce
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                ImagePath = dto.ImagePath
            };
            await base.AddDTOAsync(sauce);
        }

        public async Task UpdateSauceDTO(SauceUpdateDTO dto)
        {
            try
            {
                var sauce = await base.GetByIdDTO(s => s.Id == dto.Id);
                if (sauce != null)
                {
                    sauce.Name = dto.Name;
                    sauce.Description = dto.Description;
                    sauce.Price = dto.Price;
                    sauce.ImagePath = dto.ImagePath;

                    await base.UpdateDTOAsync(sauce);
                }
                else
                {
                    throw new Exception("Sauce bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güncelleme işleminde bir hata var: " + ex.Message);
            }
        }

        public async Task DeleteSauceDTO(int id)
        {
            await base.DeleteDTOAsync(id);
        }

        public async Task<SauceUpdateDTO> GetSauceById(int id)
        {
            var sauce = await base.GetByIdDTO(s => s.Id == id);
            if (sauce != null)
            {
                return new SauceUpdateDTO
                {
                    Id = sauce.Id,
                    Name = sauce.Name,
                    Description = sauce.Description,
                    Price = sauce.Price,
                    ImagePath = sauce.ImagePath
                };
            }
            return null;
        }

        public async Task<List<SauceListDTO>> GetSaucesAll()
        {
            var sauces = await base.GetAllDTO();
            return sauces.Select(s => new SauceListDTO
            {
                Id = s.Id,
                Name = s.Name,
                Price = s.Price,
                ImagePath = s.ImagePath
            }).ToList();
        }
    }
}
