using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Dtos;
using App.Domain.Core.User.Entities;
using App.EndPoints.UI.Areas.Admin.Models.ViewModels.UserManagment;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    public class UserManagmentController : Controller
    {
        private readonly IAppUserAppService _appUserAppService;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public UserManagmentController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            SignInManager<AppUser> signInManager,
            IPasswordHasher<AppUser> passwordHasher
            , IAppUserAppService AppUserAppService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
            _appUserAppService = AppUserAppService;
        }

        public async Task<IActionResult> Index(string? search,CancellationToken cancellationToken)
        {
            var users = await _appUserAppService.GetAll(search,cancellationToken);
            var model = users.Select(x => new UserOutputVM
            {
                Id = x.Id,
                Name = x.Name,
                UserName = x.UserName,
                Email = x.Email,
                Roles = x.Roles.ToList()
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var roles = await _appUserAppService.GetRoles();
            ViewBag.Roles = roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Name,
            });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserInputVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUserDto
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    Roles = model.Roles
                };

                var result = await _appUserAppService.Create(user);

                if (result.Succeeded)
                {

                    await _appUserAppService.Login(user,true);

                    return LocalRedirect("~/Admin/Dashboard");
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
        public async Task<IActionResult> Edit(int id)
        {

            var roles = await _appUserAppService.GetRoles();
            var user = await _appUserAppService.Get(id);
            ViewBag.Roles = roles.Where(t => !user.Roles.Contains(t.Name)).Select(x => new SelectListItem()
            { Value = x.Name, Text = x.Name }).ToList();
            if (user != null)
            {
                UserUpdateVM model = new()
                {
                    Name = user.Name,
                    UserName = user.UserName,
                    Email = user.Email,
                    Id = user.Id,
                    Roles = user.Roles.ToList(),
                    Password = user.Password,
                };
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateVM model)
        {
            var user = new AppUserDto
            {
                Id = model.Id,
                UserName = model.UserName,
                Name = model.Name,
                Password = model.Password,
                Email = model.Email,
                Roles = model.Roles,
            };
            await _appUserAppService.Update(user);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}

