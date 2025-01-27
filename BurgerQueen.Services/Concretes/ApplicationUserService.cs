using BurgerQueen.ContextDb.Abstracts;
using BurgerQueen.Entity;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Shared.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerQueen.Services.Concretes
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;

        public ApplicationUserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task UpdatePasswordAsync(PasswordUpdateDTO passwordUpdateDTO)
        {
            var user = await _userManager.FindByIdAsync(passwordUpdateDTO.UserId);
            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }

            var result = await _userManager.ChangePasswordAsync(user, passwordUpdateDTO.CurrentPassword, passwordUpdateDTO.NewPassword);
            if (!result.Succeeded)
            {
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            }
        }

        public async Task<IEnumerable<ApplicationUserListDTO>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            return users.Select(u => new ApplicationUserListDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email,
                FirstName = u.FirstName,
                LastName = u.LastName
            });
        }

        public async Task<ApplicationUserListDTO> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            return user != null ? ToListDTO(user) : null;
        }

        public async Task<ApplicationUserListDTO> GetUserByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null ? ToListDTO(user) : null;
        }

        // Dönüş tipi Task<IdentityResult> olarak güncellendi
        public async Task<IdentityResult> CreateUserAsync(ApplicationUserAddDTO userDTO)
        {
            var user = new ApplicationUser
            {
                UserName = userDTO.UserName,
                Email = userDTO.Email,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName
            };

            return await _userManager.CreateAsync(user, userDTO.Password);
        }

        // UpdateUserAsync metodunun dönüş tipi Task<IdentityResult> olarak değiştirildi
        public async Task<IdentityResult> UpdateUserAsync(ApplicationUserUpdateDTO userDTO)
        {
            var user = await _userManager.FindByIdAsync(userDTO.Id);
            if (user != null)
            {
                user.UserName = userDTO.UserName;
                user.Email = userDTO.Email;
                user.FirstName = userDTO.FirstName;
                user.LastName = userDTO.LastName;

                return await _userManager.UpdateAsync(user);
            }
            else
            {
                throw new Exception("Kullanıcı bulunamadı.");
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var result = await _userManager.DeleteAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
                }
            }
        }

        private ApplicationUserListDTO ToListDTO(ApplicationUser user)
        {
            return new ApplicationUserListDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
        }
    }
}
