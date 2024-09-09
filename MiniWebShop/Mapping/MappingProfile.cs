using AutoMapper;
using MiniWebShop.Models.Dbo;
using MiniWebShop.Models.Dbo.OrderModels;
using MiniWebShop.Models.Dbo.ProductModels;
using MiniWebShop.Models.Dbo.UserModel;
using MiniWebShop.Shared.Models.Binding;
using MiniWebShop.Shared.Models.Binding.AccountModels;
using MiniWebShop.Shared.Models.Binding.OrderModels;
using MiniWebShop.Shared.Models.Binding.ProductModels;
using MiniWebShop.Shared.Models.ViewModel;
using MiniWebShop.Shared.Models.ViewModel.AccountModels;
using MiniWebShop.Shared.Models.ViewModel.OrderModels;
using MiniWebShop.Shared.Models.ViewModel.ProductModels;

namespace MiniWebShop.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<ProductCategoryViewModel, ProductCategoryUpdateBinding>();
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<ProductCategory, ProductCategoryUpdateBinding>();
            CreateMap<ProductCategoryBinding, ProductCategory>();
            CreateMap<ProductCategoryUpdateBinding, ProductCategory>();


            CreateMap<ProductItem, ProductItemViewModel>();
            CreateMap<ProductItemBinding, ProductItem>();
            CreateMap<ProductItemUpdateBinding, ProductItem>();
            CreateMap<ProductItem, ProductItemUpdateBinding>();

            CreateMap<Address, AddressViewModel>();
            CreateMap<Address, AddressUpdateBinding>();
            CreateMap<Address, AddressBinding>();
            CreateMap<AddressBinding, Address>();
            CreateMap<AddressUpdateBinding, Address>();

            CreateMap<ApplicationUser, ApplicationUserViewModel>();
            CreateMap<ApplicationUser, ApplicationUserUpdateBinding>();
            CreateMap<ApplicationUserUpdateBinding, ApplicationUser>();

            CreateMap<OrderUpdateBinding, Order>();
            CreateMap<OrderBinding, Order>();
            CreateMap<Order, OrderViewModel>();
            CreateMap<OrderItemBinding, OrderItem>();
            CreateMap<OrderItemUpdateBiding, OrderItem>();
            CreateMap<OrderItem, OrderItemViewModel>();


        }
    }
}
