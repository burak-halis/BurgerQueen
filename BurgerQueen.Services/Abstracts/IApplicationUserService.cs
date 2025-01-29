using BurgerQueen.Entity;
using BurgerQueen.Shared.DTOs;
using BurgerQueen.Shared.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Abstracts
{
    public interface IApplicationUserService
    {
        Task<IEnumerable<ApplicationUserListDTO>> GetAllUsersAsync();
        Task<ApplicationUserListDTO> GetUserByIdAsync(string userId);
        Task<ApplicationUserListDTO> GetUserByEmailAsync(string email);
        Task<IdentityResult> CreateUserAsync(ApplicationUserAddDTO userDTO);
        Task<IdentityResult> UpdateUserAsync(ApplicationUserUpdateDTO userDTO);
        Task DeleteUserAsync(string userId);
        Task UpdatePasswordAsync(PasswordUpdateDTO passwordUpdateDTO);
        Task UpdateUserGender(string userId, Gender gender);
        Task<Gender?> GetUserGender(string userId);
    }
}