using BurgerQueen.ContextDb.Concretes;
using BurgerQueen.ContextDb.Abstracts;
using BurgerQueen.Services.Concretes;
using BurgerQueen.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BurgerQueen.Entity;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuration for database context
builder.Services.AddDbContext<BaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Identity configuration
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BaseContext>();

// Registering services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IApplicationUserService, ApplicationUserService>();
builder.Services.AddScoped<IBurgerExtraService, BurgerExtraService>();
builder.Services.AddScoped<IBurgerService, BurgerService>();
builder.Services.AddScoped<IDrinkService, DrinkService>();
builder.Services.AddScoped<IExtraIngredientService, ExtraIngredientService>();
builder.Services.AddScoped<IFryService, FryService>();
builder.Services.AddScoped<IMenuBurgerService, MenuBurgerService>();
builder.Services.AddScoped<IMenuDrinkService, MenuDrinkService>();
builder.Services.AddScoped<IMenuFryService, MenuFryService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IMenuSideItemService, MenuSideItemService>();
builder.Services.AddScoped<IOrderItemService, OrderItemService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderStatusHistoryService, OrderStatusHistoryService>();
builder.Services.AddScoped<ISauceService, SauceService>();
builder.Services.AddScoped<ISideItemService, SideItemService>();
builder.Services.AddScoped<IEFContext, BaseContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
