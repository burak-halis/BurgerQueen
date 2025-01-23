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
    public class FavoriteSideItemService : BaseService<FavoriteSideItem>, IFavoriteSideItemService
    {
        public FavoriteSideItemService(IUnitOfWork unitOfWork) : base(unitOfWork) { }

        public async Task AddFavoriteSideItemAsync(FavoriteSideItemAddDTO dto)
        {
            var favoriteSideItem = new FavoriteSideItem
            {
                UserId = dto.UserId,
                SideItemId = dto.SideItemId,
                AddedDate = DateTime.UtcNow
            };
            await base.AddDTOAsync(favoriteSideItem);
        }

        public async Task UpdateFavoriteSideItemAsync(FavoriteSideItemUpdateDTO dto)
        {
            var favoriteSideItem = await base.GetByIdDTO(fsi => fsi.UserId == dto.UserId && fsi.SideItemId == dto.SideItemId);
            if (favoriteSideItem != null)
            {
                favoriteSideItem.Notes = dto.Notes;
                await base.UpdateDTOAsync(favoriteSideItem);
            }
            else
            {
                throw new Exception("Favori yan ürün bulunamadı.");
            }
        }

        public async Task DeleteFavoriteSideItemAsync(int sideItemId, string userId)
        {
            var favoriteSideItem = await base.GetByIdDTO(fsi => fsi.UserId == userId && fsi.SideItemId == sideItemId);
            if (favoriteSideItem != null)
            {
                await base.DeleteDTOAsync(favoriteSideItem.Id);
            }
            else
            {
                throw new Exception("Favori yan ürün bulunamadı.");
            }
        }

        public async Task<FavoriteSideItemUpdateDTO> GetFavoriteSideItemByIdAsync(int sideItemId, string userId)
        {
            var favoriteSideItem = await base.GetByIdDTO(fsi => fsi.UserId == userId && fsi.SideItemId == sideItemId);
            if (favoriteSideItem != null)
            {
                return new FavoriteSideItemUpdateDTO
                {
                    UserId = favoriteSideItem.UserId,
                    SideItemId = favoriteSideItem.SideItemId,
                    Notes = favoriteSideItem.Notes
                };
            }
            return null;
        }

        public async Task<List<FavoriteSideItemListDTO>> GetAllFavoriteSideItemsAsync(string userId)
        {
            var favoriteSideItems = await base.GetByDTO(fsi => fsi.UserId == userId);
            return favoriteSideItems.Select(fsi => new FavoriteSideItemListDTO
            {
                UserId = fsi.UserId,
                SideItemId = fsi.SideItemId,
                SideItemName = fsi.SideItem.Name, // Navigasyon özelliği varsa, burada kullanılır
                AddedDate = fsi.AddedDate,
                Notes = fsi.Notes
            }).ToList();
        }
    }
}
