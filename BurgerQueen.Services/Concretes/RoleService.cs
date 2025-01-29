using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using BurgerQueen.Services.Abstracts;

namespace BurgerQueen.Services.Concretes
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateRoles()
        {
            // Burada oluşturmayı istediğiniz tüm roller eklenir
            string[] roleNames = { "User", "Admin" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await _roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}