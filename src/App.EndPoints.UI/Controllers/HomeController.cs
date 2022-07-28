using App.Domain.Core.HomeService.Dtos;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Entities;
using App.EndPoints.UI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace App.EndPoints.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;
        private readonly IAppUserAppService _appUserAppService;
        public HomeController(UserManager<AppUser> UserManager
            , RoleManager<IdentityRole<int>> RoleManager,
            IAppUserAppService appUserAppService)
        {
            _userManager = UserManager;
            _roleManager = RoleManager;
            _appUserAppService = appUserAppService;
        }

        public async Task<IActionResult> Index(string? serach)
        {
            var users = await _appUserAppService.GetAll(serach);
            if (users.Count == 0)
            {
                var adminCroleCreation = await _roleManager.CreateAsync(new IdentityRole<int>("AdminRole"));
                var customerCroleCreation = await _roleManager.CreateAsync(new IdentityRole<int>("CustomerRole"));
                var expertCroleCreation = await _roleManager.CreateAsync(new IdentityRole<int>("ExpertRole"));
                var status = new OrderStatusDto()
                {
                    Id = 1,
                    Title = "در انتظار پیشنهاد متخصصان"
                };
                return View();
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }




        [HttpGet]
        public async Task<IActionResult> SeedData()
        {
            return Ok();
        }
    }
}