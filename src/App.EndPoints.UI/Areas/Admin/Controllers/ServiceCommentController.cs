using App.Domain.Core.HomeService.Contracts.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServiceCommentController : Controller
    {
        private readonly IServiceCommentAppService _serviceCommentAppService;
        public ServiceCommentController(IServiceCommentAppService serviceCommentAppService)
        {
            _serviceCommentAppService = serviceCommentAppService;
        }
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var commments = await _serviceCommentAppService.GetAll(cancellationToken);
            return View(commments);
        }
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
           await _serviceCommentAppService.Delete(id, cancellationToken);
            return RedirectToAction("Index");
        }
    }
}
