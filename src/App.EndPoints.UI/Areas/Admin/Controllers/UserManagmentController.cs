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
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public UserManagmentController(UserManager<AppUser> userManager,
            RoleManager<IdentityRole<int>> roleManager,
            SignInManager<AppUser> signInManager,
            IPasswordHasher<AppUser> passwordHasher)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }

        public async Task<IActionResult> Index(string? SearchStrin)
        {
            var users = await _userManager.Users.ToListAsync();
            var model = users.Select(x => new UserOutputVM
            {
                Id = x.Id,
                UserName = x.UserName,
                Email = x.Email,
                Roles = (_userManager.GetRolesAsync(x).Result).ToList()
            }).ToList();
            if (!string.IsNullOrEmpty(SearchStrin))
            {
                model = model.Where(s => s.UserName.ToLower()!.Contains(SearchStrin.ToLower())).ToList();
            }


            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = _roleManager.Roles.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Name
            });
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserInputVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.UserName,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);


                if (result.Succeeded)
                {
                    
                    foreach (var role in model.Roles)
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }

                    await _signInManager.SignInAsync(user, isPersistent: false);

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
        public async Task<IActionResult> Edit(int id)
        {

            var user = await _userManager.FindByIdAsync(id.ToString());
            var roles = (_userManager.GetRolesAsync(user).Result).ToList();
            var _roles = _roleManager.Roles.Where(t => !roles.Contains(t.Name)).Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Name
            }).ToList();

            ViewBag.Roles = _roles;
            if (user != null)
            {
                UserUpdateVM model = new UserUpdateVM();
                model.UserName = user.UserName;
                model.Email = user.Email;
                model.Id = user.Id;
                model.Roles = (_userManager.GetRolesAsync(user).Result).ToList();
                return View(model);
            }
            else
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateVM model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user != null)
            {
                if (!string.IsNullOrEmpty(model.UserName))
                    user.UserName = model.UserName;
                else
                    ModelState.AddModelError("", "UserName cannot be empty");
                if (!string.IsNullOrEmpty(model.Email))
                    user.Email = model.Email;
                else
                    ModelState.AddModelError("", "Email cannot be empty");

                if (!string.IsNullOrEmpty(model.Password))
                    user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                else
                    ModelState.AddModelError("", "Password cannot be empty");

                var rol = await _userManager.GetRolesAsync(user);
                foreach (var item in rol)
                {
                    if (model.Roles.Exists(x => x != item))
                    {
                        await _userManager.RemoveFromRoleAsync(user, item);
                    }
                }

                foreach (var role in model.Roles)
                {
                    if (!string.IsNullOrEmpty(role))
                    {
                        await _userManager.AddToRoleAsync(user, role);
                    }
                }

                if (!string.IsNullOrEmpty(model.Email) || !string.IsNullOrEmpty(model.Password))
                {
                    IdentityResult result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                        return RedirectToAction("Index");
                    else
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, item.Description);
                        }
                }
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return View(model);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }
}
