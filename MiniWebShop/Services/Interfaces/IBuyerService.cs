using MiniWebShop.Models.Dbo.UserModel;
using MiniWebShop.Shared.Models.Binding.OrderModels;
using MiniWebShop.Shared.Models.ViewModel.OrderModels;
using System.Security.Claims;

namespace MiniWebShop.Services.Interfaces
{
    public interface IBuyerService
    {
        Task<OrderViewModel> AddOrder(OrderBinding model, ApplicationUser buyer);
        Task<OrderViewModel> AddOrder(OrderBinding model, ClaimsPrincipal user);
        Task<OrderViewModel> CancelOrder(int id);
        Task<OrderViewModel> GetOrder(int id);
        Task<OrderViewModel> GetOrder(int id, ApplicationUser buyer);
        Task<OrderViewModel> GetOrder(int id, ClaimsPrincipal user);
        Task<List<OrderViewModel>> GetOrders();
        Task<List<OrderViewModel>> GetOrders(ApplicationUser buyer);
        Task<List<OrderViewModel>> GetOrders(ClaimsPrincipal user);
        Task<OrderViewModel> UpdateOrder(OrderUpdateBinding model);
    }
}