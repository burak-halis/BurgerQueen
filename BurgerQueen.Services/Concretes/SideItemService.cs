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
    public class SideItemService : BaseService<SideItem>, ISideItemService
    {
        public SideItemService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddSideItemDTO(SideItemAddDTO dto)
        {
            var sideItem = new SideItem
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Calories = dto.Calories,
                IsGlutenFree = dto.IsGlutenFree,
                IsVegetarian = dto.IsVegetarian,
                ImagePath = dto.ImagePath
            };
            await base.AddDTOAsync(sideItem);
        }

        public async Task UpdateSideItemDTO(SideItemUpdateDTO dto)
        {
            try
            {
                var sideItem = await base.GetByIdDTO(si => si.Id == dto.Id);
                if (sideItem != null)
                {
                    sideItem.Name = dto.Name;
                    sideItem.Description = dto.Description;
                    sideItem.Price = dto.Price;
                    sideItem.Calories = dto.Calories;
                    sideItem.IsGlutenFree = dto.IsGlutenFree;
                    sideItem.IsVegetarian = dto.IsVegetarian;
                    sideItem.ImagePath = dto.ImagePath;

                    await base.UpdateDTOAsync(sideItem);
                }
                else
                {
                    throw new Exception("SideItem bulunamadı.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Güncelleme işleminde bir hata var: " + ex.Message);
            }
        }

        public async Task DeleteSideItemDTO(int id)
        {
            await base.DeleteDTOAsync(id);
        }

        public async Task<SideItemUpdateDTO> GetSideItemById(int id)
        {
            var sideItem = await base.GetByIdDTO(si => si.Id == id);
            if (sideItem != null)
            {
                return new SideItemUpdateDTO
                {
                    Id = sideItem.Id,
                    Name = sideItem.Name,
                    Description = sideItem.Description,
                    Price = sideItem.Price,
                    Calories = sideItem.Calories,
                    IsGlutenFree = sideItem.IsGlutenFree,
                    IsVegetarian = sideItem.IsVegetarian,
                    ImagePath = sideItem.ImagePath
                };
            }
            return null;
        }

        public async Task<List<SideItemListDTO>> GetSideItemsAll()
        {
            var sideItems = await base.GetAllDTO();
            return sideItems.Select(si => new SideItemListDTO
            {
                Id = si.Id,
                Name = si.Name,
                Price = si.Price,
                ImagePath = si.ImagePath
            }).ToList();
        }
    }
}
