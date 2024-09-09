using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniWebShop.Services.Interfaces;
using MiniWebShop.Shared.Models.Binding.ProductModels;
using MiniWebShop.Shared.Models.Dto;

namespace MiniWebShop.Controllers
{
    [Authorize(Roles = Roles.Admin)]
    public class AdminController : Controller
    {
        private readonly IProductService productService;
        private readonly IBuyerService buyerService;
        public AdminController(IProductService productService, IBuyerService buyerService)
        {
            this.productService = productService;
            this.buyerService = buyerService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await productService.GetProductCategories();
            return View(categories);
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCategoryBinding model)
        {
            await productService.AddProductCategory(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await productService.GetProductCategory<ProductCategoryUpdateBinding>(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductCategoryUpdateBinding model)
        {
            await productService.UpdateProductCategory(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await productService.DeleteProductCategory(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var productCategory = await productService.GetProductCategory(id);
            return View(productCategory);
        }

        public async Task<IActionResult> AddProductItem(int categoryId)
        {
            var model =  new ProductItemBinding
            {
                ProductCategoryId = categoryId
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddProductItem(ProductItemBinding model)
        {

            await productService.AddProductItem(model);

            return RedirectToAction(nameof(Details), new { id = model.ProductCategoryId });
        }

        public async Task<IActionResult> DeleteProductItem(int id)
        {
            var response = await productService.DeleteProductItem(id);
            return RedirectToAction(nameof(Details), new { id = response.ProductCategoryId });
        }


        public async Task<IActionResult> EditProductItem(int id)
        {
            var model = await productService.GetProductItem<ProductItemUpdateBinding>(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditProductItem(ProductItemUpdateBinding model)
        {
            await productService.UpdateProductItem(model);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Orders()
        {
            var orders = await buyerService.GetOrders(User);
            return View(orders);
        }

        public async Task<IActionResult> Order(int id)
        {
            var order = await buyerService.GetOrder(id, User);
            return View(order);
        }

        public async Task<IActionResult> CancelOrder(int id)
        {
            var orders = await buyerService.CancelOrder(id);
            return RedirectToAction(nameof(Orders));
        }




    }
}

