using MiniWebShop.Services.Interfaces;
using MiniWebShop.Shared.Models.Binding.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.UnitTest
{
    public class BuyerServiceUnitTest:MiniWebShopSetup
    {
        private readonly IBuyerService buyerService;

        public BuyerServiceUnitTest()
        {
            this.buyerService = GetBuyerService();
        }
        [Fact]
        public async void GetOrder_FetchesOrderFromDb_ValidatesIfItemIsNotNull()
        {
            var result = await buyerService.GetOrder(Orders[0].Id);
            Assert.NotNull(result);
        }
        [Fact]
        public async void GetOrders_FetchesOrdersFromDb_ValidatesIfItemIsNotEmpty()
        {
            var result = await buyerService.GetOrders(ApplicationUser);
            Assert.Single(result);
        }
        [Fact]
        public async void AddOrder_AddsOrderToDb_ValidatesIfResponseIsNotNull()
        {
            var result = await buyerService.AddOrder(new Shared.Models.Binding.OrderModels.OrderBinding
            {
                Message = "Test",
                OrderAddress = new Shared.Models.Binding.AddressBinding
                {
                    City = "Test",
                    Country = "Test",
                    Street = "Test",
                    Number = "Test",
                },
                OrderItems = new List<OrderItemBinding>
                    {
                        new OrderItemBinding
                        {
                            ProductItemId = ProductCategories[0].ProductItems.First().Id,
                            Quantity = 10
                        }
                    }

            }, ApplicationUser);
            Assert.NotNull(result);
        }
 
        [Fact]
        public async void UpdateOrder_UpdatesOrderToDb_ValidatesIfResponseIsNotEqualToPreviusRecord()
        {
            var order = await buyerService.AddOrder(new Shared.Models.Binding.OrderModels.OrderBinding
            {
                Message = "Test",
                OrderAddress = new Shared.Models.Binding.AddressBinding
                {
                    City = "Test",
                    Country = "Test",
                    Street = "Test",
                    Number = "Test",
                },
                OrderItems = new List<OrderItemBinding>
                    {
                        new OrderItemBinding
                        {
                            ProductItemId = ProductCategories[0].ProductItems.First().Id,
                            Quantity = 10
                        }
                    }

            }, ApplicationUser);

            var result = await buyerService.UpdateOrder
                (
                    new OrderUpdateBinding
                    {
                        Id = order.Id,
                        Message = "Test2",
                        OrderAddress = new Shared.Models.Binding.AddressUpdateBinding
                        {
                            City = "Test2",
                            Country = "Test2",
                            Street = "Test2",
                            Number = "Test2",
                            Id = order.OrderAddress.Id
                        }
                    }

                );

            Assert.NotEqual(order.Message, result.Message);
            Assert.NotEqual(order.OrderAddress.Country, result.OrderAddress.Country);
        }
        [Fact]
        public async void CancelOrder_RemovesOrderFromDb_ValidatesIfResponseIsNotNull()
        {
            var order = await buyerService.AddOrder(new Shared.Models.Binding.OrderModels.OrderBinding
            {
                Message = "Test",
                OrderAddress = new Shared.Models.Binding.AddressBinding
                {
                    City = "Test",
                    Country = "Test",
                    Street = "Test",
                    Number = "Test",
                },
                OrderItems = new List<OrderItemBinding>
                    {
                        new OrderItemBinding
                        {
                            ProductItemId = ProductCategories[0].ProductItems.First().Id,
                            Quantity = 10
                        }
                    }

            }, ApplicationUser);
            var previousOrders = await buyerService.GetOrders(ApplicationUser);
            int previousOrdersCount = previousOrders.Count;

            await buyerService.CancelOrder(order.Id);

            previousOrders = await buyerService.GetOrders(ApplicationUser);
            int newOrdersCount = previousOrders.Count;

            Assert.Equal(previousOrdersCount - 1, newOrdersCount);

        }

     
    }
}
