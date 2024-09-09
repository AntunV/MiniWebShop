namespace MiniWebShop.Services.Interfaces
{
    public interface IIdentitySetup
    {
        Task CreatePlatformAdminAsync();
        Task CreateRoleAsync(string role);
    }
}