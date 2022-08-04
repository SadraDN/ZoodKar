using App.Domain.Core.HomeService.Contracts.AppServices;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Dtos;
using App.EndPoints.UI.Models.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoints.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAppUserAppService _appUserAppService;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IServiceAppService _serviceAppService;
        public AccountController(IAppUserAppService AppUserAppService,
            IHttpContextAccessor httpContext
            , IServiceAppService serviceAppService)
        {
            _appUserAppService = AppUserAppService;
            _httpContext = httpContext;
            _serviceAppService = serviceAppService;
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

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            var loggedUser = await _appUserAppService.GetLoggedUserId();
            ViewBag.UserId = loggedUser;
            var user = await _appUserAppService.Get(loggedUser);
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AppUserDto model)
        {
            var loggedUser = await _appUserAppService.GetLoggedUserId();
            var user = new AppUserDto
            {
                Id = loggedUser,
                UserName = model.UserName,
                Name = model.Name,
                Password = model.Password,
                Email = model.Email,
            };
            await _appUserAppService.UpdateUsers(user);

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<IActionResult> EditExpertSkills(CancellationToken cancellationToken)
        {
            var loggeduser = await _appUserAppService.GetLoggedUserId();
            ViewBag.UserId = loggeduser;
            var user = await _appUserAppService.Get(loggeduser);
            var services = await _serviceAppService.GetAll(cancellationToken);
            ViewBag.Services = services.Where(x=>!user.Services.Any(y => y.Title == x.Title))
                .Select(x => new SelectListItem()
            { Value = x.Id.ToString(), Text = x.Title }).ToList();
            var userServices = user.Services;
            return View(userServices);
        }
        [HttpPost]
        public async Task<IActionResult> EditExpertSkills(int expertId, List<int> services, CancellationToken cancellationToken)
        {
            await _appUserAppService.UpdateExpertSkills(expertId, services, cancellationToken);
            return RedirectToAction("Index","Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
