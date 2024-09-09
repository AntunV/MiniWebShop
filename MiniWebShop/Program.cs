using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniWebShop.Data;
using MiniWebShop.Mapping;
using MiniWebShop.Models.Dbo.UserModel;
using MiniWebShop.Services.Implementations;
using MiniWebShop.Services.Interfaces;
using MiniWebShop.Shared.Models.Dto;

namespace MiniWebShop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.Configure<AppSettings>(builder.Configuration);
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddScoped<IBuyerService, BuyerService>();  
            builder.Services.AddSingleton<IIdentitySetup, IdentitySetup>();
            builder.Services.AddScoped<IProductService, ProductService>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<ICommonService, CommonService>();


            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddDefaultIdentity<ApplicationUser>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;


                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireDigit = false;
                }


                )
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add session support
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            // Add session middleware
            app.UseSession();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();
            var identitySetup = app.Services.GetRequiredService<IIdentitySetup>();


            app.Run();
        }
    }
}
