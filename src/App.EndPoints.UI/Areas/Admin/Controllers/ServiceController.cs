using App.Domain.Core.HomeService.Contracts.AppServices;
using App.Domain.Core.HomeService.Dtos;
using App.Domain.Core.User.Contracts.AppServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceController : Controller
    {
        private readonly IServiceAppService _serviceAppService;
        private readonly ICategoryAppService _categoryAppService;
        private readonly IAppUserAppService _appUserAppService;

        public ServiceController(IServiceAppService serviceAppService,
            ICategoryAppService categoryAppService,
            IAppUserAppService appUserAppService )
        {
            _serviceAppService = serviceAppService;
            _categoryAppService = categoryAppService;
            _appUserAppService = appUserAppService;
        }

        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var services = await _serviceAppService.GetAll(cancellationToken);
            return View(services);
        }
        [HttpGet]
        public async Task<IActionResult> Create(CancellationToken cancellationToken)
        {
            var categories = await _categoryAppService.GetAll(cancellationToken);
            ViewBag.Categories = categories.Select(s => new SelectListItem
            {
                Text = s.Title,
                Value = s.Id.ToString()
            });
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ServiceDto model,List<IFormFile>? serviceFile,CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                var service = new ServiceDto
                {
                    CategoryId = model.CategoryId,
                    Price = model.Price,
                    ShortDescription = model.ShortDescription,
                    Title = model.Title,
                };
                await _serviceAppService.Set(service, serviceFile, cancellationToken);
                return RedirectToAction("Index");
            }
            return View(model);
        } 
    }
}
