using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MiniWebShop.Data;
using MiniWebShop.Models.Dbo.ProductModels;
using MiniWebShop.Services.Interfaces;
using MiniWebShop.Shared.Models.Binding.ProductModels;
using MiniWebShop.Shared.Models.ViewModel.ProductModels;

namespace MiniWebShop.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext db;
        private readonly IMapper mapper;

        public ProductService(ApplicationDbContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }

        public async Task<ProductItemViewModel> AddProductItem(ProductItemBinding model)
        {
            var dbo = mapper.Map<ProductItem>(model);
            db.ProductItems.Add(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductItemViewModel>(dbo);
        }

        public async Task<ProductItemViewModel> GetProductItem(int id)
        {
            var dbo = await db.ProductItems
                .Include(x => x.ProductCategory)
                .Include(x => x.Quantity)
                .FirstOrDefaultAsync(y => y.Id == id);
            return mapper.Map<ProductItemViewModel>(dbo);

        }

        public async Task<ProductItemViewModel> DeleteProductItem(int id)
        {
            var dbo = await db.ProductItems
            .Include(y => y.ProductCategory)
            .FirstOrDefaultAsync(y => y.Id == id);
            db.ProductItems.Remove(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductItemViewModel>(dbo);

        }

        public async Task<ProductItemViewModel> UpdateProductItem(ProductItemUpdateBinding model)
        {
            var dbo = await db.ProductItems.FindAsync(model.Id);
            mapper.Map(model, dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductItemViewModel>(dbo);
        }
        public async Task<List<ProductItemViewModel>> GetProductItems(List<int> id)
        {
            var dbo = await db.ProductItems.ToListAsync();
            return dbo.Select(y => mapper.Map<ProductItemViewModel>(y)).ToList();

        }


        public async Task<ProductCategoryViewModel> AddProductCategory(ProductCategoryBinding model)
        {

            var dbo = mapper.Map<ProductCategory>(model);

            db.ProductCategories.Add(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductCategoryViewModel>(dbo);
        }

        public async Task<ProductCategoryViewModel> GetProductCategory(int id)
        {
            var dbo = await db.ProductCategories
                .Include(y => y.ProductItems)
                .FirstOrDefaultAsync(y => y.Id == id);
            return mapper.Map<ProductCategoryViewModel>(dbo);

        }
        public async Task<List<ProductCategoryViewModel>> GetProductCategories()
        {
            var categories = await db.ProductCategories.ToListAsync();
            return categories.Select(c => mapper.Map<ProductCategoryViewModel>(c)).ToList();
        }


        public async Task<ProductCategoryViewModel> UpdateProductCategory(ProductCategoryUpdateBinding model)
            {

            var dbo = await db.ProductCategories.FindAsync(model.Id);
            mapper.Map(model, dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductCategoryViewModel>(dbo);
            
        
            }

        public async Task<ProductCategoryViewModel> DeleteProductCategory(int id)
        {
            var dbo = await db.ProductCategories.FindAsync(id);
            db.ProductCategories.Remove(dbo);
            await db.SaveChangesAsync();
            return mapper.Map<ProductCategoryViewModel>(dbo);

        }
        public async Task<T> GetProductCategory<T>(int id)
        {
            var dbo = await db.ProductCategories
                .Include(y => y.ProductItems)
                .FirstOrDefaultAsync(y => y.Id == id);
            return mapper.Map<T>(dbo);
        }

        public async Task<T> GetProductItem<T>(int id)
        {
            var dbo = await db.ProductItems
                .Include(y => y.ProductCategory)
                .FirstOrDefaultAsync(y => y.Id == id);
               return mapper.Map<T>(dbo);
        }

    }
}
