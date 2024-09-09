using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiniWebShop.Data;
using MiniWebShop.Models.Dbo.OrderModels;
using MiniWebShop.Models.Dbo.UserModel;
using MiniWebShop.Services.Interfaces;
using MiniWebShop.Shared.Models.Binding.OrderModels;
using MiniWebShop.Shared.Models.Dto;
using MiniWebShop.Shared.Models.ViewModel.OrderModels;
using System.Security.Claims;

namespace MiniWebShop.Services.Implementations
{
    public class BuyerService : IBuyerService
    {
        private UserManager<ApplicationUser> userManager;
        private ApplicationDbContext db;
        private IMapper mapper;

        public BuyerService(UserManager<ApplicationUser> userManager, ApplicationDbContext db, IMapper mapper)
        {
            this.userManager = userManager;
            this.db = db;
            this.mapper = mapper;
        }


        public async Task<List<OrderViewModel>> GetOrders(ClaimsPrincipal user)
        {
            var applicationUser = await userManager.GetUserAsync(user);
            var role = await userManager.GetRolesAsync(applicationUser);

            switch (role[0])
            {
                case Roles.Admin:
                    return await GetOrders();
                case Roles.Buyer:
                    return await GetOrders(applicationUser);
                default:
                    throw new NotImplementedException($"{role[0]} isn't implemented in get orders!");

            }
        }

        public async Task<List<OrderViewModel>> GetOrders(ApplicationUser buyer)
        {
            var dbos = await db.Orders
                .Include(y => y.Buyer)
                .Include(y => y.OrderItems)
                .Include(y => y.OrderAddress)
                .Where(y => y.BuyerId == buyer.Id)
                .ToListAsync();

            return dbos.Select(y => mapper.Map<OrderViewModel>(y)).ToList();
        }

        public async Task<OrderViewModel> GetOrder(int id)
        {
            var dbo = await db.Orders
                .Include(y => y.Buyer)
                 .Include(y => y.OrderItems)
                .Include(y => y.OrderAddress)
                .FirstOrDefaultAsync(y => y.Id == id);
            return mapper.Map<OrderViewModel>(dbo);
        }

        public async Task<OrderViewModel> GetOrder(int id, ClaimsPrincipal user)
        {
            var applicationUser = await userManager.GetUserAsync(user);
            var role = await userManager.GetRolesAsync(applicationUser);

            switch (role[0])
            {
                case Roles.Admin:
                    return await GetOrder(id);
                case Roles.Buyer:
                    return await GetOrder(id, applicationUser);
                default:
                    throw new NotImplementedException($"{role[0]} isn't implemented in get orders!");

            }

        }

        public async Task<OrderViewModel> GetOrder(int id, ApplicationUser buyer)
        {
            var dbo = await db.Orders
                .Include(y => y.Buyer)
                .Include(y => y.OrderItems)
                .Include(y => y.OrderAddress)
                .FirstOrDefaultAsync(y => y.Id == id && y.BuyerId == buyer.Id);

            return mapper.Map<OrderViewModel>(dbo);
        }

        public async Task<List<OrderViewModel>> GetOrders()
        {
            var dbos = await db.Orders
                .Include(y => y.Buyer)
                .Include(y => y.OrderItems)
                .Include(y => y.OrderAddress)
                .ToListAsync();

            return dbos.Select(y => mapper.Map<OrderViewModel>(y)).ToList();
        }

        public async Task<OrderViewModel> AddOrder(OrderBinding model, ClaimsPrincipal user)
        {
            var applicationUser = await userManager.GetUserAsync(user);
            return await AddOrder(model, applicationUser);
        }

        public async Task<OrderViewModel> AddOrder(OrderBinding model, ApplicationUser buyer)
        {
            var dbo = mapper.Map<Order>(model);
            var productItems = db.ProductItems
                .Where(y => model.OrderItems.Select(y => y.ProductItemId).Contains(y.Id)).ToList();

            foreach (var product in dbo.OrderItems)
            {
                var target = productItems.FirstOrDefault(y => product.ProductItemId == y.Id);
                if (target != null)
                {
                    target.Quantity -= product.Quantity;
                    product.Price = target.Price;
                }
            }


            dbo.Buyer = buyer;
            dbo.CalcualteTotal();

            db.Orders.Add(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<OrderViewModel>(dbo);
        }

        public async Task<OrderViewModel> UpdateOrder(OrderUpdateBinding model)
        {
            var dbo = await db.Orders
                .Include(y => y.OrderItems)
                .Include(y => y.OrderAddress)
                .FirstOrDefaultAsync(y => y.Id == model.Id);
            mapper.Map(model, dbo);
            await db.SaveChangesAsync();

            return mapper.Map<OrderViewModel>(dbo);
        }
        public async Task<OrderViewModel> CancelOrder(int id)
        {
            var dbo = await db.Orders
                .Include(y => y.OrderItems)
                .ThenInclude(y => y.ProductItem)
                .FirstOrDefaultAsync(y => y.Id == id);

            var productItems = db.ProductItems
                .Where(y => dbo.OrderItems.Select(y => y.ProductItemId).Contains(y.Id)).ToList();
           
            db.Orders.Remove(dbo);
         
            foreach (var product in dbo.OrderItems)
            {
                var target = productItems.FirstOrDefault(y => product.ProductItemId == y.Id);
                if (target != null)
                {
                    target.Quantity += product.Quantity;
                }
            }

            await db.SaveChangesAsync();
            return mapper.Map<OrderViewModel>(dbo);
        }


    }
}
