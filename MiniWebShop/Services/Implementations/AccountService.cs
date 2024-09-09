using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniWebShop.Data;
using MiniWebShop.Models.Dbo.UserModel;
using MiniWebShop.Services.Interfaces;
using MiniWebShop.Shared.Models.Binding.AccountModels;
using MiniWebShop.Shared.Models.ViewModel.AccountModels;
using System.Security.Claims;

namespace MiniWebShop.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext db;
        private IMapper mapper;
        private SignInManager<ApplicationUser> signInManager;

        public AccountService(UserManager<ApplicationUser> userManager,
            ApplicationDbContext db,
            IMapper mapper, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.db = db;
            this.mapper = mapper;
            this.signInManager = signInManager;
        }

        public async Task<ApplicationUserViewModel?> CreateUser(RegistrationBinding model, string role)
        {
            var find = await userManager.FindByNameAsync(model.Email);
            if (find != null)
            {
                return null;
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                RegistrationDate = DateTime.Now
            };

            user.EmailConfirmed = true;
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, role);
                await userManager.UpdateAsync(user);
                await signInManager.SignInAsync(user, false);
                return mapper.Map<ApplicationUserViewModel>(user);
            }

            return null;
        }

        public async Task<T> GetUserAddress<T>(ClaimsPrincipal user)
        {
            var applicationUser = await userManager.GetUserAsync(user);
            var dboUser = db.Users
                .Include(x => x.Address)
                .FirstOrDefault(x => x.Id == applicationUser.Id);

            return mapper.Map<T>(dboUser.Address);

        }


        public async Task<ApplicationUserViewModel?> GetUserProfile(ClaimsPrincipal user)
        {
            var dbo = await db.Users.Include(y => y.Address)
                .FirstOrDefaultAsync(x => x.Id == userManager.GetUserId(user));
            return mapper.Map<ApplicationUserViewModel>(dbo);
        }

        public async Task<T> GetUserProfile<T>(ClaimsPrincipal user)
        {
            var dbo = await db.Users.Include(y => y.Address)
                .FirstOrDefaultAsync(x => x.Id == userManager.GetUserId(user));
            return mapper.Map<T>(dbo);
        }

        public async Task<ApplicationUserViewModel> UpdateUserProfile(ApplicationUserUpdateBinding model)
        {
            var dbo = await db.Users.Include(y => y.Address)
                .FirstOrDefaultAsync(x => x.Id == model.Id);
            mapper.Map(model, dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ApplicationUserViewModel>(dbo);

        }



    }
}
