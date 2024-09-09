using MiniWebShop.Shared.Models.Binding.AccountModels;
using MiniWebShop.Shared.Models.ViewModel.AccountModels;
using System.Security.Claims;

namespace MiniWebShop.Services.Interfaces
{
    public interface IAccountService
    {
        Task<ApplicationUserViewModel?> CreateUser(RegistrationBinding model, string role);
        Task<T> GetUserAddress<T>(ClaimsPrincipal user);
        Task<ApplicationUserViewModel?> GetUserProfile(ClaimsPrincipal user);
        Task<T> GetUserProfile<T>(ClaimsPrincipal user);
        Task<ApplicationUserViewModel> UpdateUserProfile(ApplicationUserUpdateBinding model);
    }
}