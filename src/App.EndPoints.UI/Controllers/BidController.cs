using App.Domain.Core.HomeService.Contracts.AppServices;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.UI.Controllers
{
    public class BidController : Controller
    {
        private readonly IBidAppService _bidAppService;
        public BidController(IBidAppService bidAppService)
        {
            _bidAppService = bidAppService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
