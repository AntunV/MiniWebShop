using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MiniWebShop.Data;
using MiniWebShop.Models.Dbo.OrderModels;
using MiniWebShop.Models.Dbo.ProductModels;
using MiniWebShop.Models.Dbo.UserModel;
using MiniWebShop.Models.Dbo;
using MiniWebShop.Services.Implementations;
using MiniWebShop.Services.Interfaces;
using MiniWebShop.Shared.Models.Dto;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MiniWebShop.Mapping;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace MiniWebShop.UnitTest
{
   
        public abstract class MiniWebShopSetup
        {
            protected IMapper Mapper { get; private set; }
            protected ApplicationDbContext InMemoryDbContext;
            protected static string TestString = "Test";
            protected readonly List<ProductCategory> ProductCategories;
            protected readonly List<Order> Orders;
            protected readonly ApplicationUser ApplicationUser;
            protected readonly Mock<UserManager<ApplicationUser>> UserManager;


            public MiniWebShopSetup()
            {
                SetupInMemoryContext();

                var userStoreMock = Mock.Of<IUserStore<ApplicationUser>>();
                UserManager = new Mock<UserManager<ApplicationUser>>(userStoreMock, null, null, null, null, null, null, null, null);
                ApplicationUser = GetApplicationUser();
                var mappingConfig = new MapperConfiguration(mc =>
                {
                    mc.AddProfile(new MappingProfile());
                });
                Mapper = mappingConfig.CreateMapper();
                ProductCategories = GenerateProductCategorys(100);
                Orders = GetOrders(1);

            }


            protected ApplicationUser GetApplicationUser()
            {
                var applicationUser = new ApplicationUser
                {
                    UserName = "test",
                    Email = "test@test.hr",
                    Address = new Address
                    {
                        City = "Zagreb",
                        Country = "Croatia",
                        Street = "Kunovecka",
                        Number = "10a"
                    },
                    LastName = "test",
                    FirstName = "test",
                    PhoneNumber = "123456789",
                    EmailConfirmed = true

                };

                InMemoryDbContext.Users.Add(applicationUser);
                InMemoryDbContext.SaveChanges();
                return applicationUser;
            }

            protected List<Order> GetOrders(int number)
            {
                List<Order> orders = new List<Order>();

                for (int i = 0; i < number; i++)
                {
                    var order = new Order
                    {
                        Buyer = ApplicationUser,
                        BuyerId = ApplicationUser.Id,
                        OrderAddress = new Address
                        {
                            City = "Zagreb",
                            Country = "Croatia",
                            Street = "Kunovecka",
                            Number = "10a"
                        },
                        Message = TestString,
                        OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            ProductItem = ProductCategories[0].ProductItems.First(),
                            Quantity = 10,
                            Price = 100,

                        }
                    }

                    };

                    InMemoryDbContext.Orders.Add(order);
                    InMemoryDbContext.SaveChanges();
                    orders.Add(order);
                }


                return orders;
            }




            protected List<ProductCategory> GenerateProductCategorys(int number)
            {

                List<ProductCategory> response = new List<ProductCategory>();
                Random random = new Random();

                for (int i = 0; i < number; i++)
                {

                    if (i != 0)
                    {
                        ProductCategory listItem = new ProductCategory
                        {
                            Description = $"{nameof(ProductCategory.Description)} {random.Next(1, 1000)}",
                            CategoryName = $"{nameof(ProductCategory.CategoryName)} {random.Next(1, 1000)}",
                        };
                        response.Add(listItem);
                    }
                    else
                    {
                        ProductCategory listItem = new ProductCategory
                        {
                            Description = $"{nameof(ProductCategory.Description)} {random.Next(1, 1000)}",
                            CategoryName = $"{TestString} {random.Next(1, 1000)}",
                            ProductItems = new List<ProductItem>()
                        {
                            new ProductItem
                            {
                                Description = TestString,
                                Quantity  = 10,
                                Price = 20,
                                Name = TestString
                            },
                            new ProductItem
                            {
                                Description = TestString,
                                Quantity  = 15,
                                Price = 200,
                                Name = TestString
                            }
                        }
                        };

                        response.Add(listItem);
                    }


                }

                InMemoryDbContext.ProductCategories.AddRange(response);
                InMemoryDbContext.SaveChanges();

                return response;

            }
            protected IProductService GetProductService(ApplicationDbContext? db = null)
            {
                if (db != null)
                {
                    return new ProductService(db, Mapper);
                }
                return new ProductService(InMemoryDbContext, Mapper);

            }


            protected IBuyerService GetBuyerService(ApplicationDbContext? db = null)
            {
                if (db != null)
                {
                    return new BuyerService(UserManager.Object, db, Mapper);
                }
                return new BuyerService(UserManager.Object, InMemoryDbContext, Mapper);

            }

            private void SetupInMemoryContext()
            {
                var inMemoryOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                                .Options;
                InMemoryDbContext = new ApplicationDbContext(inMemoryOptions);
            }

        
   
        
    
        }
    
}


