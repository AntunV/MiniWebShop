using MiniWebShop.Services.Implementations;
using MiniWebShop.Services.Interfaces;
using MiniWebShop.Shared.Models.Binding.ProductModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniWebShop.UnitTest
{
    public class ProductServiceUnitTest : MiniWebShopSetup
    {
        private readonly IProductService productService;

        public ProductServiceUnitTest()
        {
            this.productService = GetProductService();
        }


        [Fact]
        public async void GetProductCategory_FetchesOrderFromDb_ValidatesIfItemIsNotNull()
        {
            var result = await productService.GetProductCategory(ProductCategories[0].Id);
            Assert.NotNull(result);
        }

        [Fact]
        public async void AddProductItem_AddsNewEntityToDb_ReturnsViewModel()
        {

            var response = await productService.AddProductItem(new ProductItemBinding
            {
                Description = TestString,
                Name = TestString,
                Price = 1233,
                ProductCategoryId = ProductCategories[1].Id,
                Quantity = 10
            });

            Assert.NotNull(response);

        }

        [Fact]
        public async void DeleteProductItem_DeletesEntityFromDb_ValidatesIfItemIsNull()
        {
            var addedItem = await productService.AddProductItem(new ProductItemBinding
            {
                Description = TestString + "x",
                Name = TestString,
                Price = 1233,
                ProductCategoryId = ProductCategories[12].Id,
                Quantity = 10
            });
            Assert.NotNull(addedItem);


            await productService.DeleteProductItem(addedItem.Id);
            var productCategory = await productService.GetProductCategory(ProductCategories[12].Id);
            var productItem = productCategory.ProductItems.FirstOrDefault(y => y.Id == addedItem.Id);
            Assert.Null(productItem);


        }

        [Fact]
        public async void UpdateProductCategory_UpdatesElementInDb_ReturnsUpdatedItem()
        {

            var response = await productService.UpdateProductCategory(new ProductCategoryUpdateBinding
            {
                Id = ProductCategories[20].Id,
                Description = TestString,
                CategoryName = TestString,
            });

            Assert.NotNull(response);
            Assert.Equal(TestString, response.Description);
            Assert.Equal(TestString, response.CategoryName);


        }

        [Fact]
        public async void AddProductCategory_AddsNewEntityToDb_ReturnsViewModel()
        {

            var response = await productService.AddProductCategory(new ProductCategoryBinding
            {
                CategoryName = TestString,
                Description = TestString,
            });

            Assert.NotNull(response);
            Assert.Equal(TestString, response.Description);
            Assert.Equal(TestString, response.CategoryName);

            response = await productService.GetProductCategory(response.Id);
            Assert.NotNull(response);

        }

        [Fact]
        public async void DeleteProductCategory_DeletesEntityFromDb_ValidatesIfItemIsNull()
        {
            var deletedId = ProductCategories[12].Id;
            await productService.DeleteProductCategory(deletedId);

            var allItems = await productService.GetProductCategories();
            Assert.Null(allItems.FirstOrDefault(y => y.Id == deletedId));
        }
    } 


}

