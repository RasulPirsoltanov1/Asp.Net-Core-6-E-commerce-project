using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MultiShop.Business.Mappers;
using MultiShop.Business.Services.Implementations;
using MultiShop.Business.Services.Interfaces;
using MultiShop.Business.Validators.Course;
using MultiShop.Core.Entities;
using MultiShop.DataAccess.Contexts;
using MultiShop.DataAccess.Repositories.Implementations;
using MultiShop.DataAccess.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 7;
    options.User.RequireUniqueEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(15);
}).AddDefaultTokenProviders().AddRoleManager<RoleManager<IdentityRole>>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]);
});
builder.Services.AddAutoMapper(typeof(CategoryMapper).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<CategoryCreateVmValidator>();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
    options.AddPolicy("MemmberPolicy", policy => policy.RequireRole("Memmber"));
    options.AddPolicy("ModeratorPolicy", policy => policy.RequireRole("Moderator"));
    options.AddPolicy("ModeratorPolicy,AdminPolicy", policy => policy.RequireRole("Moderator","Admin"));
});
var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
   );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    );

app.UseStaticFiles();
app.Run();
