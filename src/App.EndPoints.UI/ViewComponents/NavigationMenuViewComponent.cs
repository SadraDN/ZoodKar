using App.Domain.Core.HomeService.Contracts.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.ViewComponents
{
    public class NavigationMenuViewComponent: ViewComponent
    {
        private readonly ICategoryAppService _categoryAppService;
        public NavigationMenuViewComponent(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        public async Task<IViewComponentResult> InvokeAsync(CancellationToken cancellationToken)
        {
            var model =await _categoryAppService.GetAll(cancellationToken);
            return View(model);
        }
    }
}
