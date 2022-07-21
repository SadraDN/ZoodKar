
using App.Domain.Core.User.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SeedDataController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        public SeedDataController(UserManager<AppUser> UserManager
            , RoleManager<IdentityRole<int>> RoleManager)
        {
            _userManager = UserManager;
            _roleManager = RoleManager;
        }
        public async Task<IActionResult> Data()
        {
            var adminCroleCreation = await _roleManager.CreateAsync(new IdentityRole<int>("AdminRole"));
            var customerCroleCreation = await _roleManager.CreateAsync(new IdentityRole<int>("CustomerRole"));
            var expertCroleCreation = await _roleManager.CreateAsync(new IdentityRole<int>("ExpertRole"));
            return View();
        }
    }
}
