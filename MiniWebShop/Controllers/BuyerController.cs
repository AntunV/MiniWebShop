using Microsoft.AspNetCore.Mvc;
using MiniWebShop.Services.Implementations;
using MiniWebShop.Services.Interfaces;
using MiniWebShop.Shared.Models.Binding.OrderModels;
using MiniWebShop.Shared.Models.Binding;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using MiniWebShop.Shared.Models.Dto;

namespace MiniWebShop.Controllers
{
    [Authorize(Roles = Roles.Buyer)]
    public class BuyerController : Controller
    {  
        
       private readonly IBuyerService buyerService;
       private readonly IProductService productService;
       private readonly IAccountService accountService;
       private readonly ICommonService commonService;
       private static string OrderItemSessionKey = "OrderItems";

        public BuyerController(IBuyerService buyerService, IProductService productService, IAccountService accountService, ICommonService commonService)
        {
            this.buyerService = buyerService;
            this.productService = productService;
            this.accountService = accountService;
            this.commonService = commonService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await productService.GetProductCategories();
            return View(categories);
        }

        public async Task<IActionResult> Details(int id)
        {
            var category = await productService.GetProductCategory(id);
            return View(category);
        }

        public async Task<IActionResult> Order()
        {
            var sessionOrderItems = HttpContext.Session.GetString(OrderItemSessionKey);
            List<OrderItemBinding> existingOrderItems = sessionOrderItems != null ?
                JsonSerializer.Deserialize<List<OrderItemBinding>>(sessionOrderItems) : new List<OrderItemBinding>();

            if (!existingOrderItems.Any())
            {
                var sessionFromDb = await commonService.GetSessionItem<List<OrderItemBinding>>(OrderItemSessionKey, User);
                if (sessionFromDb != null)
                {
                    existingOrderItems = sessionFromDb;
                    HttpContext.Session.SetString(OrderItemSessionKey, JsonSerializer.Serialize(existingOrderItems));
                }

            }


            var response = new OrderBinding
            {
                OrderItems = existingOrderItems,
                OrderAddress = await accountService.GetUserAddress<AddressBinding>(User)
            };

            return View(response);
        }


        public async Task<IActionResult> CancelOrder(int id)
        {
            var orders = await buyerService.CancelOrder(id);
            return RedirectToAction("MyOrders");
        }


        public async Task<IActionResult> MyOrders()
        {
            var orders = await buyerService.GetOrders(User);
            foreach (var order in orders)
            {
                order.Created = DateTime.Now;
            }
            return View(orders);
        }

        public async Task<IActionResult> MyOrder(int id)
        {
            var orders = await buyerService.GetOrder(id, User);
          
            if (orders == null)
            {
                return NotFound();
            }
            
            return View(orders);
        }


        [HttpPost]
        public async Task<IActionResult> Order(OrderBinding model)
        {
            var order = await buyerService.AddOrder(model, User);
            HttpContext.Session.Remove(OrderItemSessionKey);
            await commonService.RemoveFromSession(OrderItemSessionKey, User);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> AddToOrderItem([FromBody] List<OrderItemBinding> orderItems)
        {
            try
            {
                // Retrieve the existing session list of OrderItemBiding
                var sessionOrderItems = HttpContext.Session.GetString(OrderItemSessionKey);

                List<OrderItemBinding> existingOrderItems = sessionOrderItems != null ?
                    JsonSerializer.Deserialize<List<OrderItemBinding>>(sessionOrderItems) : new List<OrderItemBinding>();

                foreach (var orderItem in orderItems)
                {
                    var existingOrderItem = existingOrderItems.FirstOrDefault(item => item.ProductItemId == orderItem.ProductItemId);

                    if (existingOrderItem != null)
                    {
                        existingOrderItem.Quantity += orderItem.Quantity;
                    }
                    else
                    {
                        existingOrderItems.Add(orderItem);
                    }

                }

                await commonService.AddSessionItem(OrderItemSessionKey, existingOrderItems, User);
                HttpContext.Session.SetString(OrderItemSessionKey, JsonSerializer.Serialize(existingOrderItems));

                return Json(existingOrderItems);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }

}

