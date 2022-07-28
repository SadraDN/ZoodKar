using App.Domain.Core.HomeService.Contracts.AppServices;
using App.Domain.Core.HomeService.Dtos;
using App.Domain.Core.User.Contracts.AppServices;
using App.Domain.Core.User.Entities;
using App.EndPoints.UI.Models.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Controllers
{
    
    public class OrderController : Controller
    {
        private readonly IOrderAppService _orderAppService;
        private readonly IServiceAppService _serviceAppService;
        private readonly ICategoryAppService _categoryAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAppUserAppService _appUserAppService;
        private readonly UserManager<AppUser> _userManager;
        public OrderController(IOrderAppService OrderAppService
            , IServiceAppService ServiceAppService
             , ICategoryAppService CategoryAppService
            , IHttpContextAccessor HttpContextAccessor
            , UserManager<AppUser> UserManager
            , IAppUserAppService AppUserAppService)
        {
            _orderAppService = OrderAppService;
            _serviceAppService = ServiceAppService;
            _categoryAppService = CategoryAppService;
            _httpContextAccessor = HttpContextAccessor;
            _userManager = UserManager;
            _appUserAppService = AppUserAppService;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var record = await _categoryAppService.GetAll(cancellationToken);
            var categories = record.Select(c => new CategoryVM
            {
                Id = c.Id,
                Title = c.Title,
            }).ToList();
            return View(categories);
        }


        public async Task<IActionResult> ServiceList(int categoryId,CancellationToken cancellationToken)
        {
            var record = await _serviceAppService.GetAll(cancellationToken);
            var services = record.Where(c=>c.CategoryId==categoryId).Select(c => new ServiceVM
            {
                CategoryName = c.CategoryName,
                Title = c.Title,
                CategoryId = c.CategoryId,
                Price = c.Price,
                Id = c.Id,
                ShortDescription = c.ShortDescription,
            }).ToList();
            return View(services);
        }

        [Authorize(Roles ="CustomerRole")]
        [HttpGet]
        public async Task<IActionResult> Create(int serviceId,CancellationToken cancellationToken)
        {
            ViewBag.ServiceId = Convert.ToInt32(serviceId);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(OrderInputVM model,int id,CancellationToken cancellationToken)
        {
            var userId = await _appUserAppService.GetLoggedUserId();
            if (ModelState.IsValid)
            {
                var order = new OrderDto
                {
                    ServiceDate = model.ServiceTime,
                    SerivceAddress = model.Address,
                    ServiceId = id,
                    CustomerUserId = userId,
                };
                await _orderAppService.Set(order, cancellationToken);
                return RedirectToAction("Index");
            }
            return View(model);
            
        }
    }
}
