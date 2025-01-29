using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace BurgerQueen.Services.Abstracts
{
    public interface IRoleService
    {
        Task CreateRoles();
    }
}