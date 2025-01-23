using BurgerQueen.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BurgerQueen.Services.Abstracts;
using BurgerQueen.Services.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using BurgerQueen.ContextDb.Concretes;
using BurgerQueen.ContextDb.Abstracts;

namespace BurgerQueen.Services
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddIoCContainer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<BaseContext>()
            .AddDefaultTokenProviders();

            services.AddScoped<IEFContext, BaseContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Servisler
            services.AddScoped<IBurgerService, BurgerService>();
            services.AddScoped<IDrinkService, DrinkService>();
            services.AddScoped<IFryService, FryService>();
            services.AddScoped<ISideItemService, SideItemService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<ISauceService, SauceService>();
            services.AddScoped<IExtraIngredientService, ExtraIngredientService>();
            services.AddScoped<IBurgerExtraService, BurgerExtraService>();
            services.AddScoped<IMenuBurgerService, MenuBurgerService>();
            services.AddScoped<IMenuDrinkService, MenuDrinkService>();
            services.AddScoped<IMenuFryService, MenuFryService>();
            services.AddScoped<IMenuSideItemService, MenuSideItemService>();
            services.AddScoped<IOrderStatusHistoryService, OrderStatusHistoryService>();
            services.AddScoped<IApplicationUserService, ApplicationUserService>();

            return services;
        }
    }
}
