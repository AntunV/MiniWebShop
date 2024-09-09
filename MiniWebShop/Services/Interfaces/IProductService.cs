using MiniWebShop.Shared.Models.Binding.ProductModels;
using MiniWebShop.Shared.Models.ViewModel.ProductModels;

namespace MiniWebShop.Services.Interfaces
{
    public interface IProductService
    {
        Task<ProductCategoryViewModel> AddProductCategory(ProductCategoryBinding model);
        Task<ProductItemViewModel> AddProductItem(ProductItemBinding model);
        Task<ProductCategoryViewModel> DeleteProductCategory(int id);
        Task<ProductItemViewModel> DeleteProductItem(int id);
        Task<ProductCategoryViewModel> GetProductCategory(int id);
        Task<ProductItemViewModel> GetProductItem(int id);
        Task<List<ProductItemViewModel>> GetProductItems(List<int> id);
        Task<List<ProductCategoryViewModel>> GetProductCategories();
        Task<T> GetProductCategory<T>(int id);
        Task<T> GetProductItem<T>(int id);
        Task<ProductCategoryViewModel> UpdateProductCategory(ProductCategoryUpdateBinding model);
        Task<ProductItemViewModel> UpdateProductItem(ProductItemUpdateBinding model);
    }
}