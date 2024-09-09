using System.Security.Claims;

namespace MiniWebShop.Services.Interfaces
{
    public interface ICommonService
    {
        Task AddSessionItem(string key, object value, ClaimsPrincipal user);
        Task DeactivateAllExpiredSessions();
        Task<T> GetSessionItem<T>(string key, ClaimsPrincipal user);
        Task RemoveFromSession(string key, ClaimsPrincipal user);
    }
}