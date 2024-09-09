using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniWebShop.Services.Interfaces;
using MiniWebShop.Shared.Models.Binding.AccountModels;
using MiniWebShop.Shared.Models.Dto;

namespace MiniWebShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationBinding model)
        {
            await accountService.CreateUser(model, Roles.Buyer);
            return RedirectToAction("Index", "Buyer");
        }

        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            var profile = await accountService.GetUserProfile<ApplicationUserUpdateBinding>(User);
            return View(profile);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MyProfile(ApplicationUserUpdateBinding model)
        {
            await accountService.UpdateUserProfile(model);
            return RedirectToAction("MyProfile");
        }
    }
}
