using Microsoft.AspNetCore.Identity;
using MiniWebShop.Models.Dbo.UserModel;
using MiniWebShop.Services.Interfaces;
using MiniWebShop.Shared.Models.Dto;

namespace MiniWebShop.Services.Implementations
{
    public class IdentitySetup : IIdentitySetup
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;

        public IdentitySetup(IServiceScopeFactory scopeFactory)
        {
            using (var scope = scopeFactory.CreateScope())
            {

                userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                CreateRoleAsync(Roles.Admin).Wait();
                CreateRoleAsync(Roles.Buyer).Wait();
                CreatePlatformAdminAsync().Wait();
            }
        }

        public async Task CreatePlatformAdminAsync()
        {
            string adminEmail = "miniwebshopadmin@gmail.com";

            var find = await userManager.FindByEmailAsync(adminEmail);
            if (find != null)
            {
                return;
            }

            var user = new ApplicationUser();
            user.Email = adminEmail;
            user.UserName = adminEmail;
            user.FirstName = "Admin";
            user.LastName = "Admin";
            string pwd = "1001Admin?";
            user.EmailConfirmed = true;
            var createUser = await userManager.CreateAsync(user, pwd);
            if (createUser.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.Admin);
            }

        }


        public async Task CreateRoleAsync(string role)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole
                {
                    Name = role,
                });
            }
        }

    }
}
