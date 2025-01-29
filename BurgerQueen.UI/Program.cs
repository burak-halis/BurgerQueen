using BurgerQueen.ContextDb.Concretes;
using BurgerQueen.ContextDb.Abstracts;
using BurgerQueen.Services.Concretes;
using BurgerQueen.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BurgerQueen.Entity;
using BurgerQueen.UI.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configuration for database context
builder.Services.AddDbContext<BaseContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity configuration
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // E-posta onayý gerekli deðil
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredLength = 8;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<BaseContext>()
.AddDefaultTokenProviders();

builder.Services.Configure<SecurityStampValidatorOptions>(options =>
    options.ValidationInterval = TimeSpan.FromMinutes(30));

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
});

// E-posta doðrulama ve 2FA ile ilgili konfigürasyonlarý kaldýrdýk
//builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
//    options.TokenLifespan = TimeSpan.FromHours(3));

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
builder.Services.AddScoped<IRoleService, RoleService>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var roleService = services.GetRequiredService<IRoleService>();
        await roleService.CreateRoles();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Rollerin oluþturulmasý sýrasýnda bir hata oluþtu.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
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