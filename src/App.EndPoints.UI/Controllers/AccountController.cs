using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Dtos;
using App.EndPoints.UI.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppUserAppService _appUserAppService;
        private readonly IHttpContextAccessor _httpContext;
        public AccountController(IAppUserAppService AppUserAppService,
            IHttpContextAccessor httpContext)
        {
            _appUserAppService = AppUserAppService;
            _httpContext = httpContext;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnurl)
        {
            ViewBag.ReturnUrl = returnurl;
            await _appUserAppService.SignOutUser();
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string? ReturnUrl)
        {

            if (ModelState.IsValid)
            {
                var user = new AppUserDto()
                {
                    Password = model.Password,
                    UserName = model.UserName,
                };
                var result = await _appUserAppService.Login(user, model.RememberMe);
                if (result.Succeeded)
                {
                    if (ReturnUrl != null)
                        return LocalRedirect(ReturnUrl);
                    else
                        return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "خطا در فرآیند لاگین");
            }
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _appUserAppService.SignOutUser();
            return LocalRedirect("~/");
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUserDto
                {
                    Password = model.Password,
                    Name = model.Name,
                    UserName = model.UserName,
                    Email = model.Email,
                };

                var result = await _appUserAppService.Create(user);
                if (result.Succeeded)
                {

                    await _appUserAppService.Login(user, rememberMe: true);
                    return LocalRedirect("~/");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, item.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
