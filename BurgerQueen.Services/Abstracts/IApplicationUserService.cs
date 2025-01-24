using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<ApplicationUserListDTO>> GetAllUsersAsync();
        Task<ApplicationUserListDTO> GetUserByIdAsync(string userId);
        Task<ApplicationUserListDTO> GetUserByEmailAsync(string email);
        Task CreateUserAsync(ApplicationUserAddDTO userDTO);
        Task UpdateUserAsync(ApplicationUserUpdateDTO userDTO);
        Task DeleteUserAsync(string userId);
        Task UpdatePasswordAsync(PasswordUpdateDTO passwordUpdateDTO);

    }
}
