using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
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
        Task<IdentityResult> CreateUserAsync(ApplicationUserAddDTO userDTO);
        Task<IdentityResult> UpdateUserAsync(ApplicationUserUpdateDTO userDTO); // Burası değişti
        Task DeleteUserAsync(string userId);
        Task UpdatePasswordAsync(PasswordUpdateDTO passwordUpdateDTO);
    }
}
