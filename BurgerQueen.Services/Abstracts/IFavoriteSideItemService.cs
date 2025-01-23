using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IFavoriteSideItemService : IBaseService<FavoriteSideItem>
    {
        Task AddFavoriteSideItemAsync(FavoriteSideItemAddDTO dto);
        Task UpdateFavoriteSideItemAsync(FavoriteSideItemUpdateDTO dto);
        Task DeleteFavoriteSideItemAsync(int sideItemId, string userId);
        Task<FavoriteSideItemUpdateDTO> GetFavoriteSideItemByIdAsync(int sideItemId, string userId);
        Task<List<FavoriteSideItemListDTO>> GetAllFavoriteSideItemsAsync(string userId);
    }
}
